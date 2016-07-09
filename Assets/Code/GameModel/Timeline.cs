using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class TimelineConfig
{
	public Timeline.Type type;
	public int id;
	public int playerActionDuration;
	public int totalPlayerActionDuration;
	public int range;
}

public class Timeline
{
	public enum Type
	{
		Shield,
		Jumper,
		Shooter
	}

	public TimelineConfig m_config;
	public Player m_player;
	public List<Danger> m_dangers = new List<Danger>();
	public int m_tick;
	public int score = 0;

	public Type TimelineType
	{
		get { return m_config.type; }
	}

	public int Id
	{
		get { return m_config.id; }
	}

	public TimelineConfig Config
	{
		get { return m_config; }
	}

	public event System.Action<Danger> OnPlayerDeath;
	public event System.Action<Danger> OnDangerAdded;

	public Timeline(TimelineConfig config)
	{
		m_config = config;

		m_player = new Player(Id, m_config.playerActionDuration);
	}

	public void AddDangerToTimeline(Danger danger)
	{
		danger.distanceLeft = danger.timestamp - m_tick;
		m_dangers.Add(danger);
		m_dangers.Sort();

		if (OnDangerAdded != null)
		{
			OnDangerAdded(danger);
		}
	}

	public bool IsTimestampSafe(Player.Action requiredAction, int timestamp){
		if(m_dangers.Count == 0){
			if (timestamp > m_player.timestamp) return true;
			return false;
		}
		if(timestamp < m_player.timestamp || timestamp < m_dangers[m_dangers.Count-1].timestamp){ 
			return false;
		}
		else if(m_dangers.Count > 0 && timestamp - m_dangers[m_dangers.Count-1].timestamp > m_config.playerActionDuration){
			return true;
		}
		return false;
	}

	public bool StageComplete(){
		if(m_dangers.Count == 0) return true;
		return m_player.timestamp > m_dangers[m_dangers.Count-1].timestamp;
	}

	public int TimeUntilImpact(){
		Danger nextActiveDanger = NextActiveDanger();
		return nextActiveDanger.timestamp - m_player.timestamp;
	}

	public Danger NextActiveDanger(){
		if (m_dangers.Count != 0)
			return m_dangers[0];

		return null; 
	}

	private List<Danger> GetDangersFromFile(int stage){
		string json = System.IO.File.ReadAllText(Application.dataPath+"/Saves/stage" + stage + "_dangers" + Id + ".json");
		DangerDataContainer ddc = JsonUtility.FromJson<DangerDataContainer>(json);

		List<Danger> dangers = new List<Danger>();
		foreach (var dangerData in ddc.container) {
			dangers.Add(new Danger(dangerData));
		}

		return dangers;
	}

	public void TryToPerformAction(Player.Action action)
	{
		m_player.TryToPerformAction(action);
	}

	enum Outcome
	{
		Nothing,
		PlayerDied,
		DangerDestroyed
	}

	public void Step()
	{
		m_tick++;

		m_player.Step();
		m_player.timestamp = m_tick;

		List<Danger> dangersToRemove = new List<Danger>();

		if (TimelineType == Type.Shooter)
		{
			var dangerToDestroy = HandleShooterFireBehaviour();
			if (dangerToDestroy != null)
			{
				AudioEvent.Play("EnemyHit");
				if(dangerToDestroy.hp <= 1){
					DestroyDanger(dangerToDestroy, dangersToRemove);
				}
				else {
					dangerToDestroy.hp--;
				}
			}
		}

		foreach (var danger in m_dangers)
		{
			danger.Step(m_tick);

			Outcome outcome = Outcome.Nothing;

			if (TimelineType == Type.Shield)
			{
				outcome  = HandleShieldBehaviour(danger);
			}
			else if (TimelineType == Type.Jumper)
			{
				outcome = HandleJumperBehaviour(danger);
			}
			else if (TimelineType == Type.Shooter)
			{
				outcome = HandleShooterDeathBehaviour(danger);
			}

			if (outcome == Outcome.DangerDestroyed)
			{
				DestroyDanger(danger, dangersToRemove);
			}
			else if(outcome == Outcome.PlayerDied)
			{
				if (OnPlayerDeath != null)
				{
					OnPlayerDeath(danger);
					break;
				}
			}
		}

		foreach (var danger in dangersToRemove)
		{
			m_dangers.Remove(danger);
		}
	}

	void DestroyDanger(Danger danger, List<Danger> dangersToRemove)
	{
		score += danger.hp * danger.difficulty;
		danger.Destroy();
		dangersToRemove.Add(danger);
	}

	Outcome HandleShieldBehaviour(Danger danger)
	{
		if (danger.distanceLeft == 0)
		{
			if (m_player.m_action == danger.requiredAction)
			{
				return Outcome.DangerDestroyed;
			}
			else
			{
				return Outcome.PlayerDied;
			}
		}

		return Outcome.Nothing;
	}

	Outcome HandleJumperBehaviour(Danger danger)
	{
		if (danger.distanceLeft == 0)
		{
			if (m_player.state == Player.State.Safe && m_player.m_action == danger.requiredAction)
			{
				return Outcome.DangerDestroyed;
			}
			else
			{
				return Outcome.PlayerDied;
			}
		}

		return Outcome.Nothing;
	}

	Outcome HandleShooterDeathBehaviour(Danger danger)
	{
		if (danger.distanceLeft == 0)
		{
			return Outcome.PlayerDied;
		}

		return Outcome.Nothing;
	}

	Danger HandleShooterFireBehaviour()
	{
		if (m_player.AtFirstFrameOfAction())
		{
			var action = m_player.m_action;

			var first = m_dangers.FirstOrDefault(d => d.requiredAction == action);
			if (first != null && first.distanceLeft < m_config.range)
			{
				return first;
			}
		}

		return null;
	}
}

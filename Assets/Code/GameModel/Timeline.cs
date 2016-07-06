using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline
{
	public enum Type
	{
		Defend,
		Attack
	}

	public int id;
	public Player player;
	public List<Danger> m_dangers = new List<Danger>();
	public int m_tick;
	public Type TimelineType { get; set; }

	public event System.Action<Danger> OnPlayerDeath;
	public event System.Action<Danger> OnDangerAdded;

	public Timeline(int stage, int id){
		this.id = id;
		player = new Player(id);
	}

	public void AddDangerToTimeline(Danger danger)
	{
		m_dangers.Add(danger);

		if (OnDangerAdded != null)
		{
			OnDangerAdded(danger);
		}
	}

	public bool StageComplete(){
		return player.timestamp > m_dangers[m_dangers.Count-1].timestamp;
	}

	public int TimeUntilImpact(){
		Danger nextActiveDanger = NextActiveDanger();
		return nextActiveDanger.timestamp - player.timestamp;
	}

	public Danger NextActiveDanger(){
		foreach (var danger in m_dangers) {
			if(danger.timestamp >= player.timestamp 
				&& danger.state != Danger.State.Dead)
			{
				return danger;
			}
		}
		return null; //we should do the StageComplete check earlier
	}

	private List<Danger> GetDangersFromFile(int stage){
		string json = System.IO.File.ReadAllText(Application.dataPath+"/Saves/stage" + stage + "_dangers" + id + ".json");
		DangerDataContainer ddc = JsonUtility.FromJson<DangerDataContainer>(json);

		List<Danger> dangers = new List<Danger>();
		foreach (var dangerData in ddc.container) {
			dangers.Add(new Danger(dangerData));
		}

		return dangers;
	}

	public void TryToPerformAction(Player.Action action)
	{
		player.TryToPerformAction(action);
	}

	public void Step()
	{
		m_tick++;

		player.Step();
		player.timestamp = m_tick;

		List<Danger> dangersToRemove = new List<Danger>();

		foreach (var danger in m_dangers)
		{
			danger.Step(m_tick);

			bool destroyedDanger = false;

	/*		if (TimelineType == Type.Attack)
			{
				destroyedDanger |= HandleAttackBehaviour(danger);
			}
			else
			{
				destroyedDanger |= HandleDefensiveBehaviour(danger);
			}*/



			if (danger.distanceLeft == 0)
			{
				if (player.state == Player.State.Safe && player.m_action == danger.requiredAction)
				{
					//Safe
					danger.Destroy();
					dangersToRemove.Add(danger);
				}
				else
				{
					//You die
					if (OnPlayerDeath != null)
					{
						OnPlayerDeath(danger);
					}
				}
			}
		}

		foreach (var danger in dangersToRemove)
		{
			m_dangers.Remove(danger);
		}
	}

	/// <returns>True if the danger got destroyed</returns>
	bool HandleDefensiveBehaviour(Danger danger)
	{
		return false;
	}

	/// <returns>True if the danger got destroyed</returns>
	bool HandleAttackBehaviour(Danger danger)
	{
		return false;
	}
}

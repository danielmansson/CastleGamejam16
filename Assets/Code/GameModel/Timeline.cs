using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline {
	public int id;
	public Player player;
	public List<Danger> m_dangers = new List<Danger>();
	public int frame;

	public event System.Action<Danger> OnPlayerDeath;
	public event System.Action<Danger> OnDangerAdded;

	public Timeline(int stage, int id){
		this.id = id;
		player = new Player(id);
		/*var dangers = GetDangersFromFile(stage);

		foreach (var danger in dangers)
		{
			AddDangerToTimeline(danger);
		}

		foreach (var item in dangers) {
			Debug.Log(item.type + ", " + item.requiredAction + ", " + item.hp + ", " + item.state + ", " + item.timestamp);
		}
		*/
	}

	public void AddDangerToTimeline(Danger danger)
	{
		m_dangers.Add(danger);

		if (OnDangerAdded != null)
		{
			OnDangerAdded(danger);
		}
	}

	public bool IsTimestampSafe(Player.Action requiredAction, int timestamp){
		if(timestamp > player.timestamp && m_dangers.Count == 0) return true;
		if(timestamp < player.timestamp || timestamp < m_dangers[m_dangers.Count-1].timestamp){ 
			return false;
		}
		else if(m_dangers.Count > 0 && timestamp - m_dangers[m_dangers.Count-1].timestamp > Constants.MAX_DANGER_FREQUENCY[id]){
			return true;
		}
		return false;
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
		frame++;

		player.Step();

		List<Danger> dangersToRemove = new List<Danger>();

		foreach (var danger in m_dangers)
		{
			danger.Step(frame);

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
}

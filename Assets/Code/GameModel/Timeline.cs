using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline {
	public int id;
	public Player player;
	public List<Danger> dangers;

	public Timeline(int id){
		this.id = id;
		player = new Player(id);
		dangers = GetDangersFromFile();

		foreach (var item in dangers) {
			Debug.Log(item.type + ", " + item.requiredAction + ", " + item.hp + ", " + item.state + ", " + item.timestamp);
		}
	}

	public bool StageComplete(){
		return player.timestamp > dangers[dangers.Count-1].timestamp;
	}

	public int TimeUntilImpact(){
		Danger nextActiveDanger = NextActiveDanger();
		return nextActiveDanger.timestamp - player.timestamp;
	}

	public Danger NextActiveDanger(){
		foreach (var danger in dangers) {
			if(danger.timestamp >= player.timestamp 
				&& danger.state != Danger.State.Dead)
			{
				return danger;
			}
		}
		return null; //we should do the StageComplete check earlier
	}

	private List<Danger> GetDangersFromFile(){
		string json = System.IO.File.ReadAllText(Application.dataPath+"/Saves/stage0_dangers" + id + ".json");
		DangerDataContainer ddc = JsonUtility.FromJson<DangerDataContainer>(json);

		List<Danger> dangers = new List<Danger>();
		foreach (var dangerData in ddc.container) {
			dangers.Add(new Danger(dangerData));
		}

		return dangers;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timeline {
	public int id;
	Player player;
	List<Danger> dangers;

	public Timeline(int id){
		this.id = id;
		player = new Player(id);
		dangers = new List<Danger>();
	}

	public bool isFinished(){
		return player.timestamp > dangers[dangers.Count-1].timestamp;
	}

	public int TimeUntilImpact(){
		Danger nextActiveDanger = NextActiveDanger();
		return nextActiveDanger.timestamp - player.timestamp;
	}

	public Danger NextActiveDanger(){
		for(int i = 0; i < dangers.Count; i++){
			if(dangers[i].timestamp >= player.timestamp 
				&& dangers[i].state != Constants.DANGER_DEAD)
			{
				return dangers[i];
			}
		}
	}
}

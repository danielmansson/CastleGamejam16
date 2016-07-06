using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DangerGenerator {

	public static List<List<Danger>> GenerateDangers(int startTick, float seconds, int ticksPerSecond)
	{
		int numDimensions = 3;
		List<List<Danger>> timelineDangers = new List<List<Danger>>();
		for(int i = 0; i < numDimensions; i++)
		{
			timelineDangers.Add(new List<Danger>());
		}

		int[] intensity = {16,3,10};

		for(int i = 0; i < ticksPerSecond*seconds; i++){
			if(i % intensity[0] == 0){
				Danger newDanger = new Danger((Danger.Type)0, (Player.Action)Random.Range(0,1), 1, startTick+i);
				timelineDangers[0].Add(newDanger);
				//verify it won't break anything
			}
			if(i % intensity[1] == 0){
				Danger newDanger = new Danger((Danger.Type)1, (Player.Action)Random.Range(0,1), 1, startTick+i);
				timelineDangers[1].Add(newDanger);
				//verify it won't break anything
			}
			if(i % intensity[2] == 0){
				Danger newDanger = new Danger((Danger.Type)2, (Player.Action)Random.Range(0,1), 1, startTick+i);
				timelineDangers[2].Add(newDanger);
				//verify it won't break anything
			}
		}

		return timelineDangers;
	}
}

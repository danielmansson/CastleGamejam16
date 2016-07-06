using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DangerGenerator {

	public static List<List<Danger>> GenerateDangers(List<Timeline> timelines, int startTick, float seconds, int ticksPerSecond)
	{
		int numDimensions = 3;
		List<List<Danger>> timelineDangers = new List<List<Danger>>();
		for(int i = 0; i < numDimensions; i++)
		{
			timelineDangers.Add(new List<Danger>());
		}

		int[] intensity = {14,5,3};

		for(int i = 0; i < ticksPerSecond*seconds; i++){
			if(i % intensity[0] == 0){
				Danger newDanger = new Danger((Danger.Type)0, (Player.Action)Random.Range(0,2), 1, startTick+i);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelineDangers[0].Add(newDanger);
				}
			}
			if(i % intensity[1] == 0){
				Danger newDanger = new Danger((Danger.Type)1, (Player.Action)Random.Range(0,2), 1, startTick+i);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelineDangers[1].Add(newDanger);
				}
			}
			if(i % intensity[2] == 0){
				Danger newDanger = new Danger((Danger.Type)2, (Player.Action)Random.Range(0,2), 1, startTick+i);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelineDangers[2].Add(newDanger);
				}
			}
		}

		return timelineDangers;
	}

	private static bool DangerDoesntBreakStuff(List<Timeline> timelines, Danger newDanger){
		for(int i = 0; i < 3; i++){
			if(!timelines[i].IsTimestampSafe(newDanger.requiredAction, newDanger.timestamp)){
				return false;
			}
		}
		return true;
	}
}

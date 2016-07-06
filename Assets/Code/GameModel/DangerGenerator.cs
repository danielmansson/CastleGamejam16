using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DangerGenerator {
	public static void GenerateDangers(List<Timeline> timelines, int startTick, float seconds, int ticksPerSecond, int slowingFactor)
	{
		int numDimensions = 3;

		for(int i = 0; i < ticksPerSecond*seconds; i++){
			//written out because might be different later
			if(i % (slowingFactor * timelines[0].m_config.totalPlayerActionDuration) == 0){
				Danger newDanger = new Danger((Danger.Type)0, (Player.Action)Random.Range(0,2), 1, startTick+i);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelines[0].AddDangerToTimeline(newDanger);
				}
			}
			if(i % (slowingFactor * timelines[1].m_config.totalPlayerActionDuration) == 0){
				Danger newDanger = new Danger((Danger.Type)1, (Player.Action)Random.Range(0,2), 1, startTick+i);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelines[1].AddDangerToTimeline(newDanger);
				}
			}
			if(i % (slowingFactor * timelines[2].m_config.totalPlayerActionDuration) == 0){
				Danger newDanger = new Danger((Danger.Type)2, (Player.Action)Random.Range(0,2), 1, startTick+i);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelines[2].AddDangerToTimeline(newDanger);
				}
			}
		}
	}

	private static bool DangerDoesntBreakStuff(List<Timeline> timelines, Danger newDanger){
		for(int i = 0; i < timelines.Count; i++){
			if(!timelines[i].IsTimestampSafe(newDanger.requiredAction, newDanger.timestamp)){
				return false;
			}
		}
		return true;
	}
}

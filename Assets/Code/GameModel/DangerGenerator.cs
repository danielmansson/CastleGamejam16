using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DangerGenerator {
	public static void GenerateDangers(List<Timeline> timelines, int startTick, Stage stage)
	{
		//timelines[0].m_dangers.Clear();
		//timelines[1].m_dangers.Clear();
		//timelines[2].m_dangers.Clear();

		for(int i = 0; i < stage.ticksPerSecond*stage.duration; i++){
			//written out because might be different later
			int dangerTimestamp = ClosestEight(startTick+i);
			if(i % (stage.slowFactor * timelines[0].m_config.totalPlayerActionDuration) == 0){
				Danger newDanger = new Danger((Danger.Type)0, (Player.Action)Random.Range(0,2), 1, dangerTimestamp);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelines[0].AddDangerToTimeline(newDanger);
				}
			}
			if(i % (stage.slowFactor * timelines[1].m_config.totalPlayerActionDuration) == 0){
				Danger newDanger = new Danger((Danger.Type)1, (Player.Action)Random.Range(0,2), 1, dangerTimestamp);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelines[1].AddDangerToTimeline(newDanger);
				}
			}
			if(i % (stage.slowFactor * timelines[2].m_config.totalPlayerActionDuration) == 0){
				Danger newDanger = new Danger((Danger.Type)2, (Player.Action)Random.Range(0,2), 1, dangerTimestamp);
				if(DangerDoesntBreakStuff(timelines, newDanger)){
					timelines[2].AddDangerToTimeline(newDanger);
				}
			}
		}
		//thaba woo
	}

	private static int ClosestEight(int a)
	{
		while(a%8 != 0) a++;
		return a;
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModelConfig
{
	public float secondsPerTick = (1f / 5f);
}

public class GameModel 
{
	GameModelConfig m_config;
	List<Timeline> timelines = new List<Timeline>();
	float m_stepAccumulator;

	public GameModel(GameModelConfig config)
	{
		m_config = config;

		timelines.Add(new Timeline(0));
		timelines.Add(new Timeline(1));
		timelines.Add(new Timeline(2));

		//tmp dummy data
		timelines[0].dangers.Add(new Danger(Danger.Type.Block, Player.Action.Right, 1, 0x1f));
		timelines[0].dangers.Add(new Danger(Danger.Type.Block, Player.Action.Right, 1, 0x2f));
		timelines[1].dangers.Add(new Danger(Danger.Type.Monster, Player.Action.Right, 1, 0x3f));
		timelines[1].dangers.Add(new Danger(Danger.Type.Monster, Player.Action.Right, 1, 0x4f));
		timelines[2].dangers.Add(new Danger(Danger.Type.Shot, Player.Action.Right, 1, 0x5f));
		timelines[2].dangers.Add(new Danger(Danger.Type.Shot, Player.Action.Right, 1, 0x6f));

	}

	public List<Timeline> GetTimelines()
	{
		return timelines;
	}

	public void PerformAction(Player.Action action)
	{
		Debug.Log("I wanna perform an action: " + action);
		foreach (var line in timelines) {
			if(line.player.state == Player.State.Idle){
				//line.player.state == Player.State.Intro;
				line.player.state = Player.State.Safe;
			}
		}
	}

	public void Update(float timeStep)
	{
		m_stepAccumulator += timeStep;
		m_stepAccumulator = Mathf.Min(m_stepAccumulator, m_config.secondsPerTick*2);
		if (m_stepAccumulator > m_config.secondsPerTick)
		{
			m_stepAccumulator -= timeStep;

			//Step logic
		}
	}
}

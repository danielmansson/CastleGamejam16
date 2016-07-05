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

		timelines.Add(new Timeline(0, 0));
		timelines.Add(new Timeline(0, 1));
		timelines.Add(new Timeline(0, 2));

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

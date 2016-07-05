using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModelConfig
{
	public float secondsPerTick = (1f / 5f);
}

public class GameModel 
{
	public enum Action
	{
		Left,
		Right
	}

	GameModelConfig m_config;
	float m_stepAccumulator;

	public GameModel(GameModelConfig config)
	{
		m_config = config;

		//Init with some nice data

	}

	public List<Timeline> GetTimelines()
	{
		return new List<Timeline>();
	}

	public void PerformAction(Action action)
	{
		Debug.Log("I wanna perform an action: " + action);
	}

	public void Update(float timeStep)
	{
		m_stepAccumulator += timeStep;
		if (m_stepAccumulator > m_config.secondsPerTick)
		{
			m_stepAccumulator -= timeStep;

			//Step logic
		}
	}
}

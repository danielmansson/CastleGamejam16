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
	int totalFrame = 0;

	public GameModel(GameModelConfig config)
	{
		m_config = config;

		timelines.Add(new Timeline(0, 0));
		timelines.Add(new Timeline(0, 1));
		timelines.Add(new Timeline(0, 2));

		List<List<Danger>> dangers = DangerGenerator.GenerateDangers(0, 15, 4);
		for(int i = 0; i < 3; i++){
			foreach (var danger in dangers[i]) {
				timelines[i].AddDangerToTimeline(danger);
			}
		}

		//tmp dummy data
		/*for (int i = 0; i < 8; i++)
		{
			var data = new DangerData()
			{
				type = Danger.Type.Monster,
				hp = 1,
				requiredAction = Random.Range(0, 2) == 0 ? Player.Action.Left : Player.Action.Right,
				timestamp = Random.Range(38, 200)
			};

			foreach (var tl in timelines)
			{
				tl.dangers.Add(new Danger(data));
			}
		}*/
	}

	public List<Timeline> GetTimelines()
	{
		return timelines;
	}

	public void PerformAction(Player.Action action)
	{
		Debug.Log("I wanna perform an action: " + action);

		foreach (var timeline in timelines)
		{
			timeline.TryToPerformAction(action);
		}
	}

	public void Update(float timeStep)
	{
		m_stepAccumulator += timeStep;
		m_stepAccumulator = Mathf.Min(m_stepAccumulator, m_config.secondsPerTick * 2);

		if (m_stepAccumulator > m_config.secondsPerTick)
		{
			m_stepAccumulator -= m_config.secondsPerTick;

			totalFrame++;
			//Step logic
			foreach (var timeline in timelines)
			{
				timeline.Step();
			}
		}
	}

	public float InterpolationT
	{
		get
		{
			return m_stepAccumulator / m_config.secondsPerTick;
		}
	}

	public float ExtrapolateSecondsLeft(int framesLeft)
	{
		return ((float)framesLeft - InterpolationT) * m_config.secondsPerTick;
	}
}

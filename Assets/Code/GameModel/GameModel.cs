using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModelConfig
{
	public int ticksPerSecond;
	public float secondsPerTick;
}

public class GameModel
{
	GameModelConfig m_config;
	List<Timeline> timelines = new List<Timeline>();
	float m_stepAccumulator;
	int totalFrame = 0;
	public event System.Action<Timeline, Danger> OnDeath;

	public bool GameOver { get; private set; }
	public int TotalFrame { get { return totalFrame; } }

	public GameModel(GameModelConfig config)
	{
		m_config = config;


		timelines.Add(new Timeline(new TimelineConfig()
		{
			id = 0,
			type = Timeline.Type.Shield,
			totalPlayerActionDuration = 4,
			playerActionDuration = 1
		}));

		timelines.Add(new Timeline(new TimelineConfig()
		{
			id = 1,
			type = Timeline.Type.Jumper,
			totalPlayerActionDuration = 17,
			playerActionDuration = 4
		}));

		timelines.Add(new Timeline(new TimelineConfig()
		{
			id = 2,
			type = Timeline.Type.Shooter,
			totalPlayerActionDuration = 5,
			playerActionDuration = 1,
			range = 100
		}));

		DangerGenerator.GenerateDangers(timelines, 50, 15, m_config.ticksPerSecond, 7);

		//tmp dummy data
		/*for (int i = 0; i < 8; i++)
		{
			var data = new DangerData()
			{
				type = Danger.Type.Monster,
				hp = 1,
				requiredAction = Random.Range(0, 2) == 0 ? Player.Action.Left : Player.Action.Right,
				timestamp = Random.Range(38, 300)
			};

			foreach (var tl in timelines)
			{
				if (tl.TimelineType != Timeline.Type.Jumper || Random.Range(0f, 1f) < 0.70f)
					tl.AddDangerToTimeline(new Danger(data));

				if (tl.TimelineType == Timeline.Type.Shield)
				{
					for (int j = 0; j < 2; j++)
					{
						if (Random.Range(0f, 1f) < 0.30f)
						{
							data.timestamp -= Random.Range(3, 4);
							tl.AddDangerToTimeline(new Danger(data));
						}
					}
				}
				if (tl.TimelineType == Timeline.Type.Shooter)
				{
					for (int j = 0; j < 1; j++)
					{
						if (Random.Range(0f, 1f) < 0.30f)
						{
							data.timestamp -= Random.Range(3, 10);
							tl.AddDangerToTimeline(new Danger(data));
						}
					}
				}
			}
		}*/

		foreach (var timeline in timelines)
		{
			timeline.OnPlayerDeath += (danger) => 
			{
				OnAnyDeathHandler(timeline, danger);
			};
		}
	}

	void OnAnyDeathHandler(Timeline timeline, Danger danger)
	{
		if (!GameOver)
		{
			GameOver = true;
			Debug.Log("Death");

			if (OnDeath != null)
			{
				OnDeath(timeline, danger);
			}
		}
	}

	public List<Timeline> GetTimelines()
	{
		return timelines;
	}

	public void PerformAction(Player.Action action)
	{
		if (GameOver)
			return;

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

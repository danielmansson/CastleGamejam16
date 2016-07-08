using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel
{
	List<Timeline> timelines = new List<Timeline>();
	float m_stepAccumulator;
	int totalFrame = 0;
	Stage m_stage;
	public event System.Action<Timeline, Danger> OnDeath;

	public bool GameOver { get; private set; }
	public int TotalFrame { get { return totalFrame; } }

	public GameModel()
	{
		m_stage = new Stage(0);

		AudioEvent.Play("Music");
		AudioEvent.Play("VoiceReadyGo");

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
			playerActionDuration = 9
		}));

		timelines.Add(new Timeline(new TimelineConfig()
		{
			id = 2,
			type = Timeline.Type.Shooter,
			totalPlayerActionDuration = 5,
			playerActionDuration = 1,
			range = 32
		}));

		DangerGenerator.GenerateDangers(timelines, 50, m_stage);

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

			//Time.timeScale = 0f;

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
		m_stepAccumulator = Mathf.Min(m_stepAccumulator, m_stage.secondsPerTick * 2);

		if (m_stepAccumulator > m_stage.secondsPerTick)
		{
			m_stepAccumulator -= m_stage.secondsPerTick;
			totalFrame++;
			//Step logic
			foreach (var timeline in timelines)
			{
				timeline.Step();
			}

			if(timelines[0].StageComplete()
				&& timelines[1].StageComplete()
				&& timelines[2].StageComplete()
				&& !GameOver)
			{
				m_stage = new Stage(m_stage.id+1);
				DangerGenerator.GenerateDangers(timelines, totalFrame+50, m_stage);
				foreach (var timeline in timelines)
				{
					timeline.OnPlayerDeath += (danger) => 
					{
						OnAnyDeathHandler(timeline, danger);
					};
				}

				if(m_stage.musicParameter < 0.9f){
					AudioEvent.Play("VoiceLevelUp");
				} else {
					AudioEvent.Play("VoiceSpeedUp");
				}
				AudioEvent.ChangeParameter("Music", "LVL", m_stage.musicParameter);
			}
		}
	}

	public float InterpolationT
	{
		get
		{
			return m_stepAccumulator / m_stage.secondsPerTick;
		}
	}

	public float ExtrapolateSecondsLeft(int framesLeft)
	{
		return ((float)framesLeft - InterpolationT) * m_stage.secondsPerTick;
	}

	public float ExtrapolateActionSecondsLeft(int framesLeft)
	{
		return ((float)framesLeft + (1f - InterpolationT)) * m_stage.secondsPerTick;
	}
}

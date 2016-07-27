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
	public float GameOverTimestamp { get; private set; }
	public int TotalFrame { get { return totalFrame; } }

	bool m_shouldPlayJumpSoundSoon = false;
	float m_jumpSoundTimer = 4.3f;

	public GameModel()
	{
		Player.s_shouldPlayJumpSound = false;
		//AudioEvent.ChangeParameter("Jump", "Portal", 0f);

		m_stage = new Stage(0);

		//AudioEvent.Play("Music"); already playing
		AudioEvent.Play("VoiceReadyGo");
		AudioEvent.ChangeParameter("Music", "GameOver", 0);

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
			range = 70
		}));

		DangerGenerator.GenerateDangers(timelines, 50, m_stage);
		//generate dangers at fix positions until duration ends

		foreach (var timeline in timelines)
		{
			var capturedTimeline = timeline;
			timeline.OnPlayerDeath += (danger) => 
			{
				OnAnyDeathHandler(capturedTimeline, danger);
			};
		}
	}

	void OnAnyDeathHandler(Timeline timeline, Danger danger)
	{
		if (!GameOver)
		{
			Systems.Instance.GameInfo.score = timelines[0].score + timelines[1].score + timelines[2].score;

			int highscore = 0;
			if (PlayerPrefs.HasKey("highscore"))
				highscore = PlayerPrefs.GetInt("highscore");

			int score = Systems.Instance.GameInfo.score * 100;
			if (score > highscore)
				PlayerPrefs.SetInt("highscore", score);

			GameOver = true;
			Debug.Log("Death");
			AudioEvent.ChangeParameter("Music", "GameOver", 1);
			EventManager.Instance.SendEvent<EventChangeText>(new EventChangeText("GameOver"));
			GameOverTimestamp = Time.unscaledTime;

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

		//Debug.Log("I wanna perform an action: " + action);

		foreach (var timeline in timelines)
		{
			timeline.TryToPerformAction(action);
		}
	}

	public void Update(float timeStep)
	{
		m_stepAccumulator += timeStep;
		m_stepAccumulator = Mathf.Min(m_stepAccumulator, m_stage.secondsPerTick * 6);

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

				if(m_stage.id == 1){

					m_shouldPlayJumpSoundSoon = true;
					EventManager.Instance.SendEvent<EventChangeText>(new EventChangeText("Portal1"));
				} else if(m_stage.id == 2){
					EventManager.Instance.SendEvent<EventChangeText>(new EventChangeText("Portal2"));
				}

				if(m_stage.musicParameter < 0.9f){
					AudioEvent.Play("VoiceLevelUp");
					EventManager.Instance.SendEvent<EventChangeText>(new EventChangeText("LevelUp"));
				} else {
					AudioEvent.Play("VoiceSpeedUp");
					EventManager.Instance.SendEvent<EventChangeText>(new EventChangeText("SpeedUp"));
				}
				AudioEvent.ChangeParameter("Music", "LVL", m_stage.musicParameter);
			}
		}

		if (m_shouldPlayJumpSoundSoon)
		{
			m_jumpSoundTimer -= Time.deltaTime;
			if (m_jumpSoundTimer < 0f)
			{
				Debug.Log("Gonna play some jump sound");
				Player.s_shouldPlayJumpSound = true;
				m_shouldPlayJumpSoundSoon = false;
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

	public float GetSpeedFactor()
	{
		return 1f + ((m_stage.id) / 3) * 0.03f;
	}
}

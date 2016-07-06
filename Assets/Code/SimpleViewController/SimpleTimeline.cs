using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleTimeline : MonoBehaviour
{
	[SerializeField]
	SimpleDanger m_dangerPrefab;
	[SerializeField]
	float m_width;
	[SerializeField]
	GameObject m_horizontalLine;
	[SerializeField]
	SimpleDanger m_player;

	List<SimpleDanger> m_activeDangers = new List<SimpleDanger>();
	Timeline m_timeline;
	GameModel m_model;

	public void Init(GameModel model, Timeline timeline)
	{
		m_model = model;
		m_timeline = timeline;

		foreach (var danger in m_timeline.m_dangers)
		{
			var dangerView = (SimpleDanger)Instantiate(m_dangerPrefab, transform.position, Quaternion.identity);
			dangerView.transform.parent = transform;
			dangerView.SetActionType(danger.requiredAction == Player.Action.Left);
			dangerView.Init(danger);
			dangerView.OnDestroy += ViewDestroyedHandler;

			m_activeDangers.Add(dangerView);
		}
	}

	void ViewDestroyedHandler(SimpleDanger dangerView)
	{
		m_activeDangers.Remove(dangerView);
	}

	void Start ()
	{
		m_horizontalLine.transform.localScale = new Vector3(m_width, 0.1f, 1f);
		m_horizontalLine.transform.localPosition = Vector3.right * m_width * 0.5f;
	}
	
	void Update ()
	{
		m_player.SetPlayerState(m_timeline.m_player.m_action, m_timeline.m_player.state);

		foreach (var danger in m_activeDangers)
		{
			int d = danger.Danger.distanceLeft;

			float speed = 1f;

			if (m_timeline.TimelineType == Timeline.Type.Shield)
			{
				speed = 6f;
			}
			else if (m_timeline.TimelineType == Timeline.Type.Jumper)
			{
				speed = 4f;
			}
			else if (m_timeline.TimelineType == Timeline.Type.Shooter)
			{
				speed = 2f;

				if (d <= m_timeline.Config.range)
					danger.transform.localScale = Vector3.one;
				else
					danger.transform.localScale = new Vector3(1f, 0.4f, 1f);
			}

			danger.transform.localPosition = Vector3.right * m_model.ExtrapolateSecondsLeft(d) * speed;
		}
	}
}

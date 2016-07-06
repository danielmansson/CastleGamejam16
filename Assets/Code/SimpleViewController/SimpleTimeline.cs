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
			dangerView.Danger = danger;

			m_activeDangers.Add(dangerView);
		}
	}

	void Start ()
	{
		m_horizontalLine.transform.localScale = new Vector3(m_width, 0.1f, 1f);
		m_horizontalLine.transform.localPosition = Vector3.right * m_width * 0.5f;
	}
	
	void Update ()
	{
		m_player.SetPlayerState(m_timeline.player.m_action, m_timeline.player.state);

		foreach (var danger in m_activeDangers)
		{
			int d = danger.Danger.distanceLeft;
			if (d > 0)
			{
				danger.transform.localPosition = Vector3.right * m_model.ExtrapolateSecondsLeft(d) * 1.8f;
			}
			else
			{
				danger.gameObject.SetActive(false);
			}
		}
	}
}

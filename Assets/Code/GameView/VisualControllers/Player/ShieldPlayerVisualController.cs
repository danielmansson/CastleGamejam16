using UnityEngine;
using System.Collections;

public class ShieldPlayerVisualController : PlayerVisualController
{
	[SerializeField]
	PlayerAnimationController m_animation;

	PlayerView m_playerView;
	float m_totalDuration;
	Player.Action m_lastAction;

	public override void Init(PlayerView playerView)
	{
		m_playerView = playerView;

		m_playerView.Timeline.m_player.OnPerformAction += OnPlayerActionStarted;
		m_playerView.Timeline.m_player.OnEndAction += OnPlayerActionEnded;

		m_lastAction = m_playerView.Timeline.m_player.m_action;
	}

	void OnPlayerActionStarted(int actionDuration)
	{
		if (m_lastAction != m_playerView.Timeline.m_player.m_action)
		{
			m_totalDuration = m_playerView.Model.ExtrapolateSecondsLeft(actionDuration);

			if (m_playerView.Timeline.m_player.m_action == Player.Action.Left)
				m_animation.transform.localScale = new Vector3(-1f, 1f, 1f);
			else
				m_animation.transform.localScale = new Vector3(1f, 1f, 1f);

			m_totalDuration = m_playerView.Model.ExtrapolateSecondsLeft(actionDuration);
			m_animation.TriggerAction(m_playerView.Timeline.m_player.m_action == Player.Action.Left);
			m_lastAction = m_playerView.Timeline.m_player.m_action;
		}
	}

	void OnPlayerActionEnded()
	{

	}
}

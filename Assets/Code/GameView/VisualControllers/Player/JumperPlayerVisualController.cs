using UnityEngine;
using System.Collections;

public class JumperPlayerVisualController : PlayerVisualController
{
	[SerializeField]
	PlayerAnimationController m_animation;

	PlayerView m_playerView;
	float m_totalDuration;

	public override void Init(PlayerView playerView)
	{
		m_playerView = playerView;

		m_playerView.Timeline.m_player.OnPerformAction += OnPlayerActionStarted;
		m_playerView.Timeline.m_player.OnEndAction += OnPlayerActionEnded;
	}

	void OnPlayerActionStarted(int actionDuration)
	{
		m_totalDuration = m_playerView.Model.ExtrapolateSecondsLeft(actionDuration);
		m_animation.TriggerAction(m_playerView.Timeline.m_player.m_action == Player.Action.Left);
	}

	void OnPlayerActionEnded()
	{

	}
}

using UnityEngine;
using System.Collections;

public class ShooterPlayerVisualController : PlayerVisualController
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

		m_playerView.Model.OnDeath += OnDeath;
	}

	private void OnDeath(Timeline timeline, Danger danger)
	{
		m_animation.SetTrigger("death");
	}

	void OnPlayerActionStarted(int actionDuration)
	{
		if (m_playerView.Timeline.m_player.m_action == Player.Action.Left)
			m_animation.transform.localScale = new Vector3(-1f, 1f, 1f);
		else
			m_animation.transform.localScale = new Vector3(1f, 1f, 1f);

		m_totalDuration = m_playerView.Model.ExtrapolateSecondsLeft(actionDuration);
		m_animation.TriggerAction(m_playerView.Timeline.m_player.m_action == Player.Action.Left);
	}

	void OnPlayerActionEnded()
	{

	}
}

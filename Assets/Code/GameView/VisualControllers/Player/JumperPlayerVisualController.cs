using UnityEngine;
using System.Collections;

public class JumperPlayerVisualController : PlayerVisualController
{
	[SerializeField]
	PlayerAnimationController m_animation;

	[SerializeField]
	AnimationCurve m_jumpCurve;
	[SerializeField]
	float m_jumpHeight;
	[SerializeField]
	AnimationCurve m_duckCurve;
	[SerializeField]
	float m_duckHeight;

	PlayerView m_playerView;
	float m_totalDuration;
	bool m_duringAction;
	float m_timer;

	AnimationCurve m_currentCurve;
	float m_currentHeight;

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
		m_totalDuration = m_playerView.Model.ExtrapolateActionSecondsLeft(actionDuration + 1);
		m_animation.TriggerAction(m_playerView.Timeline.m_player.m_action == Player.Action.Left);
		m_duringAction = true;
		m_timer = 0f;

		if (m_playerView.Timeline.m_player.m_action == Player.Action.Left)
		{
			m_currentCurve = m_duckCurve;
			m_currentHeight = m_duckHeight;
		}
		else
		{
			m_currentCurve = m_jumpCurve;
			m_currentHeight = m_jumpHeight;
		}
	}

	void OnPlayerActionEnded()
	{
	}

	void Update()
	{
		if (m_duringAction)
		{
			m_timer += Time.deltaTime;

			float t = Mathf.InverseLerp(0f, m_totalDuration, m_timer);
			float height = m_currentCurve.Evaluate(t) * m_currentHeight;

			m_animation.transform.localPosition = Vector3.up * height;

			if (m_timer > m_totalDuration - 0.1f)
			{
				m_animation.EndAction();
			}

			if (m_timer > m_totalDuration)
			{
				m_duringAction = false;
			}
		}
	}
}

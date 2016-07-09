using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System;

public class ShooterPlayerVisualController : PlayerVisualController
{
	[SerializeField]
	PlayerAnimationController m_animation;

	[SerializeField]
	PlasmaVisual m_plasmaPrefab;

	PlayerView m_playerView;
	float m_totalDuration;

	PlasmaVisual m_leftShot = null;
	PlasmaVisual m_rightShot = null;

	public override void Init(PlayerView playerView)
	{
		m_playerView = playerView;

		m_playerView.Timeline.m_player.OnPerformAction += OnPlayerActionStarted;
		m_playerView.Timeline.m_player.OnEndAction += OnPlayerActionEnded;

		m_playerView.Model.OnDeath += OnDeath;

		EventManager.Instance.RegisterEvent<RobotRequestingShotEventArgs>(OnShotRequest);
	}

	void OnDestroy()
	{
		EventManager.Instance.UnregisterEvent<RobotRequestingShotEventArgs>(OnShotRequest);
	}

	private void OnDeath(Timeline timeline, Danger danger)
	{
		m_animation.SetTrigger("death");
	}

	void OnPlayerActionStarted(int actionDuration)
	{
		bool left = m_playerView.Timeline.m_player.m_action == Player.Action.Left;

		if (left)
			m_animation.transform.localScale = new Vector3(-1f, 1f, 1f);
		else
			m_animation.transform.localScale = new Vector3(1f, 1f, 1f);

		m_totalDuration = m_playerView.Model.ExtrapolateSecondsLeft(actionDuration);
		m_animation.TriggerAction(left);

		var plasma = (PlasmaVisual)Instantiate(m_plasmaPrefab, transform.position, Quaternion.identity);
		plasma.Dir = left ? Vector3.left : Vector3.right;
		plasma.StartPos = transform.position;

		if (left)
			m_leftShot = plasma;
		else
			m_rightShot = plasma;
	}

	private void OnShotRequest(RobotRequestingShotEventArgs args)
	{
		PlasmaVisual plasma = null;

		if (args.ReqAction == Player.Action.Left)
		{
			if (m_leftShot != null)
			{
				plasma = m_leftShot;
				m_leftShot = null;
			}
		}
		else
		{
			if (m_rightShot != null)
			{
				plasma = m_rightShot;
				m_rightShot = null;
			}
		}

		if (plasma != null)
		{
			plasma.TimeToImpact = args.Time;
			plasma.Target = args.Robot.Root.transform;
		}
	}

	void OnPlayerActionEnded()
	{

	}
}

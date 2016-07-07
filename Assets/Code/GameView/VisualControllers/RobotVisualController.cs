using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotVisualController : DangerVisualController
{
	[SerializeField]
	GameObject m_root;
	[SerializeField]
	GameObject m_right;
	[SerializeField]
	GameObject m_left;

	DangerView m_dangerView;

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;
		dangerView.OnDestroy += OnModelDangerDestroyed;

		m_right.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Right);
		m_left.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Left);
	}

	void Start ()
	{
	
	}
	
	void Update ()
	{
		float t = m_dangerView.GetExtrapolatedSecondsToImpact();

		m_root.transform.localPosition = Vector3.right * t * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 50f;
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		dangerView.DestroyView();
	}
}

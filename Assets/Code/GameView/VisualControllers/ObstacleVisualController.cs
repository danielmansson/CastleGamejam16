using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleVisualController : DangerVisualController
{
	[SerializeField]
	GameObject m_truck;
	[SerializeField]
	GameObject m_ball;
	[SerializeField]
	GameObject m_root;

	DangerView m_dangerView;

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;
		dangerView.OnDestroy += OnModelDangerDestroyed;

		m_truck.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Left);
		m_ball.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Right);
	}

	void Start()
	{

	}

	void Update()
	{
		float t = m_dangerView.GetExtrapolatedSecondsToImpact();

		m_root.transform.localPosition = Vector3.right * t * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 80f;
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		dangerView.DestroyView();
	}
}

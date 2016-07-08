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
	float m_timer;

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;
		dangerView.OnDestroy += OnModelDangerDestroyed;

		m_truck.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Right);
		m_ball.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Left);

		m_timer = m_dangerView.GetExtrapolatedSecondsToImpact();

		RefreshTransform();
	}

	void Start()
	{

	}

	void Update()
	{
		m_timer -= Time.deltaTime;
		RefreshTransform();
	}

	void RefreshTransform()
	{
		m_root.transform.localPosition = Vector3.right * m_timer * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 80f;
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		StartCoroutine(DestroySoon(6f));
	}

	IEnumerator DestroySoon(float t)
	{
		yield return new WaitForSeconds(t);
		m_dangerView.DestroyView();
	}
}

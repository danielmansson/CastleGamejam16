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
	float m_timer;

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;
		dangerView.OnDestroy += OnModelDangerDestroyed;

		m_right.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Right);
		m_left.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Left);

		m_timer = m_dangerView.GetExtrapolatedSecondsToImpact(); 
		RefreshTransform();
	}

	void Start ()
	{
	
	}

	void RefreshTransform()
	{
		float t = m_timer;

		m_root.transform.localPosition = Vector3.right * t * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 50f;
	}
	
	void Update ()
	{
		m_timer -= Time.deltaTime;
		RefreshTransform();
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		StartCoroutine(DestroySoon(0f));
	}

	IEnumerator DestroySoon(float t)
	{
		yield return new WaitForSeconds(t);
		m_dangerView.DestroyView();
		AudioEvent.Play("EnemyHit");
	}
}

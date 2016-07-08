using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowVisualController : DangerVisualController
{
	[SerializeField]
	GameObject m_arrow;

	DangerView m_dangerView;

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;

		dangerView.OnDestroy += OnModelDangerDestroyed;

		if (m_dangerView.Danger.requiredAction == Player.Action.Left)
		{
			m_arrow.transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		RefreshTransform();
	}

	void Start()
	{

	}

	void Update()
	{
		RefreshTransform();
	}

	void RefreshTransform()
	{
		float t = m_dangerView.GetExtrapolatedSecondsToImpact();
		m_arrow.transform.localPosition = Vector3.right * t * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 120f;
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		dangerView.DestroyView();
		AudioEvent.Play("PlayerShield");
	}
}

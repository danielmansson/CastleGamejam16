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
	}

	void Start()
	{

	}

	void Update()
	{
		float t = m_dangerView.GetExtrapolatedSecondsToImpact();


	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		dangerView.DestroyView();
	}
}

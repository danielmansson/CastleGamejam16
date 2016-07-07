using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowVisualController : DangerVisualController
{
	public override void Init(DangerView dangerView)
	{
		dangerView.OnDestroy += OnModelDangerDestroyed;
	}

	void Start()
	{

	}

	void Update()
	{

	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		dangerView.DestroyView();
	}
}

﻿using UnityEngine;
using System.Collections;

public class DestroyInTime : MonoBehaviour
{
	public float time;

	void Start()
	{
		Destroy(this.gameObject, time);
	}
}

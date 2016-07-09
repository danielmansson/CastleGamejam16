using UnityEngine;
using System.Collections;

public class SetRenderQueue : MonoBehaviour
{
	public int queue = 3000;
	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().material.renderQueue = queue;
	}
	
	// Update is called once per frame
	void Update () {
	//	GetComponent<MeshRenderer>().material.renderQueue = queue;
	}
}

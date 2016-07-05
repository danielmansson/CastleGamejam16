using UnityEngine;
using System.Collections;

public class SimpleGameView : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	GameModelDummy m_model;

	void Awake ()
	{
		m_modelMgr.OnModelInitialized += Init;
	}

	void Init()
	{
		m_model = m_modelMgr.Model;

		//Set up view
	}
}

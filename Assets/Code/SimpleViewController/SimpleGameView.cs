using UnityEngine;
using System.Collections;
using System.Linq;

public class SimpleGameView : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	GameModel m_model;

	void Awake ()
	{
		m_modelMgr.OnModelInitialized += Init;
	}

	void Init()
	{
		m_model = m_modelMgr.Model;

		//Set up view

		var timeline = m_model.GetTimelines().FirstOrDefault();

		//timeline.
	}
}

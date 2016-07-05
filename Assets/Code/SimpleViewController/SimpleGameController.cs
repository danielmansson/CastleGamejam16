using UnityEngine;
using System.Collections;

public class SimpleGameController : MonoBehaviour
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

		//Set up controller
	}

	void Update()
	{
		if (m_model != null)
		{
			if (Input.GetKeyDown(KeyCode.A))
			{
				m_model.PerformAction(GameModel.Action.Left);
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				m_model.PerformAction(GameModel.Action.Right);
			}
		}
	}
}

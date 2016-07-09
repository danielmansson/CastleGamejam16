using UnityEngine;
using System.Collections;

public class SimpleGameController : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	GameModel m_model;

    //Float for Parameters in FMOD
	public float musicParameter = 0;
	bool flag = false;


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
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				m_model.PerformAction(Player.Action.Left);
            }

			else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
			{
				m_model.PerformAction(Player.Action.Right);
			}

			else if(Input.GetKeyDown(KeyCode.Space))
			{
				if(m_model.GameOver){
					Systems.Instance.State.QueueState(State.Start);
				}
			}

			else if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				Debug.Log("Trying to show speedup text");
				EventManager.Instance.SendEvent<EventChangeText>(new EventChangeText("SpeedUp"));
			}

			if (Input.GetMouseButtonDown(0))
			{
				var vp = Camera.main.ScreenToViewportPoint(Input.mousePosition);
				if (vp.x < 0.5f)
				{
					m_model.PerformAction(Player.Action.Left);
				}
				else
				{
					m_model.PerformAction(Player.Action.Right);
				}

				if (m_model.GameOver && Time.unscaledTime > m_model.GameOverTimestamp + 2f)
				{
					Systems.Instance.State.QueueState(State.Start);
				}
			}
        }
    }
}

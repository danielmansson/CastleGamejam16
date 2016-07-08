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

			else if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				AudioEvent.Play("EnemyHit");
			}

            /*
            //Change to when player makes short combo
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioEvent.Play("Combo1");
            }
            //Change to when player makes medium combo
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioEvent.Play("Combo2");
            }
            //Change to when player makes long combo
            if (Input.GetKeyDown(KeyCode.T))
            {
                AudioEvent.Play("Combo3");
            }
            */

            //Change to when player successfully jumps over/slides under obstacle
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                AudioEvent.Play("JumpPoint");
            }



        }
    }
}

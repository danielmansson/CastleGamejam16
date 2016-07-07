using UnityEngine;
using System.Collections;

public class SimpleGameController : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	GameModel m_model;

    public float musicParameter = 0;


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
				m_model.PerformAction(Player.Action.Left);

                AudioEvent.Play("PressedA");
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				m_model.PerformAction(Player.Action.Right);

                AudioEvent.Play("PressedD");
            }

            //Change this to whenever the speed/level is going up
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (musicParameter < 2.0)
                {
                    musicParameter++;
                }
                else if (musicParameter == 2.0)
                {
                    musicParameter = 0.5f;
                }
                else if (musicParameter == 2.5)
                {
                    musicParameter = 0.0f;
                }
                print(musicParameter);
                AudioEvent.ChangeParameter("PressedA", "LVL", musicParameter);
            }

            //Change to when player is hit (if the player will have health)
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioEvent.Play("PlayerHit");
            }

           }
	}
}

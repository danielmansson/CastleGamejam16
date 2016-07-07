using UnityEngine;
using System.Collections;

public class SimpleGameController : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	GameModel m_model;

    //Float for Parameters in FMOD
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

            //Test
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
			else if(Input.GetKeyDown(KeyCode.Keypad1))
			{
				AudioEvent.Play("ReadyGo");
			}

            //Change this to whenever the speed/level is going up
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (musicParameter < 2.0)
                {
                    musicParameter++;
                    AudioEvent.Play("SpeedUp");
                }
                else if (musicParameter == 2.0)
                {
                    musicParameter = 0.5f;
                    AudioEvent.Play("LevelUp");
                }
                else if (musicParameter == 2.5)
                {
                    musicParameter = 0.0f;
                    AudioEvent.Play("LevelUp");
                }
                print(musicParameter);
                AudioEvent.ChangeParameter("PressedA", "LVL", musicParameter);
            }


            //Implement all this stuff plzz <3
            /*
            
            
            //Change to when player is hit (if the player will have health)
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioEvent.Play("PlayerHit");
            }

            //Change to when player blocks an arrow
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioEvent.Play("ArrowBlock");
            }

            //Change to when player jumps
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioEvent.Play("PlayerJump");
            }

            //Change to when player shoots. Pew pew.
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioEvent.Play("PlayerShoot");
            }

            //Change to when player hits an enemy with projectile.
            if (Input.GetKeyDown(KeyCode.F))
            {
                AudioEvent.Play("EnemyHit");
            }

            //Change to when game starts (After title screen(?)!)
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioEvent.Play("GameStart");
                AudioEvent.Play("ReadyGo");
            }

            //Change to when title screen(?) appears
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioEvent.Play("TitleScreen");
            }

            //Change to when player dies
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioEvent.Play("GameOver");
                AudioEvent.ChangeParameter("PressedA", "GameOver", 1);
            }

            //Change to when player dies
            if (Input.GetKeyDown(KeyCode.G))
            {
                AudioEvent.ChangeParameter("PressedA", "GameOver", 0);
            }
            */
            //Change to when player dies
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioEvent.Play("GameOver");
                AudioEvent.ChangeParameter("PressedA", "GameOver", 1);
            }

            //Change to when player restarts from GameOverScreen
            if (Input.GetKeyDown(KeyCode.G))
            {
                musicParameter = 0.0f;
                AudioEvent.ChangeParameter("PressedA", "GameOver", 0);
                AudioEvent.ChangeParameter("PressedA", "LVL", musicParameter);
                AudioEvent.Play("ReadyGo");
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                AudioEvent.Play("JumpPoint");
            }



        }
    }
}

using UnityEngine;
using System.Collections;

public class Player {
	public enum Action
	{
		Left,
		Right
	}
	public enum State
	{
		Idle,
		Intro,
		Safe,
		Outro
	}

	public int id;
	public int timestamp;
	public Player.State state;
	public Player.Action m_action;

	public System.Action<int> OnPerformAction;
	public System.Action OnEndAction;

	int m_actionDuration = 4;
	int m_timer;

	public static bool s_shouldPlayJumpSound = false;

	public Player(int id, int actionDuration)
	{
		m_actionDuration = actionDuration;
		this.id = id;
		timestamp = 0;
		state = Player.State.Idle;
	}

	public void TryToPerformAction(Action action)
	{
		if (state == State.Idle)
		{
			m_action = action;
			state = Player.State.Safe;
			m_timer = 0;
			if(id == 2)
			{
				AudioEvent.Play("PlayerShoot");
			}
			if (id == 1 && s_shouldPlayJumpSound)
			{
				AudioEvent.Play("PlayerJump");
			}

			if (OnPerformAction != null)
			{
				OnPerformAction(m_actionDuration);
			}
		}
	}

	public void Step()
	{
		if (state == State.Idle)
		{
			//Nothing
		}
		else if (state == State.Safe)
		{
			if (m_timer > m_actionDuration)
			{
				m_timer = 0;
				state = Player.State.Idle;

				if (OnEndAction != null)
				{
					OnEndAction();
				}
			}
			else
			{
				m_timer++;
			}
		}
	}

	public bool AtFirstFrameOfAction()
	{
		return state == State.Safe && m_timer == 1;
	}
}

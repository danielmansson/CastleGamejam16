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
		Outro,
		SafeLeft,
		SafeRight
	}

	public int id;
	public int timestamp;
	public Player.State state;
	public Player.Action m_action;

	//TODO: config per timeline
	const int m_introFrames = 1;
	const int m_safeFrames = 8;
	const int m_outroFrames = 1;

	int m_timer;

	public Player(int id)
	{
		this.id = id;
		timestamp = 0;
		state = Player.State.Idle;
	}

	public void TryToPerformAction(Action action)
	{
		if (state == State.Idle)
		{
			m_action = action;
			state = Player.State.Intro;
			m_timer = 0;
		}
	}

	public void Step()
	{
		//improve this 

		if (state == State.Idle)
		{
			//Nothing
		}
		else if(state == State.Intro)
		{
			if (m_timer > m_introFrames)
			{
				m_timer = 0;
				state = Player.State.Safe;
			}
			else
			{
				m_timer++;
			}
		}
		else if (state == State.Safe)
		{
			if (m_timer > m_safeFrames)
			{
				m_timer = 0;
				state = Player.State.Outro;
			}
			else
			{
				m_timer++;
			}
		}
		else if(state == State.Outro)
		{
			if (m_timer > m_outroFrames)
			{
				m_timer = 0;
				state = Player.State.Idle;
			}
			else
			{
				m_timer++;
			}
		}
	}
}

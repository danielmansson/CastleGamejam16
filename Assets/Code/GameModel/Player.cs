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

	//TODO: config per timeline
	const int m_actionFrames = 4;

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
			state = Player.State.Safe;
			m_timer = 0;
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
			if (m_timer > m_actionFrames)
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

	public bool AtFirstFrameOfAction()
	{
		return state == State.Safe && m_timer == 1;
	}
}

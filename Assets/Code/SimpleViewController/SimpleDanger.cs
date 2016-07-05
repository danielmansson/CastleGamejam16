using UnityEngine;
using System.Collections;

public class SimpleDanger : MonoBehaviour
{
	[SerializeField]
	GameObject m_up;
	[SerializeField]
	GameObject m_down;

	public Danger Danger { get; set; }

	public void SetActionType(bool up)
	{
		m_up.SetActive(up);
		m_down.SetActive(!up);
	}

	public void SetPlayerState(Player.Action action, Player.State state)
	{
		if (state != Player.State.Idle)
		{
			bool up = action == Player.Action.Left;

			m_up.SetActive(up);
			m_down.SetActive(!up);

			if (state == Player.State.Safe)
				transform.localScale = Vector3.one;
			else
				transform.localScale = new Vector3(1f, 0.4f, 1f);
		}
		else
		{
			m_up.SetActive(false);
			m_down.SetActive(false);
		}
	}
}

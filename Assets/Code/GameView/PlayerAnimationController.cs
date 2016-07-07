using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField]
	Animator m_animator;

	bool m_actionLastFrame = false;

	public void TriggerAction(bool dirIsLeft)
	{
		m_animator.SetBool("dirIsLeft", dirIsLeft);
		m_animator.SetBool("action", true);
		m_actionLastFrame = true;
	}

	void LateUpdate()
	{
		m_animator.SetBool("action", m_actionLastFrame);
		m_actionLastFrame = false;
	}
}

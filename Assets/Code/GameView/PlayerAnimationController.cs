using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField]
	Animator m_animator;

	public void TriggerAction(bool dirIsLeft)
	{
		m_animator.SetBool("dirIsLeft", dirIsLeft);
		m_animator.SetBool("duringAction", true);
		m_animator.SetTrigger("action");
	}

	public void EndAction()
	{
		m_animator.SetBool("duringAction", false);
	}

	public void SetTrigger(string trigger)
	{
		m_animator.SetTrigger(trigger);
	}
}

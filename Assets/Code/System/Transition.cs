using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour
{
	[SerializeField] private GameObject m_art;
	[SerializeField] private AnimationCurve m_curve;
	[SerializeField] private float m_durationIn = 0.5f;
	[SerializeField] private float m_durationOut = 0.5f;

	public static bool s_firstStartOfSession = true;

	void Awake()
	{
		m_art.SetActive(false);
	}

	public IEnumerator In()
	{
		if (s_firstStartOfSession)
		{
			s_firstStartOfSession = false;
		}
		else
		{
			AudioEvent.Play("MenuStart");
		}

		m_art.SetActive(true);
		float t = 0;
		while (t < 1)
		{
			t = Mathf.Clamp01(t + Time.deltaTime / m_durationIn);
			float anim = m_curve.Evaluate(t);
			m_art.transform.position = new Vector2((1 - anim) * 25, 0);
			yield return null;
		}
	}

	public IEnumerator Out()
	{
		float t = 0;
		while (t < 1)
		{
			t = Mathf.Clamp01(t + Time.deltaTime / m_durationOut);
			float anim = m_curve.Evaluate(t);
			m_art.transform.position = new Vector2(anim * -25, 0);
			yield return null;
		}
		m_art.SetActive(false);
	}

	[ContextMenu("In")]
	void TestIn()
	{
		StartCoroutine(In());
	}

	[ContextMenu("Out")]
	void TestOut()
	{
		StartCoroutine(Out());
	}
}

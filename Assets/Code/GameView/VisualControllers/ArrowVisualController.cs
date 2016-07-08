using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowVisualController : DangerVisualController
{
	[SerializeField]
	GameObject m_arrow;

	[SerializeField]
	GameObject m_arrowSprite;

	[SerializeField]
	AnimationCurve m_bounceY;

	[SerializeField]
	float m_bounceSpeedX;
	[SerializeField]
	float m_bounceHeightY;

	DangerView m_dangerView;
	bool m_playingDestroy = false;

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;

		dangerView.OnDestroy += OnModelDangerDestroyed;

		if (m_dangerView.Danger.requiredAction == Player.Action.Left)
		{
			m_arrow.transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		RefreshTransform();
	}

	void Start()
	{

	}

	void Update()
	{
		RefreshTransform();
	}

	void RefreshTransform()
	{
		float t = m_dangerView.GetExtrapolatedSecondsToImpact();
		if (!m_playingDestroy)
		{
			m_arrow.transform.localPosition = Vector3.right * t * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 80f;
		}
		else if (t < -10)
		{
			m_dangerView.DestroyView();
		}
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		if (!dangerView.Model.GameOver)
		{
			m_playingDestroy = true;
			AudioEvent.Play("PlayerShield");
			StartCoroutine(DestroySequence());
			EventManager.Instance.SendEvent(new BlockedArrowEventArgs());
		}
	}

	IEnumerator DestroySequence()
	{
		float timer = 0f;
		Vector3 start = m_arrowSprite.transform.localPosition;
		Vector3 pos = m_arrowSprite.transform.localPosition;
		while (timer < 3f)
		{
			timer += Time.deltaTime;
			m_arrowSprite.transform.localRotation = Quaternion.Euler(0, 0, timer*1000);

			pos.x = timer * m_bounceSpeedX;
			pos.y = m_bounceY.Evaluate(timer / 3f) * m_bounceHeightY;

			m_arrowSprite.transform.localPosition = start + pos;
			yield return null;
		}

		m_dangerView.DestroyView();
	}
}

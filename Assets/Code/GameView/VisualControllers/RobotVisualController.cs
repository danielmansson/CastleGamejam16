using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotRequestingShotEventArgs : EventArgs
{
	public RobotVisualController Robot { get; private set; }
	public float Time { get; private set; }
	public Player.Action ReqAction { get; private set; }

	public RobotRequestingShotEventArgs(RobotVisualController robot, Player.Action reqAction, float time)
	{
		Robot = robot;
		Time = time;
		ReqAction = reqAction;
	}
}

public class RobotVisualController : DangerVisualController
{
	[SerializeField]
	GameObject m_root;
	[SerializeField]
	GameObject m_right;
	[SerializeField]
	GameObject m_left;
	float m_shotSpeed = 7;

	DangerView m_dangerView;
	float m_timer;

	public GameObject Root { get { return m_root; } }

	public override void Init(DangerView dangerView)
	{
		m_dangerView = dangerView;
		dangerView.OnDestroy += OnModelDangerDestroyed;

		m_right.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Right);
		m_left.SetActive(m_dangerView.Danger.requiredAction == Player.Action.Left);

		m_timer = m_dangerView.GetExtrapolatedSecondsToImpact(); 
		RefreshTransform();
	}

	void Start ()
	{
	
	}

	void RefreshTransform()
	{
		float t = m_timer;

		m_root.transform.localPosition = Vector3.right * t * (m_dangerView.Danger.requiredAction == Player.Action.Left ? -1f : 1f) * 30f;
	}
	
	void Update ()
	{
		m_timer -= Time.deltaTime;
		RefreshTransform();
	}

	void OnModelDangerDestroyed(DangerView dangerView)
	{
		float time = Mathf.Abs(m_timer) / m_shotSpeed;
		StartCoroutine(DestroySoon(time));

		EventManager.Instance.SendEvent(new RobotRequestingShotEventArgs(this, m_dangerView.Danger.requiredAction, time));
	}

	IEnumerator DestroySoon(float t)
	{
		yield return new WaitForSeconds(t);
		m_dangerView.DestroyView();
	}
}

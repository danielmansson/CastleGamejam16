using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotDeath : MonoBehaviour
{
	[SerializeField]
	GameObject m_root;
	[SerializeField]
	List<GameObject> m_parts;

	class State
	{
		public GameObject gameObject;
		public Vector3 pos;
		public Vector3 vel;
		public float angle;
		public float angleVel;
	}

	List<State> m_state = new List<State>();

	void Start ()
	{
		foreach (var p in m_parts)
		{
			m_state.Add(new State()
			{
				gameObject = p,
				vel = ((Vector3)Random.insideUnitCircle + Vector3.right + Vector3.up) * Random.Range(15f, 70f),
				angleVel = Random.Range(-320f, 320f)
			});
		}
	}
	
	void Update ()
	{
		foreach (var p in m_state)
		{
			p.vel += new Vector3(0,-150,0) * Time.deltaTime;
			p.pos += p.vel * Time.deltaTime;
			p.angle += p.angleVel * Time.deltaTime;
			p.gameObject.transform.localPosition = p.pos;
			p.gameObject.transform.localRotation = Quaternion.Euler(0,0, p.angle);
		}
	}

	public void BlowUp(bool left)
	{
		if (left)
			m_root.transform.localScale = new Vector3(-1, 1, 1);

	}
}

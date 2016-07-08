using UnityEngine;
using System.Collections;

public class PlasmaVisual : MonoBehaviour
{
	[SerializeField]
	float m_speed;
	[SerializeField]
	GameObject m_hitEffect;
	[SerializeField]
	GameObject m_root;
	[SerializeField]
	GameObject m_visual;

	public Transform Target { get; set; }
	public float TimeToImpact { get; set; }
	public Vector3 StartPos { get; set; }
	public Vector3 Dir { get; set; }

	bool m_hasReachedAlready = false;
	bool m_hadTarget = false;
	float m_timer = 0f;

	void Start ()
	{
		if (Dir.x < 0f)
			m_root.transform.localScale = new Vector3(-1f, 1f, 1f);

		Destroy(this.gameObject, 10f);
	}
	
	void Update ()
	{
		if (Target != null)
		{
			m_hadTarget = true;
			m_timer += Time.deltaTime;
			m_root.transform.position = new Vector3(Mathf.Lerp(StartPos.x, Target.transform.position.x, m_timer / TimeToImpact), transform.position.y, transform.position.z);
			//StartPos = m_root.transform.position;
			if (TimeToImpact < 0f)
			{
				OnReachedTarget();
			}
		}
		else
		{
			m_root.transform.position = m_root.transform.position + Dir * m_speed * Time.deltaTime;
			StartPos = m_root.transform.position;

			if (m_hadTarget)
			{
				OnReachedTarget();
			}
		}
	}

	void OnReachedTarget()
	{
		if (!m_hasReachedAlready)
		{
			m_hasReachedAlready = true;
			Target = null;
			Destroy(this.gameObject);

			if (m_hitEffect != null)
				Instantiate(m_hitEffect, m_visual.transform.position, Quaternion.identity);
		}
	}
}

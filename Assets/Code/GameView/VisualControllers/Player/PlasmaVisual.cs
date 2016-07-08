using UnityEngine;
using System.Collections;

public class PlasmaVisual : MonoBehaviour
{
	[SerializeField]
	float m_speed;
	[SerializeField]
	GameObject m_hitEffect;


	public Transform Target { get; set; }
	public float TimeToImpact { get; set; }
	public Vector3 StartPos { get; set; }
	public Vector3 Dir { get; set; }

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if (Target != null)
		{

		}
		else
		{
			//DistanceJoint2D/speed
			transform.position = transform.position + Dir * m_speed * Time.deltaTime;
			StartPos = transform.position;
		}
	}

	void OnReachedTarget()
	{

	}
}

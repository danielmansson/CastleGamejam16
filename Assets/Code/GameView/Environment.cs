using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour
{
	[SerializeField]
	Timeline.Type m_type;
	[SerializeField]
	string m_layer;
	[SerializeField]
	GameObject m_foreground;
	[SerializeField]
	GameObject m_background;
	[SerializeField]
	Transform m_playerTransform;
	[SerializeField]
	Material m_foregroundMaterial;
	[SerializeField]
	Material m_backgroundMaterial;

	public Transform PlayerTransform { get { return m_playerTransform; } }

	void Start ()
	{
		var children = GetComponentsInChildren<Transform>();
		foreach (var child in children)
		{
			child.gameObject.layer = LayerMask.NameToLayer(m_layer);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

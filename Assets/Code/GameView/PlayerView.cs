using UnityEngine;
using System.Collections;

public class PlayerView : MonoBehaviour
{
	public class Context
	{
		public GameModel model;
		public Timeline timeline;
		public VisualPrefabLoader loader;
	}

	public Timeline Timeline { get { return m_context.timeline; } }
	public GameModel Model { get { return m_context.model; } }

	Context m_context;
	PlayerVisualController m_visual;

	public void Init(Context context)
	{
		m_context = context;
	}

	void Start ()
	{
		var prefab = m_context.loader.GetPlayerPrefab(Timeline.TimelineType);

		var go = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
		go.transform.parent = transform;

		m_visual = go.GetComponent<PlayerVisualController>();
		m_visual.Init(this);
	}
	
	void Update ()
	{
	
	}
}

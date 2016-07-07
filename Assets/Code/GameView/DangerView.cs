using UnityEngine;
using System.Collections;

public class DangerView : MonoBehaviour
{
	public class Context
	{
		public GameModel model;
		public Timeline timeline;
		public Danger danger;
		public VisualPrefabLoader loader;
	}

	public Danger Danger { get { return m_context.danger; } }
	public Timeline Timeline { get { return m_context.timeline; } }
	public GameModel Model { get { return m_context.model; } }

	public event System.Action<DangerView> OnDestroy;

	Context m_context;
	DangerVisualController m_visual;

	void DestroyHandler()
	{
		Danger.OnDestroy -= DestroyHandler;

		if (OnDestroy != null)
		{
			OnDestroy(this);
		}
	}

	//Should be called by DangerVisualController
	public void DestroyView()
	{
		Destroy(this.gameObject);
	}

	public void Init(Context context)
	{
		m_context = context;

		Danger.OnDestroy += DestroyHandler;
	}

	void Start ()
	{
		var prefab = m_context.loader.GetPrefab(Danger);

		var go = (GameObject)Instantiate(prefab, transform.position, transform.rotation);
		go.transform.parent = transform;

		m_visual = go.GetComponent<DangerVisualController>();
		m_visual.Init(this);
	}
	
	void Update ()
	{
	
	}

	public float GetExtrapolatedSecondsToImpact()
	{
		return Model.ExtrapolateSecondsLeft(Danger.distanceLeft);
	}
}

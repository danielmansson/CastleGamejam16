using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimelineView : MonoBehaviour
{
	public class Context
	{
		public GameModel model;
		public Timeline timeline;
		public VisualPrefabLoader loader;
	}

	[SerializeField]
	DangerView m_dangerPrefab;

	public Timeline Timeline { get { return m_context.timeline; } }
	public GameModel Model { get { return m_context.model; } }

	List<DangerView> m_activeDangers = new List<DangerView>();
	Context m_context;

	public void Init(Context context)
	{
		m_context = context;

		foreach (var danger in Timeline.m_dangers)
		{
			var dangerView = (DangerView)Instantiate(m_dangerPrefab, transform.position, transform.rotation);
			dangerView.transform.parent = transform;

			var dangerContext = new DangerView.Context()
			{
				model = Model,
				timeline = Timeline,
				danger = danger,
				loader = m_context.loader
			};

			dangerView.Init(dangerContext);
			dangerView.OnDestroy += ViewDestroyedHandler;

			m_activeDangers.Add(dangerView);
		}
	}

	void ViewDestroyedHandler(DangerView dangerView)
	{
		m_activeDangers.Remove(dangerView);
	}
}

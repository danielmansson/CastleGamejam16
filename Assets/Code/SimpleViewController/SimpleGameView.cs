using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimpleGameView : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	[SerializeField]
	SimpleTimeline m_timelinePrefab;

	[SerializeField]
	float m_distance = 3f;

	GameModel m_model;
	List<SimpleTimeline> m_timelines;
	
	void Awake ()
	{
		m_modelMgr.OnModelInitialized += Init;
	}

	void Init()
	{
		m_model = m_modelMgr.Model;

		//Set up view
		var timelines = m_model.GetTimelines();

		Vector3 pos = Vector3.zero;
		foreach (var timeline in timelines)
		{
			var timelineView = (SimpleTimeline)Instantiate(m_timelinePrefab, transform.position, Quaternion.identity);
			timelineView.transform.parent = transform;
			timelineView.transform.localPosition = pos;
			pos += Vector3.down * m_distance;

			timelineView.Init(m_model, timeline);
		}
	}
}

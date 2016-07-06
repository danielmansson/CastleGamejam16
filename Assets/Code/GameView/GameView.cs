using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameView : MonoBehaviour
{
	[SerializeField]
	GameModelManager m_modelMgr;

	[System.Serializable]
	class TimelineViewEntry
	{
		public Timeline.Type type;
		public GameObject prefab;
		public Vector3 offset;
	}

	[SerializeField]
	List<TimelineViewEntry> m_timelineViews;

	GameModel m_model;

	void Awake ()
	{
		m_modelMgr.OnModelInitialized += Init;
	}

	void Init()
	{
		m_model = m_modelMgr.Model;

		foreach (var timeline in m_model.GetTimelines())
		{
			var entry = m_timelineViews.FirstOrDefault(tl => tl.type == timeline.TimelineType);

			if (entry == null)
			{
				Debug.LogError("There is a Timeline without a view: " + timeline.TimelineType);
			}
			else
			{
				var go = (GameObject)Instantiate(entry.prefab, transform.position + entry.offset, Quaternion.identity);
				go.transform.parent = transform;
				var timelineView = go.GetComponent<TimelineView>();

				timelineView.Init(m_model, timeline);
			}
		}
	}

	void Update ()
	{
	
	}
}

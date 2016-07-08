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
	VisualPrefabLoader m_visualPrefabLoader;

	void Awake ()
	{
		m_visualPrefabLoader = GetComponent<VisualPrefabLoader>();

		m_modelMgr.OnModelInitialized += Init;
	}

	void Init()
	{
		m_model = m_modelMgr.Model;
		m_model.OnDeath += deathHandler;

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

				var context = new TimelineView.Context()
				{
					model = m_model,
					timeline = timeline,
					loader = m_visualPrefabLoader
				};

				timelineView.Init(context);
			}
		}
	}

	void deathHandler(Timeline timeline, Danger danger){
		AudioEvent.Play("PlayerDeath");
		StartCoroutine(deathSequence());
	}

	IEnumerator deathSequence(){
		List<Timeline> timelines = m_model.GetTimelines();
		int score = timelines[0].score + timelines[1].score + timelines[2].score;
		Debug.Log(score);

		yield return new WaitForSeconds(1.0f);
		AudioEvent.Play("GameOver");
		//show game over text + maybe score
	}


	void Update ()
	{
	
	}
}

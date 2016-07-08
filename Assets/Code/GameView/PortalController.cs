using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class PortalController : MonoBehaviour
{
	[System.Serializable]
	public class PortalEntry
	{
		public Timeline.Type type;
		public MeshRenderer renderer;
		public float startValue;
	}

	[SerializeField]
	List<PortalEntry> m_entries;

	[SerializeField]
	float m_openTime;
	[SerializeField]
	float m_openStart = 5.0f;
	[SerializeField]
	float m_openEnd = 0.1f;

	[SerializeField]
	float m_closeTime;
	[SerializeField]
	float m_closeEnd = 5.0f;

	void Awake()
	{
		foreach (var entry in m_entries)
		{
			entry.renderer.sharedMaterials[0].SetFloat("_Threshold2", entry.startValue);
		}
	}

	public void OpenPortal(Timeline.Type type)
	{
		var entry = m_entries.FirstOrDefault(e => e.type == type);
		if (entry != null)
		{
			StartCoroutine(OpenSequence(entry.renderer));
		}
	}

	public void ClosePortal(Timeline.Type type)
	{
		var entry = m_entries.FirstOrDefault(e => e.type == type);
		if (entry != null)
		{
			StartCoroutine(CloseSequence(entry.renderer));
		}
	}

	IEnumerator OpenSequence(MeshRenderer renderer)
	{
		float timer = 0f;

		while (timer < m_openTime)
		{
			timer += Time.deltaTime;
			float t = timer / m_openTime;
			renderer.sharedMaterials[0].SetFloat("_Threshold2", Mathf.Lerp(m_openStart, m_openEnd, t));
			yield return null;
		}
	}

	IEnumerator CloseSequence(MeshRenderer renderer)
	{
		float timer = 0f;

		while (timer < m_openTime)
		{
			timer += Time.deltaTime;
			float t = timer / m_openTime;
			renderer.sharedMaterials[0].SetFloat("_Threshold2", Mathf.Lerp(m_openEnd, m_closeEnd, t));
			yield return null;
		}
	}

	[ContextMenu("Open1")]
	void Open1()
	{
		OpenPortal(Timeline.Type.Shooter);
	}

	[ContextMenu("Open2")]
	void Open2()
	{
		OpenPortal(Timeline.Type.Jumper);
	}

	[ContextMenu("Open3")]
	void Open3()
	{
		OpenPortal(Timeline.Type.Shield);
	}

	[ContextMenu("Close1")]
	void Close1()
	{
		ClosePortal(Timeline.Type.Shooter);
	}

	[ContextMenu("Close2")]
	void Close2()
	{
		ClosePortal(Timeline.Type.Jumper);
	}

	[ContextMenu("Close3")]
	void Close3()
	{
		ClosePortal(Timeline.Type.Shield);
	}

	[ContextMenu("CloseAll")]
	void CloseAll()
	{
		ClosePortal(Timeline.Type.Shooter);
		ClosePortal(Timeline.Type.Jumper);
		ClosePortal(Timeline.Type.Shield);
	}
	[ContextMenu("CloseAllSeq")]
	void CloseAllSeq()
	{
		StartCoroutine(CloseAllSequence(1.0f));
	}

	IEnumerator CloseAllSequence(float t)
	{
		ClosePortal(Timeline.Type.Shield);
		yield return new WaitForSeconds(t);
		ClosePortal(Timeline.Type.Jumper);
		yield return new WaitForSeconds(t);
		ClosePortal(Timeline.Type.Shooter);
		yield return new WaitForSeconds(t);
	}
	IEnumerator OpenAllSequence(float t)
	{
		yield return new WaitForSeconds(t);
		OpenPortal(Timeline.Type.Jumper);
		yield return new WaitForSeconds(t);
		OpenPortal(Timeline.Type.Shield);
	}
}

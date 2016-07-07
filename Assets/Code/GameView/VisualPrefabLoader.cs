using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class VisualPrefabLoader : MonoBehaviour
{
	[System.Serializable]
	public class Entry
	{
		public Danger.Type type;
		public Player.Action action;
		public GameObject prefab;
	}

	[SerializeField]
	List<Entry> m_prefabs;

	public GameObject GetPrefab(Danger danger)
	{
		return GetPrefab(danger.type, danger.requiredAction);
	}

	public GameObject GetPrefab(Danger.Type type, Player.Action action)
	{
		var entry = m_prefabs.FirstOrDefault(p => p.type == type && p.action == action);

		return entry != null ? entry.prefab : null;
	}
}

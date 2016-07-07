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

	[System.Serializable]
	public class EnvironmentEntry
	{
		public Timeline.Type type;
		public GameObject prefab;
	}

	[System.Serializable]
	public class PlayerEntry
	{
		public Timeline.Type type;
		public GameObject prefab;
	}

	[SerializeField]
	List<Entry> m_prefabs;

	[SerializeField]
	List<EnvironmentEntry> m_environmentPrefabs;

	[SerializeField]
	List<PlayerEntry> m_playerPrefabs;

	public GameObject GetPlayerPrefab(Timeline.Type type)
	{
		var entry = m_playerPrefabs.FirstOrDefault(p => p.type == type);

		return entry != null ? entry.prefab : null;
	}

	public GameObject GetEnvironmentPrefab(Timeline.Type type)
	{
		var entry = m_environmentPrefabs.FirstOrDefault(p => p.type == type);

		return entry != null ? entry.prefab : null;
	}

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

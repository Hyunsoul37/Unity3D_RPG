using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager instance;

	public static GameManager Getinstance()
	{
		if (instance == null)
		{
			instance = FindObjectOfType<GameManager>();

			if (instance == null)
				Debug.LogError("There Needs to one Active GameManager Script a GameObject in your Scene");

			return instance;

		}

		return instance;
	}

	private List<GameObject> monsters = new List<GameObject>();

	public void AddNewMonsters(GameObject mon)
	{
		bool SomeEixt = false;

		for(int i = 0; i < monsters.Count; i++)
		{
			if(monsters[i] == mon)
			{
				SomeEixt = true;
				break;
			}
		}

		if(SomeEixt == false)
		{
			monsters.Add(mon);
		}
	}

	public void RemoveMonster(GameObject mon)
	{
		foreach(GameObject monster in monsters)
		{
			if(monster == mon)
			{
				monsters.Remove(monster);
				break;
			}
		}
	}

	public void ChangeCurrentTarget(GameObject mon)
	{
		DeselectAllMonsters();
		mon.GetComponent<EnemyFSM>().ShowSelection();
	}

	public void DeselectAllMonsters()
	{
		for(int i = 0; i < monsters.Count; i++)
		{
			monsters[i].GetComponent<EnemyFSM>().HideSelection();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnObj : MonoBehaviour
{
	List<Transform> SpawnPos = new List<Transform>();
	GameObject[] monster;

	public GameObject monsterPrefab;
	public int SpawnNumber = 1;
	public float respawnDelay = 3f;

	private int DeadMonster = 0;

	private void Start()
	{
		MakeSpawnPos();
	}

	private void MakeSpawnPos()
	{
		foreach(Transform pos in this.transform)
		{
			if(pos.CompareTag("Respawn"))
			{
				SpawnPos.Add(pos);
			}
		}

		if(SpawnNumber > SpawnPos.Count)
		{
			SpawnNumber = SpawnPos.Count;
		}

		monster = new GameObject[SpawnNumber];

		MakeMonster();
	}

	private void MakeMonster()
	{
		for(int i = 0; i < SpawnNumber; i++)
		{
			GameObject mon = Instantiate(monsterPrefab, SpawnPos[i].position, Quaternion.identity) as GameObject;
			mon.GetComponent<EnemyFSM>().SetReSpawnObj(gameObject, i, SpawnPos[i].position);
			mon.SetActive(false);

			monster[i] = mon;

			GameManager.Getinstance().AddNewMonsters(mon);
		}
	}

	private void SpawnMonster()
	{
		for(int i = 0; i < monster.Length; i++)
		{
			monster[i].GetComponent<EnemyFSM>().AddToWorldAgain();
			monster[i].SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player"))
		{
			SpawnMonster();
			GetComponent<SphereCollider>().enabled = false;
		}
	}

	public void RemoveMonster(int spawnID)
	{
		DeadMonster++;

		monster[spawnID].SetActive(false);
		print(spawnID + "monster was killed");

		if(DeadMonster == monster.Length)
		{
			StartCoroutine(InitMonster());
			DeadMonster = 0;

			
		}
	}

	IEnumerator InitMonster()
	{
		yield return new WaitForSeconds(respawnDelay);
		GetComponent<SphereCollider>().enabled = true;
	}
}

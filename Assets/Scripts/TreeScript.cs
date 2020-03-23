using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
	private float Distance = 3.5f;
	private float CutTime = 5.0f;
	private float Timer = 0f;
	private bool isClick = false;

	private Transform player;
	public GameObject TreeItem;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		if(isClick)
		{
			StartTimer();
		}
		else if (GetPlayerDistance() <= Distance)
			Debug.Log("F 키를 눌러 나무 베기");
	}

	public void CutDownTree()
	{
		if(GetPlayerDistance() <= Distance)
		{
			isClick = true;
		}
	}

	private float GetPlayerDistance()
	{
		return Vector3.Distance(this.transform.position, player.position);
	}

	private void DropItem()
	{
		if(this.gameObject.activeSelf == false)
		{
			Instantiate(TreeItem, this.transform.position, Quaternion.identity);
		}
	}

	private void StartTimer()
	{
		if(Timer >= CutTime)
		{
			Timer = 0f;
			this.gameObject.SetActive(false);
			DropItem();
		}

		Timer += Time.deltaTime;
	}
}

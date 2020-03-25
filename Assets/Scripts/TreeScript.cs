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
		GetComponent<ObjectData>().Obj_ID = 10001;
	}

	private void Update()
	{
		//if(isClick)
		//{
		//	StartTimer();
		//}
		if (GetPlayerDistance() <= Distance)
		{
			if(Input.GetKeyDown(KeyCode.F) || isClick)
			{
				Debug.Log("Input Key F");
				CutDownTree();
				StartTimer();
			}
			else
			{
				InformationUI.Getinstance().Info_Window.SetActive(true);
				InformationUI.Getinstance().CastingBar.SetActive(false);
				InformationUI.Getinstance().Info_Text.text = "F키를 눌러 나무 베기";
				isClick = false;
			}


		}
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
			InformationUI.Getinstance().Info_Window.SetActive(false);
		}

		Timer += Time.deltaTime;

		InformationUI.Getinstance().CastingBar.SetActive(true);
		InformationUI.Getinstance().images[1].rectTransform.localScale = new Vector3((Timer / CutTime), 1f, 1f);
	}
}

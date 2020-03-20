using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNPC : MonoBehaviour
{
	private Transform player;
	private float ConverseDistance = 3f;
	private ShopDisplay shop;

	public string Name;
	public int npcID;
	public Text NPC_Name;
	private Transform Cam;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		shop = GameObject.FindGameObjectWithTag("ShopDisplay").GetComponent<ShopDisplay>();
		Cam = Camera.main.transform;

		NPC_Name.text = Name;
	}

	private void Update()
	{
		NPC_Name.transform.LookAt(NPC_Name.transform.position + Cam.rotation * Vector3.forward, Cam.rotation * Vector3.up);

		if (GetDistance() > ConverseDistance)
		{
			shop.Window.SetActive(false);
			UIManager.Getinstance().Panel.SetActive(true);
		}

		shop.SetCoin(player.gameObject.GetComponent<PlayerParameter>().Money);
	}

	public void ConversePlayer()
	{
		if (ConverseDistance > GetDistance())
		{
			
			if (TalkManager.Getinstance().HaveTalk(npcID))
			{
				Debug.Log(npcID);
				TalkManager.Getinstance().NPC_Name.text = Name;
				TalkManager.Getinstance().TalkToNPC(npcID);
			}
			StartCoroutine("ActiveWindow");
			
		}
		else
		{
			shop.Window.SetActive(false);
		}
	}

	private float GetDistance()
	{
		float dis = Vector3.Distance(this.transform.position, player.position);

		return dis;
	}

	IEnumerator ActiveWindow()
	{
		while(true)
		{
			if(!TalkManager.Getinstance().TalkWindow.activeSelf)
			{
				shop.Window.SetActive(true);
				UIManager.Getinstance().Panel.SetActive(false);
				StopCoroutine("ActiveWindow");
			}

			yield return new WaitForSeconds(0.5f);
		}
	}
}

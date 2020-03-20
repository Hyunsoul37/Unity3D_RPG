using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
	private static TalkManager instance;

	public GameObject TalkWindow;
	public Text NPC_Name;
	public Text TalkText;
	private int orderNum = 0;
	private int NPC_ID;

	Dictionary<int, string[]> Talk = new Dictionary<int, string[]>();

	public static TalkManager Getinstance()
	{
		if (instance == null)
		{
			instance = FindObjectOfType<TalkManager>();

			if (instance == null)
				Debug.LogError("There Needs to one Active TalkManager Script a GameObject in your Scene");

			return instance;

		}

		return instance;
	}

	private void Start()
	{
		TalkWindow.SetActive(false);
		Talk.Add(1000, new string[] { "물건 보고 가세요", "어디에도 없습니다. \n이런 물건들은"});
	}

	public void TalkToNPC(int npcID)
	{
		if (orderNum == Talk[npcID].Length)
		{
			TalkEnd();
			return;
		}

		TalkWindow.SetActive(true);
		NPC_ID = npcID;
		TalkText.text = Talk[npcID][orderNum];
	}

	public bool HaveTalk(int npcID)
	{
		return Talk.ContainsKey(npcID);
	}

	private void TalkEnd()
	{
		TalkWindow.SetActive(false);
		orderNum = 0;
		NPC_ID = 0;
	}

	public void ClickEvent()
	{
		orderNum++;
		TalkToNPC(NPC_ID);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
	public Text Title;
	private Text[] InfoTexts;

	private QuestDisplay questDisplay;
	private QuestAcceptDisplay AcceptDisplay;
	public QuestData data;

	private int SlotID;
//	private bool isAccepted;
	private bool isClear = false;

	private void Start()
	{
		questDisplay = GetComponentInParent<QuestDisplay>();

		AcceptDisplay = GetComponentInParent<QuestAcceptDisplay>();
	}

	public void ClickTilte()
	{
		if (questDisplay != null)
		{
			questDisplay.ActiveQuestInfo(SlotID, data.isAccept);
		}
		else if(AcceptDisplay != null)
		{
			Debug.Log(SlotID);
			AcceptDisplay.ActiveQuestInfo(SlotID, isClear);
		}

		InitQuestInfo();
	}

	public void InitQuestInfo(QuestData data, GameObject QuestInfo)
	{
		this.data = data;
		this.SlotID = data.QuestID;
		InfoTexts = QuestInfo.GetComponentsInChildren<Text>();
		InitQuestInfo();
	}

	public void InitQuestInfo()
	{
		Title.text = data.QuestName;
		InfoTexts[0].text = data.QuestName;
		InfoTexts[1].text = data.QuestContents;

		string s = "";

		if (data.Reward.Coin != 0)
			s += "coin : " + data.Reward.Coin.ToString() + "\n";
		if (data.Reward.Exp != 0)
			s += "Exp : " + data.Reward.Exp.ToString() + "\n";
		if (data.Reward.item != null)
			s += data.Reward.item.GetComponent<Item>().Item_Name;
		if (data.Reward.itemNum != 0)
			s += " " + data.Reward.itemNum.ToString() + "개";

		InfoTexts[2].text = "보상 내용 \n" + s;
	}

	public void AcceptQuest()
	{
		data.isAccept = true;
		QuestManager.Getinstace().AcceptedquestList.Add(data);
		QuestManager.Getinstace().SortQuest(QuestManager.Getinstace().AcceptedquestList);
	}

	public void QuestClear()
	{
		isClear = true;
	}

}

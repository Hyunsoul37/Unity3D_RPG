using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAcceptDisplay : MonoBehaviour
{
	public GameObject Window;
	public GameObject QuestSlot;
	public GameObject QuestInfo;
	public Button ClearButton;

	private List<QuestData> questDatas = new List<QuestData>();
	private Dictionary<int, GameObject> SlotList = new Dictionary<int, GameObject>();

	private float slotsize = 40f;
	private bool WindowActive = false;
	private bool isActive = false;
	private int slotID = -1;
	private int AcceptCount;

	private void Start()
	{
		QuestInfo.SetActive(isActive);
		Window.SetActive(WindowActive);
		AcceptCount = questDatas.Count;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (questDatas.Count != AcceptCount)
			{
				ClearDictionary();
				MakeQuestSlot();
			}

			ToggleDisplay();
		}

		questDatas = QuestManager.Getinstace().AcceptedquestList;
		CheckQuestClear();
	}

	private void ToggleDisplay()
	{
		WindowActive = !WindowActive;
		Window.SetActive(WindowActive);

		if(WindowActive == false)
		{
			QuestInfo.SetActive(false);
			isActive = true;
		}
	}

	private void MakeQuestSlot()
	{
		int Count = questDatas.Count;

		for (int i = 0; i < Count; i++)
		{
			GameObject slot = Instantiate(QuestSlot, 
				Window.transform.position + new Vector3(0, (-slotsize * i) - slotsize) * this.transform.localScale.x, 
				Quaternion.identity, Window.transform);

			slot.GetComponent<QuestSlot>().InitQuestInfo(questDatas[i], QuestInfo);
			SlotList.Add(questDatas[i].QuestID, slot);
		}

		AcceptCount = questDatas.Count;
	}
	
	private void ClearDictionary()
	{
		foreach(GameObject obj in SlotList.Values)
		{
			Destroy(obj.gameObject); 
		}

		SlotList.Clear();
	}

	public void ActiveQuestInfo(int id, bool isClear)
	{
		if (slotID == id)
		{
			QuestInfo.SetActive(isActive);
			isActive = !isActive;
		}
		else
		{
			QuestInfo.SetActive(true);
			isActive = false;
		}

		ClearButton.interactable = isClear;
		slotID = id;
	}

	public void CheckQuestClear()
	{
		for(int i = 0; i < SlotList.Count; i++)
		{
			if(questDatas[i].isClear)
				SlotList[questDatas[i].QuestID].GetComponent<QuestSlot>().QuestClear();
		}
	}

	public void ClickClearButton()
	{
		PlayerParameter player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParameter>();

		QuestData RemoveData = SlotList[slotID].GetComponent<QuestSlot>().data;

		player.Money += RemoveData.Reward.Coin;
		player.Current_Exp += RemoveData.Reward.Exp;

		if(RemoveData.Reward.item != null)
		{
			Inventory inven = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

			for(int i = 0; i < RemoveData.Reward.itemNum; i++)
			{
				inven.AddItem(RemoveData.Reward.item.GetComponent<Item>());
			}

		}
		
		UIManager.Getinstance().UpdatePlayerUI(player);

		Destroy(SlotList[slotID].gameObject);
		SlotList.Remove(slotID);

		QuestManager.Getinstace().AcceptedquestList.Remove(RemoveData);
		QuestManager.Getinstace().questList.Remove(RemoveData);
		QuestInfo.SetActive(false);
	}
}

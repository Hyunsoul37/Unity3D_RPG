using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
	public GameObject Window;
	public GameObject QuestSlot;
	public GameObject QuestInfo;
	public Button AcceptButton;
	private Dictionary<int, GameObject> SlotList = new Dictionary<int, GameObject>();

	private int Count;
	private int slotID = -1;
	private float slothight = 40f;
	private bool isActive = false;
	private bool WindowActive = false;

	private void Start()
	{
		Count = QuestManager.Getinstace().questList.Count;
		MakeQuestSlots();
		QuestInfo.SetActive(isActive);
		Window.SetActive(WindowActive);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			if(Count != QuestManager.Getinstace().questList.Count)
			{
				ClearDictionary();
				Start();
			}

			ToggleDisplay();
		}
	}

	private void MakeQuestSlots()
	{
		for(int i = 0; i < Count; i++)
		{
			GameObject slot = Instantiate(QuestSlot, 
				Window.transform.position + new Vector3(0, (-slothight * i) - slothight) * this.transform.localScale.x, 
				Quaternion.identity, Window.transform);

			slot.GetComponent<QuestSlot>().InitQuestInfo(QuestManager.Getinstace().questList[i], QuestInfo);
			SlotList.Add(QuestManager.Getinstace().questList[i].QuestID, slot);
		}
	}

	private void ClearDictionary()
	{
		foreach (GameObject obj in SlotList.Values)
		{
			Destroy(obj.gameObject);
		}

		SlotList.Clear();
	}

	public void ActiveQuestInfo(int id, bool isAccepted)
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

		AcceptButton.interactable = !isAccepted;
		slotID = id;
	}

	private void ToggleDisplay()
	{
		WindowActive = !WindowActive;
		Window.SetActive(WindowActive);
	}

	public void buttonClick()
	{
		SlotList[slotID].GetComponent<QuestSlot>().AcceptQuest();
		AcceptButton.interactable = false;
	}
}

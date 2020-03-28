using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
	private static QuestManager instance;

	public static QuestManager Getinstace()
	{
		if(instance == null)
		{
			instance = GameObject.FindObjectOfType<QuestManager>();

			if (instance == null)
				Debug.Log("There Needs to one Active QuestManager Script a GameObject in your Scene");

			return instance;
		}

		return instance;
	}

	public int QuestID;

	public List<QuestData> questList;
	public List<QuestData> AcceptedquestList;

	public GameObject[] RewardItem;

	private void Awake()
	{
		questList = new List<QuestData>();
		AcceptedquestList = new List<QuestData>();
		GeneateData();
	}

	private void GeneateData()
	{
		questList.Add(new QuestData(0,
									new Quest { type = TYPE.Monster, Num = 5, TargetObject_ID = 102 },
									"주민들의 골칫거리", 
									"슬라임 5마리 처치하기",
									new QuestReward { Coin = 10, Exp = 10},
									new Current()));

		questList.Add(new QuestData(1, 
									new Quest { type = TYPE.Monster, Num = 5, TargetObject_ID = 103 },
									"주민들의 부탁",
									"보스 슬라임 5마리 처치하기",
									new QuestReward { Coin = 20, Exp = 20, item = RewardItem[0], itemNum = 2},
									new Current()));

		questList.Add(new QuestData(2,
									new Quest { type = TYPE.Monster, Num = 5, TargetObject_ID = 103 },
									"Quest ID 2",
									"Quest ID 2",
									new QuestReward { Coin = 20, Exp = 20, item = RewardItem[0], itemNum = 2 },
									new Current()));

		questList.Add(new QuestData(4,
									new Quest { type = TYPE.Collect, Num = 5, TargetObject_ID = 1003 },
									"장작 구하기",
									"주변의 나무를 베어 장작을 모으자",
									new QuestReward { Coin = 20, Exp = 20, item = RewardItem[0], itemNum = 3 },
									new Current()));
	}

	public void SortQuest(List<QuestData> list)
	{
		for(int i = 0; i < list.Count - 1; i++)
		{
			QuestData tmp = list[i];

			for(int j = i + 1; j < list.Count; j++)
			{
				if (tmp.QuestID > list[j].QuestID)
				{
					list[i] = list[j];
					list[j] = tmp;
				}
			}
		}
	}

	//public void KillMonster(GameObject monster)
	//{
	//	string KilledMonster = monster.GetComponent<EnemyParameter>().e_name;
		
	//	for(int i = 0; i < AcceptedquestList.Count; i++)
	//	{
	//		if(AcceptedquestList[i].quest.type == TYPE.Monster)
	//		{
	//			if(AcceptedquestList[i].quest.ObjectName == KilledMonster)
	//			{
	//				AcceptedquestList[i].current.Num++;
	//			}

	//			if(AcceptedquestList[i].quest.Num == AcceptedquestList[i].current.Num)
	//			{
	//				AcceptedquestList[i].isClear = true;
	//			}
	//		}
	//	}
	//}

	public void QuestCheck(GameObject obj)
	{
		int Obj_ID = obj.GetComponent<ObjectData>().Obj_ID;

		for (int i = 0; i < AcceptedquestList.Count; i++)
		{
			if (AcceptedquestList[i].quest.type == TYPE.Monster)
			{
				if (AcceptedquestList[i].quest.TargetObject_ID == Obj_ID)
				{
					AcceptedquestList[i].current.Num++;


					Debug.Log(AcceptedquestList[i].QuestName);
					Debug.Log(AcceptedquestList[i].current.Num);
				}

				if (AcceptedquestList[i].quest.Num <= AcceptedquestList[i].current.Num)
				{
					AcceptedquestList[i].isClear = true;
				}
			}
			else if(AcceptedquestList[i].quest.type == TYPE.Collect)
			{
				Inventory inven = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

				for(int j = 0; j < inven.SlotList.Count; j++)
				{
					Slot slot = inven.SlotList[j].GetComponent<Slot>();

					if (slot.isSlots())
					{
						Item item = slot.slot.Peek();

						Debug.Log(item.Item_ID + " " + AcceptedquestList[i].quest.TargetObject_ID);

						if(item.Item_ID == AcceptedquestList[i].quest.TargetObject_ID &&
							slot.slot.Count >= AcceptedquestList[i].quest.Num)
						{
							AcceptedquestList[i].isClear = true;
						}

					}
				}
			}
		}
	}
}

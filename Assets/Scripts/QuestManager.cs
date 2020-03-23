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
									new Quest { type = TYPE.Monster, Num = 5, ObjectName = "Slime" },
									"주민들의 골칫거리", 
									"슬라임 5마리 처치하기",
									new QuestReward { Coin = 10, Exp = 10},
									new Current()));

		questList.Add(new QuestData(1, 
									new Quest { type = TYPE.Monster, Num = 5, ObjectName = "BossSlime" },
									"주민들의 부탁",
									"보스 슬라임 5마리 처치하기",
									new QuestReward { Coin = 20, Exp = 20, item = RewardItem[0], itemNum = 2},
									new Current()));

		questList.Add(new QuestData(2,
									new Quest { type = TYPE.Monster, Num = 5, ObjectName = "BossSlime" },
									"Quest ID 2",
									"Quest ID 2",
									new QuestReward { Coin = 20, Exp = 20, item = RewardItem[0], itemNum = 2 },
									new Current()));

		questList.Add(new QuestData(4,
									new Quest { type = TYPE.Collect, Num = 5, ObjectName = "BossSlime" },
									"Quest ID 4",
									"Quest ID 4",
									new QuestReward { Coin = 20, Exp = 20, item = RewardItem[0], itemNum = 2 },
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

	public void KillMonster(GameObject monster)
	{
		string KilledMonster = monster.GetComponent<EnemyParameter>().e_name;
		
		for(int i = 0; i < AcceptedquestList.Count; i++)
		{
			if(AcceptedquestList[i].quest.type == TYPE.Monster)
			{
				if(AcceptedquestList[i].quest.ObjectName == KilledMonster)
				{
					AcceptedquestList[i].current.Num++;
				}

				if(AcceptedquestList[i].quest.Num == AcceptedquestList[i].current.Num)
				{
					AcceptedquestList[i].isClear = true;
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct QuestReward
{
	public int Coin;
	public GameObject item;
	public int itemNum;
	public int Exp;
}

public enum TYPE
{
	Monster,
	Collect,
}

public struct Quest
{
	public TYPE type;
	public int Num;
	public string ObjectName;

}

public struct Current
{
	public int Num;

	public Current(int _num = 0) { Num = _num; }
}

public class QuestData
{
	public int QuestID;
	public Quest quest;
	public string QuestName;
	public string QuestContents;
	public QuestReward Reward;
	public bool isClear = false;
	public bool isAccept = false;
	public Current current = new Current();

	public QuestData(int ID, Quest quest, string name, string Contents, QuestReward reward, Current current)
	{
		QuestID = ID;
		this.quest = quest;
		QuestName = name;
		QuestContents = Contents;
		Reward = reward;
		this.current = current;
	}
}

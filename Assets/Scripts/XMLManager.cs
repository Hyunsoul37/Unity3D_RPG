using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XMLManager : MonoBehaviour
{
	public static XMLManager instance;
	public TextAsset EnemyFileXml;
	private Dictionary<string, MonsterParameter> dicMonster = new Dictionary<string, MonsterParameter>();

	struct MonsterParameter
	{
		public string Name;
		public int Level;
		public int Max_HP;
		public int Attack_Min;
		public int Attack_Max;
		public int Defence;
		public int Exp;
		public int RewardMoney;
	}

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		MakeMonsterXML();
	}

	private void MakeMonsterXML()
	{
		XmlDocument monsterXMLDoc = new XmlDocument();
		monsterXMLDoc.LoadXml(EnemyFileXml.text);

		XmlNodeList monsterNodeList = monsterXMLDoc.GetElementsByTagName("row");

		foreach(XmlNode monsterNode in monsterNodeList)
		{
			MonsterParameter monsterParameter = new MonsterParameter();

			foreach(XmlNode childNode in monsterNode.ChildNodes)
			{
				if (childNode.Name == "Name")
					monsterParameter.Name = childNode.InnerText;

				if (childNode.Name == "Level")
					monsterParameter.Level = int.Parse(childNode.InnerText);

				if (childNode.Name == "Max_HP")
					monsterParameter.Max_HP = int.Parse(childNode.InnerText);

				if (childNode.Name == "Attack_Min")
					monsterParameter.Attack_Min = int.Parse(childNode.InnerText);

				if (childNode.Name == "Attack_Max")
					monsterParameter.Attack_Max = int.Parse(childNode.InnerText);

				if (childNode.Name == "Defence")
					monsterParameter.Defence = int.Parse(childNode.InnerText);

				if (childNode.Name == "Exp")
					monsterParameter.Exp = int.Parse(childNode.InnerText);

				if (childNode.Name == "RewardMoney")
					monsterParameter.RewardMoney = int.Parse(childNode.InnerText);

				print(childNode.Name + " : " + childNode.InnerText);
			}

			dicMonster[monsterParameter.Name] = monsterParameter;
		}
	}

	public void LoadMonsterParameterFromXML(string monName, EnemyParameter m_Parameter)
	{
		m_Parameter.level = dicMonster[monName].Level;
		m_Parameter.Current_HP = dicMonster[monName].Max_HP;
		m_Parameter.Max_HP = dicMonster[monName].Max_HP;
		m_Parameter.Attack_Min = dicMonster[monName].Attack_Min;
		m_Parameter.Attack_Max = dicMonster[monName].Attack_Max;
		m_Parameter.Defense = dicMonster[monName].Defence;
		m_Parameter.Exp = dicMonster[monName].Exp;
		m_Parameter.RewardMoney = dicMonster[monName].RewardMoney;

		Debug.Log("LoadMonsterParameterFromXML : " + monName);
	}
}

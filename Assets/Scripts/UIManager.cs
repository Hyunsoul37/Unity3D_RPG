using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;

	public Text PlayerName;
	public Text PlayerCoin;
	public Text PlayerLevel;
	public Image PlayerHP;
	public Image PlayerExp;
	public GameObject Panel;

	public static UIManager Getinstance()
	{
		if(instance == null)
		{
			instance = FindObjectOfType<UIManager>();

			if (instance == null)
				Debug.LogError("There Needs to one Active UIManager Script a GameObject in your Scene");

			return instance;

		}

		return instance;
	}

	public void UpdatePlayerUI(PlayerParameter p_Parameter)
	{
		PlayerName.text = "Name : " + p_Parameter.name;
		PlayerCoin.text = "Coin : " + p_Parameter.Money;
		PlayerLevel.text = "Level : " + p_Parameter.level;
		PlayerHP.rectTransform.localScale = new Vector3((float)p_Parameter.Current_HP / (float)p_Parameter.Max_HP, 1f, 1f);
		PlayerExp.rectTransform.localScale = new Vector3((float)p_Parameter.Current_Exp / (float)p_Parameter.ExpToNextLevel, 1f, 1f);
	}
}
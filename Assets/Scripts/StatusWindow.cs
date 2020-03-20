using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusWindow : MonoBehaviour
{
	private PlayerParameter p_parameter = null;

	public GameObject Window;

	public Text Max_Hp;
	public Text Attack;
	public Text Defence;
	public Text FreeStatus;
	public Text Exp;

	public Button HPUpgrade;
	public Button AttackUpgrade;
	public Button DefenceUpgrade;

	public bool isActive = false;

	private void Start()
	{
		p_parameter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParameter>();

		Window.SetActive(isActive);
	}

	private void ToggleDisplay()
	{
		isActive = !isActive;

		Window.SetActive(isActive);
	}

	private void StatusText()
	{
		Max_Hp.text = p_parameter.Current_HP + " / " + p_parameter.Max_HP;
		Attack.text = p_parameter.Attack_Min + " ~ " + p_parameter.Attack_Max;
		Defence.text = p_parameter.Defense.ToString();
		FreeStatus.text = p_parameter.FreeStatus.ToString();
		Exp.text = p_parameter.Current_Exp + " / " + p_parameter.ExpToNextLevel + 
			" (" + ((float)p_parameter.Current_Exp / p_parameter.ExpToNextLevel) * 100 + "%)";
	}

	private void InitButton()
	{
		if(p_parameter.FreeStatus <= 0)
		{
			HPUpgrade.interactable = false;
			AttackUpgrade.interactable = false;
			DefenceUpgrade.interactable = false;
		}
		else
		{
			HPUpgrade.interactable = true;
			AttackUpgrade.interactable = true;
			DefenceUpgrade.interactable = true;
		}
	}

	public void Upgrade_HP()
	{
		p_parameter.Max_HP += 10;
		p_parameter.FreeStatus--;
	}

	public void Upgrade_Attack()
	{
		p_parameter.Attack_Min += 2;
		p_parameter.Attack_Max += 2;
		p_parameter.FreeStatus--;
	}

	public void Upgrade_Defence()
	{
		p_parameter.Defense += 2;
		p_parameter.FreeStatus--;
	}

	private void Update()
	{
		StatusText();
		InitButton();

		if (Input.GetKeyDown(KeyCode.S))
		{
			ToggleDisplay();
		}
	}
}

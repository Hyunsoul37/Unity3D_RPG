using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameter : CharacterParameter
{
	public string p_name;
	public int Current_Exp { get; set; }
	public int ExpToNextLevel { get; set; }
	public int Money { get; set; }
	public int FreeStatus;

	public override void InitParameter()
	{
		p_name = "Player";
		level = 1;
		Max_HP = 1000;
		Current_HP = Max_HP;
		Attack_Min = 10;
		Attack_Max = 13;
		Defense = 1;

		Current_Exp = 0;
		ExpToNextLevel = 100 * level;
		Money = 0;

		FreeStatus = 0;

		isDead = false;

		UIManager.Getinstance().UpdatePlayerUI(this);

		StartCoroutine(NaturalRecovery());
	}

	public void AddMoney(int money)
	{
		this.Money += money;
		UIManager.Getinstance().UpdatePlayerUI(this);
	}

	protected override void UpdateAfterReceiveAttack()
	{
		base.UpdateAfterReceiveAttack();

		UIManager.Getinstance().UpdatePlayerUI(this);
		LevelUp();
		TakeDamage();
	}

	private void LevelUp()
	{
		if(Current_Exp >= ExpToNextLevel)
		{
			Current_Exp -= ExpToNextLevel;
			level++;
			FreeStatus++;
			ExpToNextLevel = level * 100;

			//level Up 하면 기본 status 증가
			Max_HP += (int)((float)Max_HP * 0.1f);
			Attack_Min += (int)((float)Attack_Min * 0.1f);
			Attack_Max += (int)((float)Attack_Max * 0.1f);
			Defense++;
		}
	}

	public IEnumerator NaturalRecovery()
	{
		while(true)
		{
			if(Current_HP + 20 <= Max_HP)
			{
				Current_HP += 20;
			}
			else if(Current_HP + 20 > Max_HP)
			{
				Current_HP = Max_HP;
			}

			LevelUp();
			UIManager.Getinstance().UpdatePlayerUI(this);

			yield return new WaitForSeconds(5.0f);
		}
	}

	private void TakeDamage()
	{
		HudDamageText = Resources.Load<GameObject>("HudDamageText");
		HudDamageText.GetComponent<DamageScript>().Damage = Damage;

		GameObject HudText = Instantiate(HudDamageText, this.gameObject.transform);
		HudText.transform.position = HudPos.position;

		Debug.Log(this.gameObject.name + " Damaged : " + Damage);
	}
}

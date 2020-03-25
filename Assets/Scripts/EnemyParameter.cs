using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyParameter : CharacterParameter
{
	public string e_name;
	public int Exp { get; set; }
	public int RewardMoney { get; set; }
	public int Monster_ID { get; set; }
	public Image HPBar;

	public override void InitParameter()
	{
		//e_name = "Monster";
		level = 1;
		Max_HP = 50;
		Current_HP = Max_HP;
		Attack_Min = 3;
		Attack_Max = 5;
		Defense = 1;
		Monster_ID = 100;

		Exp = 10;
		RewardMoney = Random.Range(10, 31);
		isDead = false;

		InitHPBarSize();

		XMLManager.instance.LoadMonsterParameterFromXML(e_name, this);
	}

	private void InitHPBarSize()
	{
		HPBar.rectTransform.localScale = new Vector3(1f, 1f, 1f);
	}

	protected override void UpdateAfterReceiveAttack()
	{
		base.UpdateAfterReceiveAttack();
		TakeDamage();
		HPBar.rectTransform.localScale = new Vector3((float)Current_HP / (float)Max_HP, 1f, 1f);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterParameter : MonoBehaviour
{
    public int level { get; set; }
	public int Max_HP { get; set; }
	public int Current_HP { get; set; }
	public int Attack_Min { get; set; }
	public int Attack_Max { get; set; }
	public int Defense { get; set; }
	public bool isDead { get; set; }

	public int Damage { get; set; }

	[System.NonSerialized]
	public UnityEvent DeadEvent = new UnityEvent();

	public GameObject HudDamageText;
	public Transform HudPos;

	public virtual void InitParameter()
	{

	}

	public int GetRandomAttack()
	{
		int Damage = Random.Range(Attack_Min, Attack_Max + 1);

		return Damage;
	}

	public void SetEnemyAttack(int EnemyAttackPower)
	{
		Damage = EnemyAttackPower;
		Current_HP -= EnemyAttackPower;
		UpdateAfterReceiveAttack();
	}

	protected virtual void UpdateAfterReceiveAttack()
	{
		if(Current_HP <= 0)
		{
			Current_HP = 0;
			isDead = true;
			DeadEvent.Invoke();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour
{
	public const int Idle = 0;
	public const int Walk = 1;
	public const int Attack = 2;
	public const int Attack_Idle = 3;
	public const int Dead = 4;

	public Animator animator;

	private void Start()
	{
		animator = GetComponentInChildren<Animator>();
	}

	public void ChangeAni(int aniName)
	{
		animator.SetInteger("AniName", aniName);
	}
}

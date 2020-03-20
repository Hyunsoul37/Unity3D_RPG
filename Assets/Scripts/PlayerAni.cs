using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
	public const int Ani_Idle = 0;
	public const int Ani_Walk = 1;
	public const int Ani_Attack = 2;
	public const int Ani_Attack_Idle = 3;
	public const int Ani_Dead = 4;

	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	public void ChangeAni(int aniNum)
	{
		animator.SetInteger("AniName", aniNum);
	}
}

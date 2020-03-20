using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniEventControl : MonoBehaviour
{
    public void SendAttackEnemy()
	{
		transform.parent.gameObject.SendMessage("AttackCalculate");
	}
}

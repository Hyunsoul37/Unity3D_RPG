using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public float rotateSpeed = 180f;

	[System.NonSerialized]
	public int money = 100;

	public void SetCoinValue(int _money)
	{
		this.money = _money;
	}

	private void OnTriggerEnter(Collider obj)
	{
		if(obj.gameObject.CompareTag("Player"))
		{
			obj.gameObject.GetComponent<PlayerParameter>().AddMoney(money);

			//Destroy(this.gameObject);
			ReMoveFromWorld();
		}
	}

	public void ReMoveFromWorld()
	{
		gameObject.SetActive(false);
	}

}

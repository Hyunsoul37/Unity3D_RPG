using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
	public static ObjectManager instance;

	public GameObject CoinPrefab;
	public int initialCoin = 15;

	private List<GameObject> coin = new List<GameObject>();

	private void Awake()
	{
		if (instance == null)
			instance = this;

		MakeCoins();
	}

	private void MakeCoins()
	{
		for(int i = 0; i < initialCoin; i++)
		{
			GameObject tmpCoin = Instantiate(CoinPrefab) as GameObject;

			tmpCoin.transform.parent = this.transform;

			tmpCoin.SetActive(false);
			coin.Add(tmpCoin);
		}
	}

	public void DropCoinToPosition(Vector3 Pos, int coinValue)
	{
		GameObject reusedCoin = null;

		for(int i = 0; i < coin.Count; i++)
		{
			if(coin[i].activeSelf == false)
			{
				reusedCoin = coin[i];
				break;
			}
		}

		if(reusedCoin == null)
		{
			GameObject newCoin = Instantiate(CoinPrefab) as GameObject;
			coin.Add(newCoin);
			reusedCoin = newCoin;
		}

		reusedCoin.SetActive(true);
		reusedCoin.GetComponentInChildren<Coin>().SetCoinValue(coinValue);
		reusedCoin.transform.position = new Vector3(Pos.x, reusedCoin.transform.position.y, Pos.z);
	}
}

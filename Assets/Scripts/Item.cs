using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	public string Item_Name;
	public int Item_ID;
	public string Item_Explanation;
	public Sprite image;
	public int ItemCount;
	public int Cost;

	private Inventory inventory = null;

	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
		GetComponent<ObjectData>().Obj_ID = Item_ID;
	}

	public void AddItem()
	{
		if (!inventory.AddItem(this))
			Debug.Log("Full Inventory");
		else
		{
			gameObject.SetActive(false);
			QuestManager.Getinstace().QuestCheck(this.gameObject);
			Destroy(this.gameObject);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
			AddItem();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
	public Image item_Image;
	public Text item_Name;
	public Text item_Cost;
	public Button Buy_Button;

	private Item sell_item;
	private PlayerParameter player;
	private Inventory inventory;

	private int cost;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParameter>();
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
	}

	public void InitDisplay(Item item)
	{
		sell_item = item;
		item_Image.sprite = item.image;
		item_Name.text = item.Item_Name;
		item_Cost.text = item.Cost + " Coin";

		cost = item.Cost;
	}

	public void ClickedBuyButton()
	{
		if (player.Money >= cost)
		{
			player.Money -= cost;
			inventory.AddItem(sell_item);
			UIManager.Getinstance().UpdatePlayerUI(player);
		}
		else
			return;
	}
}

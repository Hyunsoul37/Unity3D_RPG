using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Stack<Item> slot = new Stack<Item>();
	public Text text;
	public Sprite DefaultImage;
	private PlayerParameter playerParameter;

	public Image ItemImage;
	private bool isSlot;

	private void Awake()
	{
		isSlot = false;

		float size = text.gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta.x;
		text.fontSize = (int)(size * 0.3f);

		playerParameter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerParameter>();
		//ItemImage = transform.GetChild(1).GetComponent<Image>();
	}

	public Item ItemReturn()
	{
		return slot.Peek();
	}

	public bool ItemMax(Item item)
	{
		return (ItemReturn().ItemCount > slot.Count);
	}

	public bool isSlots()
	{
		return isSlot;
	}

	public void SetSlot(bool _slot)
	{
		this.isSlot = _slot;
	}

	public void AddItem(Item item)
	{
		slot.Push(item);
		UpDateinfo(true, item.image);
	}

	public void UsedItem()
	{
		if (!isSlot)
			return;

		if(slot.Count == 1)
		{
			CheckItemID(slot.Peek());
			slot.Clear();
			UpDateinfo(false, DefaultImage);
			return;
		}

		Item usingItem = slot.Pop();

		CheckItemID(usingItem);

		UpDateinfo(isSlot, ItemImage.sprite);
	}

	public void UpDateinfo(bool isSlot, Sprite sprite)
	{
		SetSlot(isSlot);
		transform.GetChild(1).GetComponent<Image>().sprite = sprite;

		ItemImage.sprite = sprite;

		if (slot.Count >= 1)
		{
			text.text = slot.Count.ToString();
		}
		else
			text.text = "";
		//ItemIO.SaveData();
	}

	private void CheckItemID(Item item)
	{
		switch(item.Item_ID)
		{
			case 1001:
				Debug.Log("using RedPortion");

				if (playerParameter.Current_HP + 50 > playerParameter.Max_HP)
					playerParameter.Current_HP = playerParameter.Max_HP;
				else
					playerParameter.Current_HP += 50;

				UIManager.Getinstance().UpdatePlayerUI(playerParameter);
				break;

			case 1002:
				Debug.Log("using FreeStatus Item");
				playerParameter.FreeStatus++;
				break;

			default:
				break;
		}
	}

	public void SellItem()
	{
		if (!isSlot)
			return;

		if (slot.Count == 1)
		{
			playerParameter.Money += (slot.Peek().Cost / 2);
			slot.Clear();
			UpDateinfo(false, DefaultImage);
			return;
		}

		Item usingItem = slot.Pop();

		UpDateinfo(isSlot, ItemImage.sprite);
		
		playerParameter.Money += (usingItem.Cost / 2);
	}
}

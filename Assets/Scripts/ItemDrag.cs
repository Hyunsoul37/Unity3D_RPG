using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{
	public Transform image;

	private Image EmptyImage;
	private Slot slot;

	private void Start()
	{
		slot = GetComponent<Slot>();
		image = GameObject.FindGameObjectWithTag("DragItem").transform;
		EmptyImage = image.GetComponent<Image>();
	}

	public void Down()
	{
		if (!slot.isSlots())
			return;

		if(Input.GetMouseButtonDown(1))
		{
			if (CheckShop())
			{
				slot.SellItem();
				return;
			}

			slot.UsedItem();
			return;
		}

		image.gameObject.SetActive(true);

		float size = slot.transform.GetComponent<RectTransform>().sizeDelta.x;
		EmptyImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
		EmptyImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);

		EmptyImage.sprite = slot.ItemReturn().image;
		image.transform.position = Input.mousePosition;
		slot.UpDateinfo(true, slot.DefaultImage);
		slot.text.text = "";
	}

	public void Drag()
	{
		if (!slot.isSlots())
			return;

		image.transform.position = Input.mousePosition;
	}

	public void DragEnd()
	{
		if (!slot.isSlots())
			return;

		Inventory inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

		inventory.Swap(slot, image.transform.position);
	}

	public void Up()
	{
		if (!slot.isSlots())
			return;

		image.gameObject.SetActive(false);
		slot.UpDateinfo(true, slot.slot.Peek().image);
	}

	private bool CheckShop()
	{
		return GameObject.FindGameObjectWithTag("ShopDisplay").GetComponent<ShopDisplay>().Window.activeInHierarchy;
	}
}

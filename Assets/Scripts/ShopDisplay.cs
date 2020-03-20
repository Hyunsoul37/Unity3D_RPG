using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
	public GameObject ShopSlot;
	public GameObject Window;
	public Text Coin_Text;

	public List<Item> ItemList = new List<Item>();
	private List<GameObject> SlotList = new List<GameObject>();

	private ShopSlot shopslot;
	private int slot_row = 0;
	private float Shopslotsize = 40f;

	public bool isActive = false;

	private void Start()
	{
		slot_row = ItemList.Count;
		
		MakeShopSlots();

		Window.SetActive(isActive);
	}

	private void MakeShopSlots()
	{
		for(int i = 0; i < slot_row; i++)
		{
			GameObject slot = Instantiate(ShopSlot, Window.transform.position + new Vector3(0, -i * Shopslotsize) * this.transform.localScale.x, Quaternion.identity, Window.transform);

			SlotList.Add(slot);

			slot.GetComponent<ShopSlot>().InitDisplay(ItemList[i]);
		}
	}

	public void SetCoin(int Coin)
	{
		Coin_Text.text = "Coin : " + Coin;
	}

	public void ExitButton()
	{
		Window.SetActive(false);
		UIManager.Getinstance().Panel.SetActive(true);
	}
}

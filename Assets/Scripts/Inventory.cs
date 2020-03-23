using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public List<GameObject> SlotList = new List<GameObject>();

	public GameObject SlotOrigin;
	public GameObject Window;

	private int slotcount_X = 5;
	private int slotcount_Y = 7;

	private const int slot_size = 40;

	private int EmptySlot;

	public bool isActive = false;

	private void Start()
	{
		MakeInventory();
		Window.SetActive(isActive);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			ToggleDisplay();
		}
	}

	private void MakeInventory()
	{
		for (int y = 0; y < slotcount_Y; y++)
		{
			for (int x = 0; x < slotcount_X; x++)
			{
				GameObject Slot = Instantiate(SlotOrigin, 
					Window.transform.position + new Vector3(x * slot_size, -y * slot_size) * this.transform.localScale.x,
					Quaternion.identity, Window.transform);

				Slot.name = "slot_" + y + "_" + x;

				SlotList.Add(Slot);
			}
		}

		EmptySlot = SlotList.Count;
	}

	private void ToggleDisplay()
	{
		isActive = !isActive;

		Window.SetActive(isActive);
	}

	public bool AddItem(Item item)
	{
		int slotCount = SlotList.Count;

		for(int i = 0; i < slotCount; i++)
		{
			Slot slot = SlotList[i].GetComponent<Slot>();

			if (!slot.isSlots())
				continue;

			if(slot.ItemReturn().Item_ID == item.Item_ID && slot.ItemMax(item))
			{
				slot.AddItem(item);
				return true;
			}
		}

		for(int i = 0; i < slotCount; i++)
		{
			Slot slot = SlotList[i].GetComponent<Slot>();

			if (slot.isSlots())
				continue;

			slot.AddItem(item);
			return true;
		}

		return false;
	}

	public Slot NearDistanceSlot(Vector3 Pos)
	{
		float min = 100000f;
		int index = -1;

		int Count = SlotList.Count;

		for(int i = 0; i < Count; i++)
		{
			Vector2 sPos = SlotList[i].transform.GetChild(0).position;
			float Distance = Vector2.Distance(sPos, Pos);

			if (Distance < min)
			{
				min = Distance;
				index = i;
			}
		}

		if (min > slot_size)
			return null;

		return SlotList[index].GetComponent<Slot>();
	}

	public void Swap(Slot _slot, Vector3 Pos)
	{
		Slot firstslot = NearDistanceSlot(Pos);

		if (_slot == firstslot || firstslot == null)
		{
			_slot.UpDateinfo(true, _slot.slot.Peek().image);
			return;
		}

		if (!firstslot.isSlots())
			Swap(firstslot, _slot);
		else
		{
			int Count = _slot.slot.Count;
			Item item = _slot.slot.Peek();

			Stack<Item> tmp = new Stack<Item>();

			for(int i = 0; i < Count; i++)
			{
				tmp.Push(item);
			}

			_slot.slot.Clear();

			Swap(_slot, firstslot);

			Count = tmp.Count;
			item = tmp.Peek();

			for(int i = 0; i < Count; i++)
			{
				firstslot.slot.Push(item);
			}

			firstslot.UpDateinfo(true, tmp.Peek().image);
		}
	}

	public void Swap(Slot first, Slot second)
	{
		int Count = second.slot.Count;
		Item item = second.slot.Peek();

		for(int i = 0; i < Count; i++)
		{
			if(first != null)
			{
				first.slot.Push(item);
			}
		}

		if(first != null)
		{
			first.UpDateinfo(true, second.ItemReturn().image);
		}

		second.slot.Clear();
		second.UpDateinfo(false, second.DefaultImage);
	}
}

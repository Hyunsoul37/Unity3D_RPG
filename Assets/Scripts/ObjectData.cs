using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
	public enum ObjectType { Monster, NPC, Item, Extra, };

	public int Obj_ID;
	public ObjectType type;
}
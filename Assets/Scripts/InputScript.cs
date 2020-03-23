using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
	GameObject Player;

	private void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		CheckClick();
	}

	private void CheckClick()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit rayhit;

			//UI를 클릭하면 움직일수 없게하는 코드
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == true)
				return;

			if (Physics.Raycast(ray, out rayhit))
			{
				if (rayhit.collider.gameObject.name == "Terrain")
				{
					//Player.transform.position = rayhit.point;
					Player.GetComponent<PlayerFSM>().MoveTo(rayhit.point);
				}
				else if (rayhit.collider.gameObject.CompareTag("Enemy"))
				{
					Player.GetComponent<PlayerFSM>().AttackEnemy(rayhit.collider.gameObject);
				}
				else if(rayhit.collider.gameObject.CompareTag("NPC"))
				{
					Player.GetComponent<PlayerFSM>().ConverseNPC(rayhit.collider.gameObject);
				}
				else if(rayhit.collider.gameObject.CompareTag("Tree"))
				{
					Player.GetComponent<PlayerFSM>().CutDownTree(rayhit.collider.gameObject);
				}
			}
		}
	}
}

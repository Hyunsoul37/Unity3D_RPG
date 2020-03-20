using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
	public Transform player;

	private Vector3 offset;

	private void Start()
	{
		offset = player.position - this.transform.position;
	}

	private void LateUpdate()
	{
		this.transform.position = player.position - offset;
	}


}

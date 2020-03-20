using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
	Transform cameraPos;

	private void Start()
	{
		cameraPos = Camera.main.transform;
	}

	private void Update()
	{
		transform.LookAt(transform.position + cameraPos.rotation * Vector3.forward, cameraPos.rotation * Vector3.up);
	}
}

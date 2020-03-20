using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMenu : MonoBehaviour
{
	public GameObject MenuWindow;
	private bool isActive = false;

	private void Start()
	{
		MenuWindow.SetActive(isActive);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isActive = !isActive;
			MenuWindow.SetActive(isActive);
		}
	}

	public void EixtButton()
	{
		Application.Quit();
	}

	public void ContinueButton()
	{
		isActive = false;
		MenuWindow.SetActive(isActive);
	}
}

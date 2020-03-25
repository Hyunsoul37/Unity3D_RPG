using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationUI : MonoBehaviour
{
	private static InformationUI instance;

	public static InformationUI Getinstance()
	{
		if (instance == null)
		{
			instance = FindObjectOfType<InformationUI>();

			if (instance == null)
				Debug.LogError("There Needs to one Active InformationUI Script a GameObject in your Scene");

			return instance;
		}

		return instance;
	}

	public GameObject Info_Window;
	public GameObject CastingBar;
	public Text Info_Text;
	public Image[] images;

	private void Start()
	{
		images = CastingBar.GetComponentsInChildren<Image>();
		Info_Window.SetActive(false);
	}
}

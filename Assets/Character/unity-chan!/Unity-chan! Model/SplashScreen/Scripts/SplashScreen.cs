using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace UnityChan
{
	[ExecuteInEditMode]
	public class SplashScreen : MonoBehaviour
	{
		void NextLevel ()
		{
			Application.LoadLevel (Application.loadedLevel + 1);
		}
	}
}
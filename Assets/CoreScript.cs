using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
	public class CoreScript : MonoBehaviour
	{
		void onHealthDeplete()
		{
			SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
		}
	}
}

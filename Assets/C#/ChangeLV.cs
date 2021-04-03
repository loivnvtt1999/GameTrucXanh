using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLV : MonoBehaviour
{
	public void ChangeScene(string sceneName)
	{
		//SceneManager.Lo
		SceneManager.LoadScene(name);
		//SceneManager.CreateScene()
	}
	public void Exit()
	{
		Application.Quit();
	}
}

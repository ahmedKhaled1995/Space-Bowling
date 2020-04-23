using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadLevel(string level){
		//Application.LoadLevel (level);
		SceneManager.LoadScene (level);
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void LoadNextLevel(){
		//int currentLevel = Application.loadedLevel;
		int currentLevel =  SceneManager.GetActiveScene().buildIndex;
		//Application.LoadLevel (currentLevel + 1);
		SceneManager.LoadScene(currentLevel + 1);
	}


}

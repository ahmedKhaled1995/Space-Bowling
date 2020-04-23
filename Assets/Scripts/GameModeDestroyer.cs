using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeDestroyer : MonoBehaviour {
	private GameMode gameMode;

	// Use this for initialization
	void Start () {
		gameMode = GameObject.FindObjectOfType<GameMode> ();
		if(gameMode){
			Destroy (gameMode.gameObject);	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

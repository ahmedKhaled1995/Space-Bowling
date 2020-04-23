using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour {
	public bool isTwoPlayerGame;
	public Text playerName;
	public Text PlayerOneName;
	public Text playerTwoName;
	public int highestPlayerScore;  // gets set form GameManager when player has the finised bowling
	public string highestPlayerName;    // gets set form GameManager when player has the finised bowling
	public string skin;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		isTwoPlayerGame = false;
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		highestPlayerScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TwoPlayerGame(){
		this.isTwoPlayerGame = true;
		levelManager.LoadNextLevel ();
	}

	public void SingleplayerGame(){
		this.isTwoPlayerGame = false;
		levelManager.LoadNextLevel ();
	}

	public string GetPlayername(){
		if (playerName.text != "") {
			return playerName.text;
		} else {
			return "Player";
		}
	}

	public string GetPlayerOneName(){
		if (PlayerOneName.text != "") {
			return PlayerOneName.text;
		} else {
			return "Player One";
		}
	}

	public string GetPlayerTwoName(){
		if (playerTwoName.text != "") {
			return playerTwoName.text;
		} else {
			return "Player Two";
		}
	}

	public void Marble(){
		skin = "Marble";
	}

	public void Jupiter(){
		skin = "Jupiter";
	}
}

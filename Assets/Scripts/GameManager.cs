using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public GameObject playerOnePrefab;
	public GameObject playerTwoPrefab;

	private bool isTwoPlayerGame;
	private GameMode gameMode;
	private SinglePlayer singlePlayer;
	private MultiPlayer multiPlayer;
	private ScoreDisplay scoreDisplayPlayerTwo;

	void Start(){
		gameMode = GameObject.FindObjectOfType<GameMode> ();
		scoreDisplayPlayerTwo = GameObject.Find ("Score Panel Player Two").GetComponent<ScoreDisplay> ();
		isTwoPlayerGame = gameMode.isTwoPlayerGame;
		if (isTwoPlayerGame) {
			Instantiate (playerTwoPrefab);
			multiPlayer = GameObject.FindObjectOfType<MultiPlayer> ();
		} else {
			Instantiate (playerOnePrefab);
			singlePlayer = GameObject.FindObjectOfType<SinglePlayer> ();
			Destroy (scoreDisplayPlayerTwo.gameObject);
		}
	}

	public void Bowl(int fallenPins){
		if (isTwoPlayerGame) {
			multiPlayer.Bowl (fallenPins);
		} else {
			singlePlayer.Bowl (fallenPins);
		}
	}
}
	
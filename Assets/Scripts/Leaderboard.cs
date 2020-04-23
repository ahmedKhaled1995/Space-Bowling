using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {
	public Text championName;
	public Text championScore;
	public Text playerScore;

	private GameMode gameMode;
	private PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		gameMode = GameObject.FindObjectOfType<GameMode> ();
		playerManager = GameObject.FindObjectOfType<PlayerManager> ();
		string champion_name = playerManager.GetHighestScorePlayerName();
		int champion_score = playerManager.GetHighestScorePlayerScore();
		if (champion_name == "") {
			championName.text = gameMode.highestPlayerName;
			championScore.text = gameMode.highestPlayerScore.ToString();
			playerScore.text = gameMode.highestPlayerScore.ToString();
			playerManager.SetHighestScorePlayerName (gameMode.highestPlayerName);
			playerManager.SetHighestScorePlayerScore (gameMode.highestPlayerScore);
		} else {
			if (gameMode.highestPlayerScore >= champion_score) {
				championName.text = gameMode.highestPlayerName;
				championScore.text = gameMode.highestPlayerScore.ToString();
				playerManager.SetHighestScorePlayerName (gameMode.highestPlayerName);
				playerManager.SetHighestScorePlayerScore (gameMode.highestPlayerScore);
			} else {
				championName.text = playerManager.GetHighestScorePlayerName();
				championScore.text = playerManager.GetHighestScorePlayerScore ().ToString ();
				playerScore.text = gameMode.highestPlayerScore.ToString();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

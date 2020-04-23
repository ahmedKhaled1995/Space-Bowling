using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiPlayer : MonoBehaviour {
	public Material jupiterTexture;

	private Text playerOneNameText;
	private Text playerTwoNameText;
	private Ball ball;
	private PinSetter pinSetter;
	private ScoreDisplay scoreDisplayPlayerOne;
	private ScoreDisplay scoreDisplayPlayerTwo;
	private List<int> bowlsPlayerOne;
	private List<int> bowlsPlayerTwo;
	private string playerOneName;
	private string playerTwoName;
	private bool thisPlayerTurn;    // used in two player game only, starts true to indicate player one turn
	private bool hasPlayerOneFinishedBowling;
	private bool hasPlayerTwoFinishedBowling;
	private int playerOneFinalScore;
	private int playerTwoFinalScore;
	private Color yellowBoardColor;
	private Color originalBoardColor;
	private LevelManager levelManager;
	private GameMode gameMode;
	private Material marbleInitialTexture;


	// Use this for initialization
	void Start () {
		playerOneNameText = GameObject.Find ("Player one name").GetComponent<Text>();
		playerTwoNameText = GameObject.Find ("Player two name").GetComponent<Text>();
		ball = GameObject.Find ("Ball").GetComponent<Ball>();
		pinSetter = GameObject.Find ("Pin Setter").GetComponent<PinSetter> ();
		scoreDisplayPlayerOne = GameObject.Find ("Score Panel Player One").GetComponent<ScoreDisplay> ();
		scoreDisplayPlayerTwo = GameObject.Find ("Score Panel Player Two").GetComponent<ScoreDisplay> ();
		gameMode = GameObject.FindObjectOfType<GameMode> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		originalBoardColor = scoreDisplayPlayerOne.gameObject.GetComponent<Image> ().color;
		yellowBoardColor = Color.yellow;
		marbleInitialTexture = ball.gameObject.GetComponent<MeshRenderer> ().material;
		playerOneName = gameMode.GetPlayerOneName ();
		playerTwoName = gameMode.GetPlayerTwoName ();
		bowlsPlayerOne = new List<int> ();
		bowlsPlayerTwo = new List<int> ();
		playerOneNameText.text = playerOneName;
		playerTwoNameText.text = playerTwoName;
		thisPlayerTurn = true;
		hasPlayerOneFinishedBowling = false;
		hasPlayerTwoFinishedBowling = false;
		playerOneFinalScore = 0;
		playerTwoFinalScore = 0;
		scoreDisplayPlayerOne.gameObject.GetComponent<Image> ().color = yellowBoardColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Bowl(int fallenPins){
		if (thisPlayerTurn) {   // player one turn
			PlayerMoveTwoPlayers(fallenPins, bowlsPlayerOne, hasPlayerOneFinishedBowling, hasPlayerTwoFinishedBowling, scoreDisplayPlayerOne, scoreDisplayPlayerTwo, playerOneFinalScore);
		} else {     // player two turn  
			PlayerMoveTwoPlayers(fallenPins, bowlsPlayerTwo, hasPlayerTwoFinishedBowling, hasPlayerOneFinishedBowling, scoreDisplayPlayerTwo, scoreDisplayPlayerOne, playerTwoFinalScore);
		}
	}

	private void PlayerMoveTwoPlayers(int fallenPins, List<int> bowls, bool hasThisPlayerFinishedBowling, bool hasTheOtherPlayerFinishedBowling,
		ScoreDisplay playerScoreDisplay, ScoreDisplay otherPlayerScoreDisplay, int playerFinalScore){
		bowls.Add (fallenPins);
		ActionMaster.Action nextAction = ActionMaster.NextAction (bowls);
		pinSetter.PinsHaveSetteled (nextAction);
		ball.Reset ();
		List<int> modBowls = ScoreMaster.ScoreFramesHelper (bowls);
		playerScoreDisplay.FillRolls (modBowls);
		playerScoreDisplay.FillFrames (modBowls);
		if(nextAction == ActionMaster.Action.EndTurn){
			thisPlayerTurn = !thisPlayerTurn;
			SwitchBallSkin ();
			playerScoreDisplay.gameObject.GetComponent<Image> ().color = originalBoardColor;
			otherPlayerScoreDisplay.gameObject.GetComponent<Image> ().color = yellowBoardColor;
		}
		if(nextAction == ActionMaster.Action.EndGame){
			hasThisPlayerFinishedBowling = true;
			if(hasTheOtherPlayerFinishedBowling){
				playerFinalScore = playerScoreDisplay.finalScore;
				SetHighestScoringPlayerInGameMode ();
				Invoke ("LoadNextLevel", 5f);   // loads leaderboard scene
			}
		}
	}

	public void SetHighestScoringPlayerInGameMode(){
		if (playerOneFinalScore >= playerTwoFinalScore) {
			gameMode.highestPlayerScore = playerOneFinalScore;
			gameMode.highestPlayerName = playerOneName;
		} else {
			gameMode.highestPlayerScore = playerTwoFinalScore;
			gameMode.highestPlayerName = playerTwoName;
		}
	}

	private void SwitchBallSkin(){
		if(ball.gameObject.GetComponent<MeshRenderer> ().material == marbleInitialTexture){
			ball.gameObject.GetComponent<MeshRenderer> ().material = jupiterTexture;
		}else{
			ball.gameObject.GetComponent<MeshRenderer> ().material = marbleInitialTexture;
		}
	}

	private void LoadNextLevel(){
		levelManager.LoadNextLevel ();
	}
}
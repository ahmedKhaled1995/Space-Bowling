using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinglePlayer : MonoBehaviour {
	public Material marbleTexture;
	public Material jupiterTexture;

	private Text playerNameText; 
	private List<int> bowlsList;
	private Ball ball;
	private PinSetter pinSetter;
	private ScoreDisplay scoreDisplay;
	private GameMode gameMode;
	private string playerName;
	private LevelManager levelManager;
	private int playerFinalScore;


	// Use this for initialization
	void Start () {
		playerNameText = GameObject.Find ("Player one name").GetComponent<Text>();
		ball = GameObject.Find ("Ball").GetComponent<Ball>();
		pinSetter = GameObject.Find ("Pin Setter").GetComponent<PinSetter> ();
		scoreDisplay = GameObject.Find ("Score Panel Player One").GetComponent<ScoreDisplay> ();
		gameMode = GameObject.FindObjectOfType<GameMode> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		bowlsList = new List<int> ();
		playerName = gameMode.GetPlayername ();
		playerNameText.text = playerName;
		playerFinalScore = 0;
		SetUpBallSkin ();
	}

	public void Bowl(int fallenPins){
		bowlsList.Add (fallenPins);
		ball.Reset ();
		List<int> modListPlayerOne = ScoreMaster.ScoreFramesHelper (bowlsList);
		scoreDisplay.FillRolls (modListPlayerOne);
		scoreDisplay.FillFrames (modListPlayerOne);
		ActionMaster.Action nextAction = ActionMaster.NextAction (this.bowlsList);
		pinSetter.PinsHaveSetteled (nextAction);
		if(nextAction == ActionMaster.Action.EndGame){
			playerFinalScore = scoreDisplay.finalScore;
			SetHighestScoringPlayerInGameMode ();
			Invoke ("LoadNextLevel", 5f);   // loads leaderboard scene
		}
	}

	public void SetHighestScoringPlayerInGameMode(){
		gameMode.highestPlayerScore = playerFinalScore;
		gameMode.highestPlayerName = playerName;

	}

	private void LoadNextLevel(){
		levelManager.LoadNextLevel ();
	}

	private void SetUpBallSkin(){
		if(gameMode.skin == "Marble"){
			ball.gameObject.GetComponent<MeshRenderer> ().material = marbleTexture;
		}else if(gameMode.skin == "Jupiter"){
			ball.gameObject.GetComponent<MeshRenderer> ().material = jupiterTexture;
		}
	}
}

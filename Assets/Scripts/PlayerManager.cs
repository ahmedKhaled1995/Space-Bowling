using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	private string nameKey = "nameKey";
	private string scoreKey = "scoreKey";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public  void SetHighestScorePlayerName(string name){
		PlayerPrefs.SetString (this.nameKey, name);
	}

	public  string GetHighestScorePlayerName(){
		return PlayerPrefs.GetString (this.nameKey);
	}

	public  void SetHighestScorePlayerScore(int score){
		PlayerPrefs.SetInt (this.scoreKey, score);
	}

	public  int GetHighestScorePlayerScore(){
		return PlayerPrefs.GetInt (this.scoreKey);
	}
}

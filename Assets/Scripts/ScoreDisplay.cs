using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	
	public Text[] rollTexts;    // max length is 21
	public Text[] frameTexts;  // max length is 10
	public int finalScore;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FillRolls(List<int> rolls){
		string rollsTextValue = FormatRolls (rolls);
		for(int i=0, rollsTextLength = rollsTextValue.Length; i<rollsTextLength; i++){
			this.rollTexts [i].text = rollsTextValue [i].ToString();
		}
	}

	// fills the frames and retutnes the last frame score
	public void FillFrames(List<int> rolls){
		List<int> framesScore = ScoreMaster.ScoreCumulative (rolls);
		int framesScoreCount = framesScore.Count;
		for(int i=0; i<framesScoreCount; i++){
			this.frameTexts [i].text = framesScore [i].ToString ();
		}
		if(framesScoreCount == 10){
			finalScore = framesScore [framesScoreCount - 1];
		}
	}

	public static string FormatRolls(List<int> rolls){
		ActionMaster actionMaster = new ActionMaster ();
		actionMaster.SetBowls (rolls.ToArray());
		string toReturn = string.Empty;
		int rollsLength = rolls.Count;
		int frameCount = 0;
		for(int i=0; i<rollsLength; i++){
			if( (i==0) || (i != 20 && i % 2==0) ){
				frameCount = ((i + 1) + 1) / 2;       // note that i+1 gives us the strart bowl throw of this frame
				string frameAnnotation = actionMaster.GetFrameAnnotation(frameCount);
				toReturn += frameAnnotation;
			}
		}
		return toReturn;
	}
}

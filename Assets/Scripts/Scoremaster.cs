using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMaster {

	// returns a list of cumulative scores, like a normal score cards
	public static List<int> ScoreCumulative(List <int> rolls){
		List<int> cumulativeFramesScore = new List<int> ();
		List<int> individualFramesScore = ScoreFrames (rolls);
		for(int i=0; i<individualFramesScore.Count; i++){
			int sum = 0;
			for(int j=0; j<(i+1); j++){
				sum += individualFramesScore [j];
			}
			cumulativeFramesScore.Add(sum);     // cumulativeFramesScore [i] = sum; is wrong because there is no cumulativeFramesScore [i] as the list is initialized empty
		}
		return cumulativeFramesScore;
	}

	// returns a list of individual frame scores, Not cumulative
	public static List<int> ScoreFrames(List <int> rolls){
		ActionMaster actionMaster = new ActionMaster ();
		actionMaster.SetBowls (rolls.ToArray());
		List<int> frameList = new List<int> ();
		int rollsLength = rolls.Count;
		int frameCount = 0;
		for(int i=0; i<rollsLength; i++){
			if( (i==0) || (i != 20 && i % 2==0) ){
				frameCount = ((i + 1) + 1) / 2;       // note that i+1 gives us the strart bowl throw of this frame
				int frameScore = actionMaster.CalculateFrameScore (frameCount);
				if(frameScore != -1){
					frameList.Add (frameScore);
				}
			}
		}
		return frameList;
	}

	// adds 0 to the rolls list after every strike
	public static List<int> ScoreFramesHelper (List <int> rolls){
		List<int> temp = new List<int> ();
		foreach(int roll in rolls ){
			temp.Add (roll);
		}
		for (int i = 0; i < temp.Count; i++) {   // note that I can't write: int rollsLength = rolls.Count; because the list size may grow as the loop iterates
			if ((i == 0) || (i < 18  && i % 2 == 0)) {  // selecting the first throw of every bowl also excluding the entire last frame (it starts at bow[18])
				if (temp [i] == 10) {
					temp.Insert ((i + 1), 0);   // adding 0 after the strike, so if we had 10, 1 we will have 10, 0, 1
					i++;
				}
			}
		}
		return temp;
	}
		
	public static void DebugList(List<int> bowls){
		int[] temp = bowls.ToArray ();
		int i;
		int tempLength = temp.Length;
		string score = "[";
		for(i=0; i<tempLength; i++){
			if (i == tempLength - 1) {
				score += temp [i].ToString () + "]";
			} else {
				score += temp [i].ToString () + ", ";
			}
		}
		Debug.Log (score);
		Debug.Log ("-------------");
	}
}

/*
public static List<int> ScoreCumulative(List <int> rolls){
		List<int> cumulativeFramesScore = new List<int> ();
		int runnigTotal = 0;
		//Debug.Log (ScoreFrames(rolls).Count);
		foreach(int frameScore in ScoreFrames(rolls)){
			//Debug.Log (frameScore);
			runnigTotal += frameScore;
			cumulativeFramesScore.Add (runnigTotal);
		}
		return cumulativeFramesScore;
	}
*/

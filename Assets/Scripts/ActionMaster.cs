 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster{
	public enum Action{Tidy, Reset, EndTurn, EndGame};

	private int bowl = 1;
	private int[] bowls = new int[21];   

	public static Action NextAction(List<int> pinfalls){
		ActionMaster am = new ActionMaster ();
		Action action = new Action ();
		foreach(int pinfall in pinfalls){
			action = am.Bowl (pinfall);
		}
		return action;
	}

	public Action Bowl(int hitPins){
		if(hitPins < 0 || hitPins > 10){throw new UnityException ("Pins hit can never be negative or greater than 10");}  // capture illegal pins
		// capture here if last frame
		if(bowl == 19){
			bowls [bowl - 1] = hitPins;
			bowl++;
			if(hitPins == 10){return Action.Reset;}
			else{return Action.Tidy;}
		}if(bowl == 20){       // checks if the player achieved a strike at bowl number 19 (the previous bowl)
			if (bowls [18] == 10) {
				bowls [bowl - 1] = hitPins;
				bowl++;
				if (hitPins == 10) {return Action.Reset;}
				else {return Action.Tidy;}
			} else {   // player didn't achieve a strike at the frist throw of the last frame (the previous bowl)
				if (bowls [18] + hitPins == 10) {    // a spare at the second throw of the final frame
					bowls [bowl - 1] = hitPins;
					bowl++;
					return Action.Reset;
				} else {
					bowls [bowl - 1] = hitPins;
					return Action.EndGame;
				}
			}
		}if(bowl == 21){
			bowls [bowl - 1] = hitPins;
			return Action.EndGame;
		}
		// capture here for a strike in any frame except the last one
		if(hitPins == 10){                
			if(bowl % 2 != 0){  // a strike, also note I put the 10 in the first throw of the frame to distinguish between a strike and a spare
				bowls[bowl-1] = 10;
				bowls[bowl] = 0;   //technically redundant, but I just want to make sure the computer doesn't put something funny in it
				bowl += 2;
				return Action.EndTurn;
			}
		}
		// capture here for any action to be taken if not a last frame nor a strike	
		if(bowl % 2 == 0){  // meaning in the second throw of a frame
			bowls[bowl-1] = hitPins;
			bowl++;
			return Action.EndTurn;
		}else if(bowl % 2 != 0){  // meaning in the first throw of a frame
			bowls[bowl-1] = hitPins;
			bowl++;
			return Action.Tidy;
		}
		throw new UnityException ("Not sure what action to take");
	}

	public void DebugScore(string str){
		int i;
		int bowlsLength = bowls.Length;
		string score = "[";
		for(i=0; i<bowlsLength; i++){
			if (i == bowlsLength - 1) {
				score += bowls [i].ToString () + "]";
			} else {
				score += bowls [i].ToString () + ", ";
			}
		}
		Debug.Log (str + ": " + score);
	}

	public void SetFrame(int frameNumber){
		this.bowl = (frameNumber * 2) - 1;
	}

	// note that returning -1 means the frame passed has no score yet
	public int CalculateFrameScore(int frame){
		int startBowlOfFrame = (frame * 2) - 1;    // make sure to substract 1 from that value when accessing bowls (score array)
		// checks to see if the given frame is complete (if the frame has two throws)
		int sumOfTheFrameScore = -1;
		try { sumOfTheFrameScore = bowls[startBowlOfFrame - 1] + bowls[startBowlOfFrame]; }
		catch (System.IndexOutOfRangeException) {return -1;}
		// capture last frame here
		if(frame == 10){
			bool spareCondition = (bowls [startBowlOfFrame - 1] != 10) && ((bowls [startBowlOfFrame - 1] + bowls [startBowlOfFrame]) == 10);
			bool strikeCondition = bowls [startBowlOfFrame - 1] == 10;
			if (spareCondition || strikeCondition) {    // checks to see if the last frame has a spare or a strike
				try { return sumOfTheFrameScore + bowls [startBowlOfFrame + 1];}
				catch (System.IndexOutOfRangeException) {return -1;}
			} else {return sumOfTheFrameScore;}    // no spare or a strike in the last frame
		} 
		// no strike or spare, a regular frame that can be calculated
		if( sumOfTheFrameScore < 10){
			return sumOfTheFrameScore;
		// a spare in this frame
		} else if( (bowls[startBowlOfFrame - 1] != 10) && (sumOfTheFrameScore == 10) ){  
			try {return sumOfTheFrameScore + bowls[startBowlOfFrame + 1];}
			catch (System.IndexOutOfRangeException){return -1;}
		// a strike in this frame
		}else if(bowls[startBowlOfFrame - 1] == 10){  
			//ckeck to see if the next throw is also a strike
			int firstThrowAfterStrike = ((frame+1) * 2) - 1;
			try{
				if (bowls [firstThrowAfterStrike - 1] == 10) {  // the player first throw after a strike was also a strike
					int secondThrowAfterStrike = 0;
					if (frame != 9) {secondThrowAfterStrike = ((frame + 2) * 2) - 1;} 
					else {secondThrowAfterStrike = ((frame + 2) * 2) - 2;}   //  this is basically the second throw of the 10th frame 
					return bowls [startBowlOfFrame - 1] + bowls [firstThrowAfterStrike - 1] + bowls [secondThrowAfterStrike - 1];
				} else { return bowls [startBowlOfFrame - 1] + bowls [firstThrowAfterStrike - 1] + bowls [firstThrowAfterStrike];}  // no strike in next frame
			}catch(System.IndexOutOfRangeException) {return -1;}
		}
		return sumOfTheFrameScore;   // should never be reached, it's just there to keep the compiler happy
	}

	public string GetFrameAnnotation(int frame){
		int startBowlOfFrame = (frame * 2) - 1;    // make sure to substract 1 from that value when accessing bowls (score array)
		bool isTwoThrowsAvailable;    // meaning does the frame has two throws or not
		if (startBowlOfFrame < bowls.Length) {
			isTwoThrowsAvailable = true;
		} else {
			isTwoThrowsAvailable = false;
		}
//		try{
//			int frameSum = bowls[startBowlOfFrame - 1] + bowls[startBowlOfFrame];
//			isTwoThrowsAvailable = true;
//		}catch(System.IndexOutOfRangeException){
//			isTwoThrowsAvailable = false;
//		}
		if (frame == 10) {
			bool isThirdThrowAvailable = false;    // available if the player acheives a strike or a throw in the final frame 
			if (startBowlOfFrame + 1 < bowls.Length) {
				isThirdThrowAvailable = true;
			} else {
				isThirdThrowAvailable = false;
			}
//			try{
//				int bowl21 = bowls[startBowlOfFrame + 1];
//				isThirdThrowAvailable = true;
//			}catch(System.IndexOutOfRangeException){
//				isThirdThrowAvailable = false;
//			}
			return GetFrame10AnnotationHelper (startBowlOfFrame, isTwoThrowsAvailable, isThirdThrowAvailable);
		}else {
			return GetFrameAnnotationHelper(frame, startBowlOfFrame, isTwoThrowsAvailable);
		}
	}

	// handles any frame except the 10th frame
	private string GetFrameAnnotationHelper(int frame, int startBowlOfFrame, bool isFrameComplete){
		string toReturn = string.Empty;
		if (bowls[startBowlOfFrame - 1] == 10){   // the player scored a strike
			toReturn += " X";
		}else if(isFrameComplete && bowls[startBowlOfFrame] == 10){   // the player didn't score any thing then scored a spare (knocked down all the pins in the sceond throw of the frame) 
			toReturn += " /";        ////////
		}else if( isFrameComplete && (bowls[startBowlOfFrame-1]+bowls[startBowlOfFrame]==10) ){ // the player scored a spare by knocking down some of the pins in the first throw then the rest in the second throw 
			toReturn += (bowls[startBowlOfFrame-1].ToString() + "/");
		}else if( isFrameComplete && !(bowls[startBowlOfFrame-1]+bowls[startBowlOfFrame]==10) ){    // regular frame (no strikes or spares)
			if(bowls[startBowlOfFrame-1] == 0){
				toReturn += "-";
			}else{
				toReturn += bowls[startBowlOfFrame-1].ToString();
			}if(bowls[startBowlOfFrame] == 0){
				toReturn += "-";
			}else{
				toReturn += bowls[startBowlOfFrame].ToString();
			}
		}else if(!isFrameComplete){   // a frame with only one throw
			if (bowls [startBowlOfFrame - 1] == 0) {
				toReturn += "-";
			} else {
				toReturn += bowls[startBowlOfFrame-1].ToString();
			}
		}
		return toReturn;
	}

	// handles the 10th frame
	private string GetFrame10AnnotationHelper(int startBowlOfFrame, bool isTwoThrowsAvailable, bool isThirdThrowAvailable){
		string toReturn = string.Empty;
		// getting the annotation of the first throw
		if (bowls [startBowlOfFrame - 1] == 10) {
			toReturn += "X";
		} else if (bowls [startBowlOfFrame - 1] == 0) {
			toReturn += "-";
		} else {
			toReturn += bowls [startBowlOfFrame - 1].ToString ();
		}
		// getting the annotation of the second throw if any
		if(isTwoThrowsAvailable){
			if (bowls [startBowlOfFrame] == 10 && (bowls [startBowlOfFrame - 1] == 0)) {    // scoring nothing then knocking down all the pins
				toReturn = " /";
			} else if (bowls [startBowlOfFrame] == 10) {   // double strike
				toReturn += "X";
			} else if (bowls [startBowlOfFrame - 1] + bowls [startBowlOfFrame] == 10 && (bowls [startBowlOfFrame-1] != 10) ) {   // spare
				toReturn += "/";
			} else {             // regular frame
				if (bowls [startBowlOfFrame] == 0) {
					toReturn += "-";
				} else {
					toReturn += bowls [startBowlOfFrame].ToString ();
				}
			}
		}
		// getting the annotation of the third throw if any
		if(isThirdThrowAvailable){
			if(bowls [startBowlOfFrame + 1] == 10 && bowls [startBowlOfFrame] == 0 ){  // spare,  the player scoring 0 in the second throw then 10 in the last
				toReturn = toReturn [0] + " /";
			}else if (bowls [startBowlOfFrame + 1] == 10) {    // a strike in the final throw
				toReturn += "X";
			} else if( (bowls [startBowlOfFrame] + bowls [startBowlOfFrame + 1] ==10) && (bowls [startBowlOfFrame] != 10) ){   // a spare in the second throw and the third throw
				toReturn += "/";
			}else if (bowls [startBowlOfFrame + 1] == 0) {
				toReturn += "-";
			} else {
				toReturn += bowls [startBowlOfFrame + 1].ToString ();
			}
		}
		return toReturn;
	}

	public int GetScore(){
		int frame = 0;
		int score = 0;
		for(frame = 1; frame<=10; frame++){
			score += CalculateFrameScore (frame);
		}
		return score;
	}

	public int GetBowl(){
		return this.bowl;
	}

	public int[] GetBowls(){
		int[] bowlsToReturn = new int[21];
		for(int i=0; i<bowl; i++){
			bowlsToReturn[i] = bowls[i];
		}
		return bowlsToReturn;
	}

	public void SetBowls(int[] newBowls){
		this.bowls = newBowls;
	}
}

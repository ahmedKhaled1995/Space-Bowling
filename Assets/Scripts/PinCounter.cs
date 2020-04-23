using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {
	private Text standingPinText;
	private Lane lane;
	private float lastCahngeTime;
	private int lastStandingCount;   // acts as a flag
	private int lastSettledCount;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		standingPinText = GameObject.Find ("Standing Pins").GetComponent<Text>();
		lane = GameObject.Find("LaneBox").GetComponent<Lane> ();
		lastStandingCount = -1;
		lastSettledCount = 10;
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager> ();
	}

	// update is called once per frame
	void Update () {
		standingPinText.text = CountStandingPins ().ToString ();
		if(lane.ballExitedBox){
			UpdateStandingPinsCountWhenSetteled ();
		}
	}

	int CountStandingPins(){
		Pin[] pinsArray = GameObject.FindObjectsOfType<Pin> ();
		int numberOfStandingPins = 0;
		foreach(Pin pin in pinsArray){
			if(pin.IsStanding()){
				numberOfStandingPins++;
			}
		}
		return numberOfStandingPins;
	}

	void UpdateStandingPinsCountWhenSetteled(){
		if(lastStandingCount != CountStandingPins()){
			lastStandingCount = CountStandingPins ();
			lastCahngeTime = Time.time;
			return;
		}
		if( (Time.time - lastCahngeTime) >= 3f){   // 3 seconds has passed since the ball entered the pin setter
			PinsHaveSetteled ();
		}
	}

	// call the animator (PinSetter) from here
	void PinsHaveSetteled(){
		int standing = CountStandingPins ();
		int fallenPins = lastSettledCount - standing;    // try later lastSettledCount - lastStandingCount;
		lastSettledCount = standing;
		gameManager.Bowl (fallenPins);
		standingPinText.color = Color.green;
		lastStandingCount = -1;
		lane.ballExitedBox = false;
	}

	// used in PinSetter class
	public void ResetSettleCount(){
		this.lastSettledCount = 10;
	}

	// Simulates Ball Out OfBox
	public void Fowl(){
		lane.ballExitedBox = true;
	}
}

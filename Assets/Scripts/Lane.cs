using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour {
	public bool ballExitedBox;

	private Text standingPinText;

	// Use this for initialization
	void Start () {
		ballExitedBox = false;
		standingPinText = GameObject.Find ("Standing Pins").GetComponent<Text>();
	}
		
	void OnTriggerExit(Collider col){
		if(col.gameObject.GetComponent<Ball>()){
			ballExitedBox = true;
			standingPinText.color = Color.red;
		}
	}
}

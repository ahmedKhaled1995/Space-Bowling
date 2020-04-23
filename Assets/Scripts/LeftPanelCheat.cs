using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPanelCheat : MonoBehaviour {
	private Ball ball;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Cheat(){
		if(Input.GetMouseButtonDown(1)){
			ball.Launch (new Vector3(0, 0, 1500f));
		}
	}
}

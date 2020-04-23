using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private Ball ball;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindObjectOfType<Ball> ();
		offset = this.transform.position - ball.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(ball.transform.position.z <= 1800f){
			this.transform.position = ball.transform.position + offset;
		}
	}
}

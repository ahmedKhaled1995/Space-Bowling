using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLaunch : MonoBehaviour {
	private Ball ball;
	private float dragStartTime;
	private float dragEndTime;
	private Vector3 dragStartPos;
	private Vector3 dragEndPos;
	private GameObject floor;

	// Use this for initialization
	void Start () {
		ball = this.GetComponent<Ball> ();
		floor = GameObject.Find ("Floor");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DragStart(){
		dragStartTime = Time.time;
		dragStartPos = Input.mousePosition;
	}

	public void DragEnd(){
		dragEndTime = Time.time;
		dragEndPos = Input.mousePosition;
		CalculateSpeedAndlaunchBall ();
	}

	void CalculateSpeedAndlaunchBall(){
		float dragTime = dragEndTime - dragStartTime;
		float xVelocity = (dragEndPos.x - dragStartPos.x) / dragTime;
		float zVelocity = Mathf.Abs ((dragEndPos.y - dragStartPos.y)) / dragTime;
		ball.Launch (new Vector3((xVelocity/2), 0, (zVelocity*1.30f)));
	}

	// used to move the ball left and right
	public void MoveStart(float xDeltaPos){
		if((int)ball.transform.position.z == (int)ball.initialBallZPos){   // meaning ball hasn't moved yet
			float xDelta = xDeltaPos * Time.deltaTime;
			float newXBallPos = this.transform.position.x + xDelta;
			newXBallPos = Mathf.Clamp (newXBallPos, -(floor.transform.localScale.x/2f), (floor.transform.localScale.x/2f));   //( (floor.transform.localScale.x-this.transform.localScale.x) / 2f)
			Vector3 newBallPos = new Vector3(newXBallPos, this.transform.position.y, this.transform.position.z);
			this.ball.transform.position = newBallPos;
		}
	}
}

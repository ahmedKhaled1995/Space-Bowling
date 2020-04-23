using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public Vector3 initialDirection;
	public float initialBallZPos;
	public bool inPlay;

	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private Vector3 defaultBallPos;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody> ();
		audioSource = this.GetComponent<AudioSource>();
		rigidBody.useGravity = false;
		initialBallZPos = this.transform.position.z;
		defaultBallPos = this.transform.position;
		inPlay = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void Launch(Vector3 launchSpeed){
		if(!inPlay){  // to avoid re-lauching the ball after it has already been launched
			inPlay = true;
			rigidBody.useGravity = true;
			rigidBody.velocity = launchSpeed;
			audioSource.Play ();
		}
	}

	public void Reset(){
		Invoke ("TurnInPlayFalse", 7f);   // to prevent the player ftom playing before the tidy or reset of pins animation is not over
		this.rigidBody.useGravity = false;
		this.rigidBody.velocity = new Vector3 (0, 0, 0);
		this.rigidBody.angularVelocity = new Vector3 (0, 0, 0);
		this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));    // sames as Quaternion.identity
		this.transform.position = defaultBallPos;
	}

	private void TurnInPlayFalse(){
		inPlay = false;
	}
		
}

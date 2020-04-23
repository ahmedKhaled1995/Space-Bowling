using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {
	public float standingThreshold;
	public float distanceToRise;

	// Use this for initialization
	void Start () {
		this.standingThreshold = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsStanding(){
		Vector3 pinRotation = this.transform.rotation.eulerAngles;   // to access the rotation of the gameobject, just like accessing the position    
		// note that you can't say  this.transform.rotation because this returns a Quaternion not a Vector3
		float tiltInXAxis = Mathf.Abs(pinRotation.x);
		float tiltInZAxis = Mathf.Abs(pinRotation.z);
		// also note that we are not intersted in the rotation around the y axis
		return (tiltInXAxis < this.standingThreshold && tiltInZAxis < this.standingThreshold);
	}

	 public void Raise(){
	    if(this.IsStanding()){
			this.GetComponent<Rigidbody> ().useGravity = false;
			this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));  // to reset the pin rotation
			this.transform.Translate(new Vector3 (0, distanceToRise, 0), Space.World);

		}
		
	}

	public void Lower(){
		this.transform.Translate(new Vector3 (0, -distanceToRise, 0), Space.World);
		this.GetComponent<Rigidbody> ().useGravity = true;
	}

}

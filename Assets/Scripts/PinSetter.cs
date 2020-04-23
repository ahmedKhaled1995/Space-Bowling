using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
	public GameObject pinsPrefab;

	private Animator animator;
	private PinCounter pinCounter;
	private Vector3 pinsInitialPos;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		pinCounter = GameObject.Find ("LaneBox").GetComponent<PinCounter>();
		pinsInitialPos = GameObject.Find ("Bowl Pins").transform.position;
	}

	public void PinsHaveSetteled(ActionMaster.Action actionToTake){
		if(actionToTake == ActionMaster.Action.EndGame){
			//throw new UnityException ("End Game");
			this.ResetTrigger ();
			pinCounter.ResetSettleCount ();
		}
		if (actionToTake == ActionMaster.Action.Tidy) {
			this.TidyTrigger ();
		} else {
			this.ResetTrigger ();
			pinCounter.ResetSettleCount ();
		}
	}
		
	void OnTriggerExit(Collider col){
		if(col.gameObject.GetComponentInParent<Pin>()){
			Destroy (col.gameObject.transform.parent.gameObject);
		}
	}

	public void TidyTrigger(){
		animator.SetTrigger ("TidyTrigger");
	}

	public void ResetTrigger(){
		animator.SetTrigger ("ResetTrigger");
	}

	public void RaisePins(){
		Pin[] pinsArray = GameObject.FindObjectsOfType<Pin> ();
		foreach(Pin pin in pinsArray){
			pin.Raise ();
		}
	}

	public void LowerPins(){
		Pin[] pinsArray = GameObject.FindObjectsOfType<Pin> ();
		foreach(Pin pin in pinsArray){
			pin.Lower ();
		}
	}

	public void RenewPins(){
		Destroy (GameObject.FindGameObjectWithTag("Bowl_Pins"));
		Vector3 offset = new Vector3 (0, 80f, 0);
		Vector3 spawnPos = pinsInitialPos + offset;
		Instantiate(pinsPrefab, spawnPos, Quaternion.identity);
	}
}

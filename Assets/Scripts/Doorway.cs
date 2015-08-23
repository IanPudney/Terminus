﻿using UnityEngine;
using System.Collections;

public class Doorway : MonoBehaviour {
	public int doorNumber;
	Vector3 OpenLocation;
	Vector3 ClosedLocation;
	bool Active;
	
	const float PRIORITY = 2f;
	
	public Vector3 CloseDirection;
	
	public enum State {
		Open,
		Closed,
	};
	public State state;
	State defaultState;
	
	// Use this for initialization
	void Start () {
		defaultState = state;
		GetComponentInChildren<TextMesh>().text = doorNumber.ToString ();

		OpenLocation = transform.position;
		ClosedLocation = OpenLocation + CloseDirection;
		if (CloseDirection == Vector3.up) {
			GameObject a = Instantiate(ProtoDict.obj.doorBrace,
								 transform.position,
							 Quaternion.Euler(Vector3.zero)) as GameObject;
			a.transform.parent = ProtoDict.obj.transform;
		} else if (CloseDirection == Vector3.down) {
			GameObject a = Instantiate(ProtoDict.obj.doorBrace,
												transform.position,
												Quaternion.Euler(Vector3.forward * 180f)) as GameObject;
			a.transform.parent = ProtoDict.obj.transform;
		} else if (CloseDirection == Vector3.right) {
			GameObject a = Instantiate(ProtoDict.obj.doorBrace,
												transform.position,
												Quaternion.Euler(Vector3.forward * 270f)) as GameObject;
			a.transform.parent = ProtoDict.obj.transform;
		} else if (CloseDirection == Vector3.left) {
			GameObject a = Instantiate(ProtoDict.obj.doorBrace,
												transform.position,
												Quaternion.Euler(Vector3.forward * 90f)) as GameObject;
			a.transform.parent = ProtoDict.obj.transform;
		}
		if (state == State.Open) {
			Active = false;
		} else {
			state = State.Open;			
//		TimeControl.OnSetup += DoorClose;
		}
	}
	
	//Don't touch this ever.	
	void OnEnable() {
		TimeControl.OnStop += StopAction;
	}
	
	//Don't touch this ever.
	void OnDisable() {
		TimeControl.OnStop -= StopAction;
	}
	
	void Update() {
		if (Active) {
			if (state == State.Open) {
				transform.position =
						OpenLocation + TimeControl.obj.transition_fraction * (ClosedLocation - OpenLocation);
			} else {
				transform.position =
					ClosedLocation + TimeControl.obj.transition_fraction * (OpenLocation - ClosedLocation);
			}
		}
	}
	
	public void DoorOpen() {
		if (state == State.Closed) {
			Active = true;
		}
	}
	
	public void DoorClose() {
		if (state == State.Open) {
			Active = true;
		TimeControl.OnSetup -= DoorClose;
		}
	}
	
	//That typo is intentional.
	public void DoorToogle() {
		if (state == State.Open) {
//			DoorClose ();
		} else if (state == State.Closed){
//			DoorOpen();
		}
	}
	
	public void StartAction() {
		if (state == State.Open) {
			//Whee
		}
	}
	
	public void StopAction() {
		if (Active) {
			Active = false;
			if (state == State.Open) {
				state = State.Closed;
				transform.position = ClosedLocation;
			} else {
				state = State.Open;
				transform.position = OpenLocation;
			}
		}
	}
	
	public void Reset() {
		if (defaultState == State.Open) {
			transform.position = OpenLocation;
		} else {
			transform.position = ClosedLocation;
		}
		Active = false;
	}
}
﻿using UnityEngine;
using System.Collections;

public class Doorway : MonoBehaviour {
	Vector3 OpenLocation;
	Vector3 ClosedLocation;
	bool Active;
	GameObject DoorBrace;
	
	const float PRIORITY = 2f;
	
	public enum CloseDirection {
		North,
		South,
		East,
		West
	};
	public CloseDirection closeDirection = CloseDirection.North;
	
	enum State {
		Open,
		Closed,
	};
	State state;
	
	// Use this for initialization
	void Start () {
		OpenLocation = transform.position;
		if (closeDirection == CloseDirection.North) {
			ClosedLocation = OpenLocation + Vector3.up;
			DoorBrace = (GameObject)Instantiate(ProtoDict.obj.doorBrace,
											   transform.position,
											   Quaternion.Euler(Vector3.zero));
		} else if (closeDirection == CloseDirection.South) {
			ClosedLocation = OpenLocation + Vector3.down;
			DoorBrace = (GameObject)Instantiate(ProtoDict.obj.doorBrace,
			                                   transform.position,
			                                    Quaternion.Euler(Vector3.forward * 180f));
		} else if (closeDirection == CloseDirection.East) {
			ClosedLocation = OpenLocation + Vector3.right;
			DoorBrace = (GameObject)Instantiate(ProtoDict.obj.doorBrace,
			                                   transform.position,
			                                   Quaternion.Euler(Vector3.forward * 270f));
		} else if (closeDirection == CloseDirection.West) {
			ClosedLocation = OpenLocation + Vector3.left;
			DoorBrace = (GameObject)Instantiate(ProtoDict.obj.doorBrace,
			                                   transform.position,
			                                    Quaternion.Euler(Vector3.forward * 90f));
		}
		Active = false;
		state = State.Open;
	}
	
	//Don't touch this ever.	
	void OnEnable() {
		TimeControl.TriggerDoorOpen += DoorOpen;
		TimeControl.TriggerDoorClose += DoorClose;
		TimeControl.TriggerDoorToogle += DoorToogle;
		TimeControl.OnStart += StartAction;
		TimeControl.OnStop += StopAction;
	}
	
	//Don't touch this ever.
	void OnDisable() {
		TimeControl.TriggerDoorOpen -= DoorOpen;
		TimeControl.TriggerDoorClose -= DoorClose;
		TimeControl.TriggerDoorToogle -= DoorToogle;
		TimeControl.OnStart += StartAction;
		TimeControl.OnStop -= StopAction;
	}
	
	void Update() {
		if (Active) {
			if (state == State.Open) {
				transform.position = OpenLocation + TimeControl.obj.fraction * (ClosedLocation - OpenLocation);
			} else {
				transform.position = ClosedLocation + TimeControl.obj.fraction * (OpenLocation - ClosedLocation);
			}
		}
	}
	public void DoorOpen() {
		if (state == State.Closed) {
			Active = true;
			SpaceControl.obj.AttemptAction(PRIORITY * (ClosedLocation - OpenLocation),
										   Mathf.FloorToInt(ClosedLocation.x), Mathf.FloorToInt(ClosedLocation.y));
			SpaceControl.obj.AttemptAction(PRIORITY * (ClosedLocation - OpenLocation),
			       						   Mathf.FloorToInt(OpenLocation.x), Mathf.FloorToInt(OpenLocation.y));
		}
	}
	
	public void DoorClose() {
		if (state == State.Open) {
			Active = true;
			SpaceControl.obj.AttemptAction(PRIORITY * (OpenLocation - ClosedLocation),
			                               Mathf.FloorToInt(ClosedLocation.x), Mathf.FloorToInt(ClosedLocation.y));
			SpaceControl.obj.AttemptAction(PRIORITY * (OpenLocation - ClosedLocation),
			                               Mathf.FloorToInt(OpenLocation.x), Mathf.FloorToInt(OpenLocation.y));
		}
	}
	
	//That typo is intentional.
	public void DoorToogle() {
		if (state == State.Open) {
			DoorClose ();
		} else if (state == State.Closed){
			DoorOpen();
		}
	}
	
	public void StartAction() {
		print ("Starting action");
		if (state == State.Open) {
			Vector3 travelVector = SpaceControl.obj.ResolveAction(
					PRIORITY * (OpenLocation - ClosedLocation),
					Mathf.FloorToInt(ClosedLocation.x),
					Mathf.FloorToInt(ClosedLocation.y));
			if (travelVector.magnitude == 0f) {
				Active = false;
			}
		}
	}
	
	public void StopAction() {
		if (Active) {
			Active = false;
			if (state == State.Open) {
				state = State.Closed;
			} else {
				state = State.Open;
			}
		}
	}
}
using UnityEngine;
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
	
	// Use this for initialization
	void Start () {
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
		TimeControl.OnSetup += DoorClose;
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
		TimeControl.OnSetup -= DoorClose;
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
				transform.position = ClosedLocation;
			} else {
				state = State.Open;
				transform.position = OpenLocation;
			}
		}
	}
}
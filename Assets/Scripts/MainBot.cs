using UnityEngine;
using System.Collections;

public class MainBot : MonoBehaviour {
	Vector3 StartLocation;
	Vector3 StopLocation;
	Quaternion StartRotation;
	Quaternion StopRotation;
	bool Active;
	Vector3 DoorVector;
	
	const float PRIORITY = 1f;

	// Use this for initialization
	void Start () {
		StartLocation = transform.position;
		StopLocation = StartLocation;
		Active = false;
		DoorVector = Vector3.zero;
		StartRotation = transform.rotation;
		StopRotation = StartRotation;
	}
	
	//Don't touch this ever.	
	void OnEnable() {
		TimeControl.TriggerMoveForward += MoveForwardAction;
		TimeControl.TriggerRotateLeft += RotateLeftAction;
		TimeControl.TriggerRotateRight += RotateRightAction;
		TimeControl.OnStart += StartAction;
		TimeControl.OnStop += StopAction;
	}
	
	//Don't touch this ever.
	void OnDisable() {
		TimeControl.TriggerMoveForward -= MoveForwardAction;
		TimeControl.TriggerRotateLeft -= RotateLeftAction;
		TimeControl.TriggerRotateRight -= RotateRightAction;
		TimeControl.OnStart -= StartAction;
		TimeControl.OnStop -= StopAction;
	}
	
	void Update() {
		if (Active) {
			transform.position = StartLocation + TimeControl.obj.fraction * (StopLocation - StartLocation);
			transform.rotation = Quaternion.Lerp(StartRotation, StopRotation, TimeControl.obj.fraction);
		}
	}
	
	public void MoveForwardAction() {
		Vector3 travelVector = transform.up;
		StopLocation = StartLocation + travelVector;
		SpaceControl.obj.AttemptAction(PRIORITY * travelVector,
									   Mathf.FloorToInt (StopLocation.x),
									   Mathf.FloorToInt(StopLocation.y));
		Active = true;
	}
	
	public void RotateLeftAction() {
		StopRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90f));
		SpaceControl.obj.AttemptAction(PRIORITY * Vector3.forward,
		                               Mathf.FloorToInt(transform.position.x),
		                               Mathf.FloorToInt(transform.position.y));
		Active = true;
	}
	
	public void RotateRightAction() {
		StopRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90f));
		SpaceControl.obj.AttemptAction(PRIORITY * Vector3.forward,
		                               Mathf.FloorToInt(transform.position.x),
		                               Mathf.FloorToInt(transform.position.y));
		Active = true;
	}
	
	public void StartAction() {
		Vector3 travelVector = SpaceControl.obj.ResolveAction(
				PRIORITY * (StopLocation - StartLocation),
				Mathf.FloorToInt(StopLocation.x),
				Mathf.FloorToInt(StopLocation.y));
		StopLocation = StartLocation + travelVector;
	}
	
	public void StopAction() {
		StartLocation = StopLocation;
		StartRotation = StopRotation;
		transform.position = StartLocation;
		transform.rotation = StartRotation;
		Active = false;
	}
	
	void OnTriggerEnter(Collider other) {
		Doorway door = other.gameObject.GetComponent<Doorway>();
		if (door) {
			if (DoorVector + door.CloseDirection == Vector3.zero) {
				Time.timeScale = 0.0f;
			} else if (DoorVector == Vector3.zero) {
				DoorVector = door.CloseDirection;
				StopLocation += DoorVector;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class MainBot : MonoBehaviour {
	Vector3 StartLocation;
	Vector3 StopLocation;
	bool Active;
	Vector3 DoorVector;

	// Use this for initialization
	void Start () {
		StartLocation = transform.position;
		StopLocation = StartLocation;
		Active = false;
		DoorVector = Vector3.zero;
	}
	
	//Don't touch this ever.	
	void OnEnable() {
		TimeControl.TriggerMoveForward += MoveForwardAction;
		TimeControl.OnStart += StartAction;
		TimeControl.OnStop += StopAction;
	}
	
	//Don't touch this ever.
	void OnDisable() {
		TimeControl.TriggerMoveForward -= MoveForwardAction;
		TimeControl.OnStart -= StartAction;
		TimeControl.OnStop -= StopAction;
	}
	
	void Update() {
		if (Active) {
			transform.position = StartLocation + TimeControl.obj.fraction * (StopLocation - StartLocation);
		}
	}
	
	public void MoveForwardAction() {
		Vector3 travelVector = Vector3.up;
		StopLocation = StartLocation + travelVector;
		SpaceControl.obj.AttemptAction(travelVector,
									   Mathf.FloorToInt (StopLocation.x),
									   Mathf.FloorToInt(StopLocation.y));
		Active = true;
	}
	
	public void StartAction() {
		Vector3 travelVector = SpaceControl.obj.ResolveAction(
				StopLocation-StartLocation,
				Mathf.FloorToInt(StopLocation.x),
				Mathf.FloorToInt(StopLocation.y));
		StopLocation = StartLocation + travelVector;
	}
	
	public void StopAction() {
		StartLocation = StopLocation;
		transform.position = StartLocation;
		Active = false;
	}
	
	void OnTriggerEnter(Collider other) {
		print ("Fontenot!");
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

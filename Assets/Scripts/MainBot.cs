using UnityEngine;
using System.Collections;

public class MainBot : MonoBehaviour {
	Vector3 StartLocation;
	Vector3 StopLocation;
	bool Active;

	// Use this for initialization
	void Start () {
		StartLocation = transform.position;
		Active = false;
	}
	
	//Don't touch this ever.	
	void OnEnable() {
		TimeControl.OnStart += StartAction;
		TimeControl.OnStop += StopAction;
	}
	
	//Don't touch this ever.
	void OnDisable() {
		TimeControl.OnStart -= StartAction;
		TimeControl.OnStop -= StopAction;
	}
	
	void Update() {
		if (Active) {
			transform.position = StartLocation + TimeControl.obj.fraction * (StopLocation - StartLocation);
		}
	}
	
	public void StartAction() {
		Active = true;
		StopLocation = StartLocation + Vector3.up;
	}
	
	public void StopAction() {
		StartLocation = StopLocation;
		transform.position = StartLocation;
		Active = false;
	}
}

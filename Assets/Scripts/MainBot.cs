using UnityEngine;
using System.Collections;

public class MainBot : MonoBehaviour {
	Vector3 StartLocation;
	Vector3 StopLocation;
	Quaternion StartRotation;
	Quaternion StopRotation;
	bool Active;
	Vector3 DoorVector;
	public int ID;
	[HideInInspector]
	bool dead = false;
		
	const float PRIORITY = 1f;

	// Use this for initialization
	void Start () {
		StartLocation = transform.position;
		StopLocation = StartLocation;
		Active = false;
		DoorVector = Vector3.zero;
		StartRotation = transform.rotation;
		StopRotation = StartRotation;
		ID = ObjectDict.obj.AddToRobotsDict(this);
	}

	void OnEnable() {
		TimeControl.OnStop += StopAction;
	}

	void OnDisable() {
		TimeControl.OnStop -= StopAction;
	}
	
	void FixedUpdate() {
		if (dead) {
			transform.localScale *= TimeControl.obj.decay_ratio;
			GetComponent<Renderer>().material.color *= TimeControl.obj.decay_ratio;
			transform.position -= new Vector3(0, 0, - TimeControl.obj.slider_rate / 30f);
		}
	}
	
	void Update() {
		if (dead) {
			return;
			transform.localScale *= (1f - Time.deltaTime);
			//transform.position -= 0.1f * Time.deltaTime * Vector3.up;
		} else if (Active) {
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
	
	void OnCollisionEnter(Collision other) {
		print(other.transform.name);
		Doorway door = other.gameObject.GetComponent<Doorway>();
		MainBot bot = other.gameObject.GetComponent<MainBot>();
		if (door) {
			if (DoorVector + door.CloseDirection == Vector3.zero) {
				Time.timeScale = 0.0f;
			} else if (DoorVector == Vector3.zero) {
				DoorVector = door.CloseDirection;
				StopLocation += DoorVector;
			}
		} else {
			StopLocation = StartLocation;
		}
	}

	public bool IsOnPaint() {
		foreach (Paint paint in ObjectDict.obj.paints) {
			if(Vector3.Distance(paint.transform.position, transform.position) < 0.1f) {
				return true;
			}
		}
		return false;
	}
	
	public void FallIntoVoid() {
		dead = true;
		TimeControl.OnStop -= StopAction;
	}
}

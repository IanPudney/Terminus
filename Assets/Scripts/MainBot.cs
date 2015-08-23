using UnityEngine;
using System.Collections;

public class MainBot : WorldObject {
	//"Personal" use
	Quaternion StartRotation;
	Quaternion StopRotation;
	Vector3 DoorVector;
	public int ID;
	[HideInInspector]
	bool dead = false;
	
	Quaternion DefaultRotation;
	const float BumpPush = 0.2f;
	
	protected override void Start () {
		base.Start ();
		DoorVector = Vector3.zero;
		StartRotation = transform.rotation;
		StopRotation = StartRotation;
		DefaultRotation = StartRotation;
		ID = ObjectDict.obj.AddToRobotsDict(this);
	}

	void OnEnable() {
		TimeControl.OnStop += StopAction;
		TimeControl.OnStart += StartAction;
		//Temp:
		//TimeControl.OnTelegraph += MoveForwardAction;
		
	}

	void OnDisable() {
		TimeControl.OnStop -= StopAction;
		TimeControl.OnStart -= StartAction;
		//Temp
		//TimeControl.OnTelegraph -= MoveForwardAction;
	}
	
	/*void FixedUpdate() {
		if (dead) {
			transform.localScale *= TimeControl.obj.decay_ratio;
			GetComponentInChildren<Renderer>().material.color *= TimeControl.obj.decay_ratio;
			transform.position -= new Vector3(0, 0, - TimeControl.obj.slider_rate / 30f);
		}
	}*/
	
	void Update() {
		if (dead) {
			return;
			transform.localScale *= (1f - Time.deltaTime);
		} else if (isMoving) {
			transform.position =	new Vector3(xPos + 0.5f, yPos + 0.5f, 0)
									+ TimeControl.obj.transition_fraction * (new Vector3(	futureXPos - xPos,
																				futureYPos - yPos, 0));
			transform.rotation = Quaternion.Lerp(StartRotation, StopRotation, TimeControl.obj.transition_fraction);
		} else if (isColliding) {
			float fraction = Mathf.PingPong(2f * BumpPush * TimeControl.obj.transition_fraction, BumpPush);
			transform.position =	new Vector3(xPos + 0.5f, yPos + 0.5f, 0)
									+ fraction * (new Vector3(	futureXPos - xPos, futureYPos - yPos, 0));
			transform.rotation = Quaternion.Lerp(StartRotation, StopRotation, TimeControl.obj.transition_fraction);
		}
	}
	
	public void MoveForwardAction() {
		Telegraph (	Mathf.FloorToInt(transform.position.x + transform.up.x),
					Mathf.FloorToInt(transform.position.y + transform.up.y));
	}
	
	public void StopAction() {
		Cleanup();
		StartRotation = StopRotation;
		transform.position = new Vector3(xPos + 0.5f, yPos + 0.5f, 0);
		transform.rotation = StartRotation;
		DoorVector = Vector3.zero;
		isMoving = false;
		isColliding = false;
	}
	
	public void RotateLeftAction() {
		StopRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 90f));
	}
	
	public void RotateRightAction() {
		StopRotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, -90f));
	}
	
	public void StartAction() {
		Move ();
	}

	public bool IsOnPaint() {
		foreach (Paint paint in ObjectDict.obj.paints) {
			if(Vector2.Distance(paint.transform.position, transform.position) < 0.1f) {
				return true;
			}
		}
		return false;
	}
	
	public void FallIntoVoid() {
		dead = true;
		TimeControl.OnStop -= StopAction;
	}
	
	public override void Reset() {
		base.Reset();
		dead = false;
		StartRotation = DefaultRotation;
		StopRotation = DefaultRotation;
		transform.rotation = DefaultRotation;
		DoorVector = Vector3.zero;
		transform.localScale = Vector3.one;
		TimeControl.OnStop += StopAction;
	}
}

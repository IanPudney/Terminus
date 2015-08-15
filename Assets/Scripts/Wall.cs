﻿using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	const float PRIORITY = 5f;
	
	void OnEnable() {
	}
		
	void OnDisable() {
	}
	
	public void SendReport() {
		SpaceControl.obj.AttemptAction(PRIORITY * Vector3.forward,
		                               Mathf.FloorToInt(transform.position.x),
		                               Mathf.FloorToInt(transform.position.y));
	}
}

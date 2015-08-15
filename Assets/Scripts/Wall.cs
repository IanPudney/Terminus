using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
	const float PRIORITY = 5f;
	
	void OnEnable() {
		TimeControl.TriggerWallReport += SendReport;
	}
		
	void OnDisable() {
		TimeControl.TriggerWallReport -= SendReport;
	}
	
	public void SendReport() {
		SpaceControl.obj.AttemptAction(PRIORITY * Vector3.forward,
		                               Mathf.FloorToInt(transform.position.x),
		                               Mathf.FloorToInt(transform.position.y));
	}
}

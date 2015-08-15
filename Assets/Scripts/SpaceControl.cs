using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceControl : MonoBehaviour {
	public static SpaceControl obj;
	public int xSize = 10;
	public int ySize = 10;
	
	List<List<List<Vector3>>> actionspace = new List<List<List<Vector3>>>();
	
	// Use this for initialization
	void Awake () {
		obj = this;
		for (int x = 0; x < xSize; ++x) {
			actionspace.Add(new List<List<Vector3>>());
			for (int y = 0; y < ySize; ++y) {
				actionspace[x].Add (new List<Vector3>());
			}
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
	
	public void AttemptAction(Vector3 actionToAttempt, int x, int y) {
		actionspace[x][y].Add (actionToAttempt);
	}
	
	public Vector3 ResolveAction(Vector3 actionToResolve, int x, int y) {
		foreach (Vector3 action in actionspace[x][y]) {
			if (action.normalized == actionToResolve.normalized
					|| action.magnitude < actionToResolve.magnitude) {
				continue;
			} else {
				return Vector3.zero;
			}
		}
		return actionToResolve.normalized;
	}
	
	void StopAction() {
		foreach (List<List<Vector3>> actioncol in actionspace) {
			foreach (List<Vector3> actionlist in actioncol) {
				actionlist.Clear();
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpaceControl : MonoBehaviour {
	public static SpaceControl obj;
	public int xSize = 10;
	public int ySize = 10;
	
	List<List<List<Vector3>>> actionspace = new List<List<List<Vector3>>>();
	
	void Awake () {
		obj = this;
		xSize += 2;
		ySize += 2;
		
		//Configure action space
		for (int x = 0; x < xSize; ++x) {
			actionspace.Add(new List<List<Vector3>>());
			for (int y = 0; y < ySize; ++y) {
				actionspace[x].Add (new List<Vector3>());
			}
		}
	}
	
	void Start() {
	//UICanvas.obj.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);
	//FindObjectOfType<Slider>().transform.position = new Vector3(xSize * 0.5f, ySize + 0.4f, -3f);
	
		//Build walls around the edge to handle undetermined behavior
		for (int x = 0; x < xSize; ++x) {
			GameObject a = Instantiate(ProtoDict.obj.wall,
					new Vector3(x * 1.0f + 0.5f, 0.5f, 0f), Quaternion.identity) as GameObject;
			a.transform.parent = ProtoDict.obj.transform;
			GameObject b = Instantiate(ProtoDict.obj.wall,
					new Vector3(x * 1.0f + 0.5f, ySize * 1.0f - 0.5f, 0f), Quaternion.identity) as GameObject;
			b.transform.parent = ProtoDict.obj.transform;
		}
		for (int y = 1; y < ySize - 1; ++y) {
			GameObject a = Instantiate(ProtoDict.obj.wall,
				new Vector3(0.5f, y * 1.0f + 0.5f, 0f), Quaternion.identity) as GameObject;
			a.transform.parent = ProtoDict.obj.transform;
			GameObject b = Instantiate(ProtoDict.obj.wall,
					new Vector3(xSize * 1.0f - 0.5f, y * 1.0f + 0.5f, 0f), Quaternion.identity) as GameObject;
			b.transform.parent = ProtoDict.obj.transform;
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

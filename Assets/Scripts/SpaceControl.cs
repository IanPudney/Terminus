using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SpaceControl : MonoBehaviour {
	public static SpaceControl obj;
	public int xSize = 10;
	public int ySize = 10;
	
	public List<List<WorldObject>> currentSpace = new List<List<WorldObject>>();
	public List<List<WorldObject>> futureSpace = new List<List<WorldObject>>();
	
	void Awake () {
		obj = this;
		xSize += 2;
		ySize += 2;
		
		//Configure action space
		for (int x = 0; x < xSize; ++x) {
			currentSpace.Add(new List<WorldObject>());
			futureSpace.Add(new List<WorldObject>());
			for (int y = 0; y < ySize; ++y) {
				currentSpace[x].Add (null);
				futureSpace[x].Add (null);
			}
		}
	}
	
	void Start() {
		Camera.main.transform.position = new Vector3(xSize*0.5f, ySize*0.5f - 1f, -20f);
		float minWidth = ((xSize + 4) * Screen.height / Screen.width * 0.5f) / (1f - Camera.main.rect.x);
		float minHeight = (ySize + 4) * 0.5f / (1 - Camera.main.rect.y);
		Camera.main.orthographicSize = Mathf.Max (minWidth, minHeight);
	}
}

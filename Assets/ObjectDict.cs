using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDict : MonoBehaviour {
	public static ObjectDict obj;
	public List<MainBot> robots = new List<MainBot>();
	public List<Paint> paints = new List<Paint>();
	
	void Awake() {
		obj = this;
	}
	
	public int AddToRobotsDict(MainBot robot) {
		robots.Add (robot);
		return robots.Count - 1;
	}

	public int AddToPaintsDict(Paint paint) {
		paints.Add (paint);
		return robots.Count - 1;
	}
}

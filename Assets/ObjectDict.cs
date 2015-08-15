using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectDict : MonoBehaviour {
	public static ObjectDict obj;
	public List<MainBot> robots = new List<MainBot>();
	
	void Awake() {
		obj = this;
	}
	
	public int AddToRobotsDict(MainBot robot) {
		robots.Add (robot);
		return robots.Count - 1;
	}
}

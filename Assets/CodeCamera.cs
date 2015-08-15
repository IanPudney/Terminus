using UnityEngine;
using System.Collections;

public class CodeCamera : MonoBehaviour {
	public static Camera obj;
	void Awake() {
		obj = this.GetComponent<Camera>();
	}
}

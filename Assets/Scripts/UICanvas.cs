using UnityEngine;
using System.Collections;

public class UICanvas : MonoBehaviour {
	public static RectTransform obj;
	void Awake() {
		obj = GetComponent<RectTransform>();
	}
}

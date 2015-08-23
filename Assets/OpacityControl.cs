using UnityEngine;
using System.Collections;

public class OpacityControl : MonoBehaviour {
	void Start () {
		Color colorBase = GetComponent<Renderer>().material.color;
		GetComponent<Renderer>().material.color = new Color(colorBase.r, colorBase.g, colorBase.b, 0.2f);
	}
}

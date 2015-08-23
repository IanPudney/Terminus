using UnityEngine;
using System.Collections;

public class PrintStatement : Statement {
	public string value;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTick() {
		Debug.LogError (value);
	}

	public void OnTelegraph() {
		Debug.LogError ("Telegraph: " + value);
	}
}

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

	public override void OnTick() {
		Debug.Log (value);
		base.OnTick ();
	}
}

using UnityEngine;
using System.Collections;

public class BaseStatement : Statement {

	// Use this for initialization
	void Start () {
		TimeControl.OnStart += OnTick;
		stack = new System.Collections.Stack ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTick() {
		base.OnTick ();
	}
}

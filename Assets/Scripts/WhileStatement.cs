using UnityEngine;
using System.Collections;

public class WhileStatement : Statement {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTick() {
		base.OnTick ();
	}

	public override bool ShouldPop() {
		return false;
	}
}

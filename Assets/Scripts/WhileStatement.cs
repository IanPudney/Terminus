using UnityEngine;
using System.Collections;

public class WhileStatement : StmtBlock {

	// Use this for initialization
	public override void Start () {
		base.Start ();
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

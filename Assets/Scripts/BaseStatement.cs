using UnityEngine;
using System.Collections;

public class BaseStatement : StmtBlock {

	// Use this for initialization
	public override void Start () {
	base.Start();
		TimeControl.OnStart += OnTick;
		stack = new System.Collections.Stack ();
		label.text = "On <color=\"cyan\">Play</color>";
	}
	
	void Update () {
		base.Update();
	}

	public override void OnTick() {
		base.OnTick ();
	}
	
	public override void Reset() {
		base.Reset();
		TimeControl.OnStart += OnTick;
		TimeControl.OnTelegraph += OnTelegraph;
	}
}

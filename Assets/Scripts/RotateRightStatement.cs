﻿using UnityEngine;
using System.Collections;

public class RotateRightStatement : Statement {
	
	public int robotID = 0;
	
	protected override void Start() {
		base.Start ();
		info.text = "rotate ROBO" + (robotID + 1) + " rgt";
	}	
	
	public override void OnTick() {
		Debug.Log ("Rotating robot");
		ObjectDict.obj.robots[robotID].StartAction ();
		base.OnTick ();
	}
	
	public override void OnTelegraph() {
		ObjectDict.obj.robots[robotID].RotateRightAction ();
		base.OnTelegraph ();
	}
}

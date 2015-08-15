﻿using UnityEngine;
using System.Collections;

public class RotateLeftStatement : Statement {
	
	public int robotID = 0;
	
	protected override void Start() {
		base.Start ();
		info.text = "rotate ROBO" + (robotID + 1) + " lft";
	}	
	
	public override void OnTick() {
		Debug.Log ("Rotating robot");
		ObjectDict.obj.robots[robotID].StartAction ();
		base.OnTick ();
	}
	
	public override void OnTelegraph() {
		ObjectDict.obj.robots[robotID].RotateLeftAction ();
		base.OnTelegraph ();
	}
}

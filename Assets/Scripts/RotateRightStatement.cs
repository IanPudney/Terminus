﻿using UnityEngine;
using System.Collections;

public class RotateRightStatement : Statement {
	
	int robotID;
	public int startRobotID;
	public int RobotID
	{
		get
		{
			return robotID;
		}
		set
		{
			robotID = value;
			label.text = "Rotate <color=\"cyan\">R" + robotID.ToString() + "</color> Left";
			Debug.Log("Set label to " + label.text);
		}
	}
	
	protected override void Start() {
		base.Start ();
		RobotID = startRobotID;
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

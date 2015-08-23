using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveStatement : Statement {
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
			label.text = "Move <color=\"cyan\">R" + robotID.ToString() + "</color> Forward 1";
			Debug.Log("Set label to " + label.text);
		}
	}
	
	protected override void Start() {
		base.Start ();
		RobotID = startRobotID;
		TakesTurn = true;
	}

	public override void OnTick() {	
		Debug.Log ("Start Action");
		ObjectDict.obj.robots[robotID].StartAction ();
	}

	public override void OnTelegraph() {
		Debug.Log ("Move Forward Action");
	ObjectDict.obj.robots[robotID].MoveForwardAction ();
	}
}

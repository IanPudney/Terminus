using UnityEngine;
using System.Collections;

public class PaintIfElseStmt : IfElseStmt {
	public MainBot robot;

	protected override void Start() {
		base.Start ();
		ifBlock.SetupLabel().text = "True";
		elseBlock.SetupLabel().text = "False";
		label.text = "If <color=\"cyan\">R" + robot.ID + "</color> On Paint:";
	}

	public override bool Test() {
		return robot.IsOnPaint();
	}
}

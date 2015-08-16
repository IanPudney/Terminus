﻿using UnityEngine;
using System.Collections;

public class PaintIfElseStmt : IfElseStmt {
	public MainBot robot;

	public override void Start() {
		base.Start ();
		ifBlock.SetupLabel().text = "True";
		elseBlock.SetupLabel().text = "False";
		if (robot) {
			SetupLabel().text = "If <color=\"cyan\">R" + robot.ID + "</color> On Paint:";
		}
	}

	public override bool Test() {
		return robot.IsOnPaint();
	}
}

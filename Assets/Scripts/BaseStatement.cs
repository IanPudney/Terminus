using UnityEngine;
using System.Collections;

public class BaseStatement : StmtBlock {

	// Use this for initialization
	public override void Start () {
	base.Start();
		label.text = "On <color=\"cyan\">Play</color>";
		new StatementVisitor(this);
	}
	
	void Update () {
		base.Update();
	}
}

using UnityEngine;
using System.Collections;

public class TriggerStatement : StmtBlock {
	public int buttonNumber;
	public override void Start () {
	base.Start();
		stack = new System.Collections.Stack ();
		label.text = "On <color=\"cyan\">Button " + buttonNumber.ToString () + "</color> Pressed";
	}

	public void setButtonNumber(int x) {
		buttonNumber = x;
		SetupLabel().text = "On <color=\"cyan\">Button " + buttonNumber.ToString () + "</color> Pressed";
	}
	
	void Update () {
		base.Update();
	}

	public override void OnTick() {
		base.OnTick ();
	}
}

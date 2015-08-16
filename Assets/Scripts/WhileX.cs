using UnityEngine;
using System.Collections;

public class WhileX : WhileStatement {
	public int loops = 2;
	// Use this for initialization
	public override void Start () {
		base.Start ();
		label.text = "Repeat <color=\"cyan\">" + loops.ToString () + "</color> Times";
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	int counter = 0;
	public override bool ShouldPop() {
		counter++;
		if(counter >= loops) {
			counter = 0;
			return true;
		}
		return false;
	}
}

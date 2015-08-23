using UnityEngine;
using System.Collections;

public class IfElseStmt : StmtBlock {
	public Statement ifBlock;
	public Statement elseBlock;
	bool initedLayout = false;
	// Use this for initialization
	public override void Start () {
		base.Start ();
		ifBlock.SetupLabel().text = "True";
		elseBlock.SetupLabel().text = "False";
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if(!initedLayout) {
			RecursiveLayout ();
			initedLayout = true;
		}
	}
	public bool tru = true;
	public virtual bool Test() {
		return tru;
	}
}

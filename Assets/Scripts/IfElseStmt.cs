using UnityEngine;
using System.Collections;

public class IfElseStmt : Statement {
	public Statement ifBlock;
	public Statement elseBlock;
	// Use this for initialization
	void Start () {
		base.Start ();
		//todo: move Layout call to Statement from StmtBlock
		//todo: if/else text
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public bool tru = true;
	public virtual bool Test() {
		return tru;
	}

	/*public override bool CallNextChild() {
		//Queue ();
		if(Test ()) {
			TimeControl.OnStart += ifBlock.OnTick;
			TimeControl.OnTelegraph += ifBlock.OnTelegraph;
			ifBlock.stack = stack;
		} else {
			TimeControl.OnStart += elseBlock.OnTick;
			TimeControl.OnTelegraph += elseBlock.OnTelegraph;
			elseBlock.stack = stack;
		}
		return true;
	}*/
	int nextStmt = 0;
	public override bool CallNextChild() {
		while (true) {
			//call the next child, and remove this from the callback
			
			if (nextStmt >= transform.childCount) {
				nextStmt = 0;
				return true;
			}
			Statement child = transform.GetChild (nextStmt).GetComponent<Statement>();
			
			if (child == null || child is Placeholder) {
				nextStmt++;
				continue;
			}
			if(Test ()) {
				if(child != ifBlock) {
					nextStmt++;
					continue;
				}
			} else {
				if(child != elseBlock) {
					nextStmt++;
					continue;
				}
			}
			Queue ();
			Debug.Log ("Length: " + transform.childCount + " stmt: " + nextStmt);
			TimeControl.OnStart += child.OnTick;
			TimeControl.OnTelegraph += child.OnTelegraph;
			child.stack = stack;
			nextStmt ++;
			return false;
		}
	}
}

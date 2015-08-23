using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatementVisitor {
	System.Collections.Generic.Stack<System.Collections.Generic.Queue<Statement>> programStack;

	Statement CurrentStatement() {
		if(programStack.Count == 0) return null;
		if(programStack.Peek () == null) throw new UnityException("Null Queue on program stack");
		if(programStack.Peek ().Count == 0) throw new UnityException("Empty frame on program stack");
		if(programStack.Peek ().Peek () == null) throw new UnityException("Null Instruction on program stack");
		return programStack.Peek ().Peek ();
	}

	public StatementVisitor (Statement entryPoint) {
		programStack = new System.Collections.Generic.Stack<System.Collections.Generic.Queue<Statement>>();
		programStack.Push(new System.Collections.Generic.Queue<Statement>());
		programStack.Peek ().Enqueue(entryPoint);
		TimeControl.OnStartCycle += OnStartCycle;
		TimeControl.OnTelegraph += OnTelegraph;
		TimeControl.OnStart += OnStartAction;
		TimeControl.OnStop += OnStopAction;
		TimeControl.OnEndCycle += OnEndCycle;
	}

	public bool Stop() {
		TimeControl.OnStartCycle -= OnStartCycle;
		TimeControl.OnTelegraph -= OnTelegraph;
		TimeControl.OnStart -= OnStartAction;
		TimeControl.OnStop -= OnStopAction;
		TimeControl.OnEndCycle -= OnEndCycle;
		if(programStack == null || programStack.Count == 0) {
			return true;
		} else {
			return false;
		}
	}

	void OnStartCycle() {
		//TODO: highlight executing nodes
		//run until we have a statement that actually consumes a turn
		if(!AdvancePC()) {
			Assert.IsTrue(Stop ());
			return;
		}
		while(CurrentStatement() != null && !CurrentStatement ().TakesTurn) {
			if(!AdvancePC()) {
				Assert.IsTrue(Stop ());
				return;
			}
		}
		Debug.Log ("After Advancement");
		Debug.Log (CurrentStatement());
	}

	void OnTelegraph() {
		if(CurrentStatement() == null) return;
		CurrentStatement().OnTelegraph();
	}

	void OnStartAction() {
		if(CurrentStatement() == null) return;
		Debug.Log ("Sending start");
		CurrentStatement().OnStartAction();
	}

	void OnStopAction() {
		if(CurrentStatement() == null) return;
		CurrentStatement().OnStopAction();
	}

	void OnEndCycle() {
		//TODO: unhighlight
	}

	bool AdvancePC() { //advance the "program counter" one tick
		//if this object doesn't have any sub-statements to be run at this time
		if(CurrentStatement().getChildren() == null ||
		   CurrentStatement().getChildren().Count == 0) {
			//return up the stack until we get an object with a sequential statement after it,
			//or we get an object where we should reenter the children
			while(programStack.Peek ().Count == 1) {
				programStack.Pop ();
				if(programStack.Count == 0) return false;
				if(CurrentStatement().ShouldReEnterChildren) {
					return true;
				}
			}
			//use that sequential statement
			programStack.Peek().Dequeue();
		} else { //object has children, so we push a new frame for them
			programStack.Push(new Queue<Statement>(CurrentStatement().getChildren()));
		}
		return true;
	}
}

using UnityEngine;
using System.Collections;

public class Statement : MonoBehaviour {
	protected System.Collections.Stack stack;
	int nextStmt = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public virtual void OnTick() {
		if (CallNextChild() && ShouldPop()) {
			TimeControl.OnStart -= OnTick;
			((Statement)stack.Peek ()).Dequeue ();
		}
	}

	public virtual void Queue() {
		stack.Push (this);
		TimeControl.OnStart -= OnTick;
	}

	public virtual void Dequeue() {
		stack.Pop ();
		TimeControl.OnStart += OnTick; 
	}

	public virtual bool CallNextChild() {
		//call the next child, and remove this from the callback
		Statement[] children = gameObject.GetComponentsInChildren<Statement>(); 
		if (nextStmt >= children.Length) {
			nextStmt = 1;
			return true;
		}
		Queue ();
		TimeControl.OnStart += children[nextStmt].OnTick;
		children [nextStmt].stack = stack;
		nextStmt ++;
		return false;
	}
	public virtual bool ShouldPop() {
		//whether this should be popped when all its children are done
		//loops should override this
		return true;
	}
}

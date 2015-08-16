using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Statement : ProgBlock {
  protected System.Collections.Stack stack;
  int nextStmt = 1;
	Color defaultColor;

	protected static Statement highlighted = null;
  protected void Highlight() {
		GetComponent<Image>().color = defaultColor * 0.5f;
	}
  protected void Unhighlight() {
		GetComponent<Image>().color = defaultColor;
	}

  protected override void Start () {
	base.Start();
	defaultColor = GetComponent<Image>().color;
  }
	
  protected override void Update () {
  	base.Update();
  }
  
  public virtual void OnTick() {
    Debug.Log ("Tick: " + GetType ().ToString ());
	if(highlighted != null) {
			highlighted.Unhighlight ();
		}
		highlighted = this;
		Highlight();
	
    if (CallNextChild() && ShouldPop()) {
      TimeControl.OnStart -= OnTick;
      TimeControl.OnTelegraph -= OnTelegraph;
      if(stack.Count == 0) {
        return; //we're done
      }
      Statement topStackStatement = ((Statement)stack.Peek ());
      topStackStatement.Dequeue ();
    }
  }

  public virtual void OnTelegraph() {

  }

  public virtual void Queue() {
    stack.Push (this);
    TimeControl.OnStart -= OnTick;
	TimeControl.OnTelegraph -= OnTelegraph;
  }

  public virtual void Dequeue() {
    stack.Pop ();
    TimeControl.OnStart += OnTick; 
    TimeControl.OnTelegraph += OnTelegraph;
  }  

  public virtual bool CallNextChild() {
    while (true) {
      //call the next child, and remove this from the callback
      Statement[] children = gameObject.GetComponentsInChildren<Statement>(); 
      if (nextStmt >= children.Length) {
        nextStmt = 1;
        return true;
      }
      if (children [nextStmt] is Placeholder) {
        nextStmt++;
        continue;
      }
      Queue ();
      TimeControl.OnStart += children[nextStmt].OnTick;
      TimeControl.OnTelegraph += children[nextStmt].OnTelegraph;
      children [nextStmt].stack = stack;
      nextStmt ++;
      return false;
    }
  }
  public virtual bool ShouldPop() {
    //whether this should be popped when all its children are done
    //loops should override this
    return true;
  }

	void OnDestroy() {
		TimeControl.OnStart -= OnTick;
		TimeControl.OnTelegraph -= OnTelegraph;
	}
	
	//RETURNS: Total height of object with all children.
	public override float Layout() {
		//Position immediately beneath
		Vector3 nextPosition = new Vector3 (1f, -1f, 0);
		foreach (Transform child in transform) {
			ProgBlock block = child.GetComponent<ProgBlock>();
			if (!block) {
				continue;
			}
			float height = block.Layout();
			Vector3 blockPosition = block.transform.localPosition;
			block.transform.localPosition = nextPosition;
			nextPosition.y -= height;
		}
		
		return -nextPosition.y;
	}
}

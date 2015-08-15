﻿using UnityEngine;
using System.Collections;

public class Statement : ProgBlock {
  protected System.Collections.Stack stack;
  int nextStmt = 1;

  // Use this for initialization
  void Start () {

  }
  
  // Update is called once per frame
  void Update () {
    
  }
  
  public virtual void OnTick() {
    Debug.Log ("Tick: " + GetType ().ToString ());
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

}

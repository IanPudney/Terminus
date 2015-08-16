using UnityEngine;
using System.Collections;

public class TriggerStatement : StmtBlock {
	
  public override void Start () {
	base.Start();
    stack = new System.Collections.Stack ();
	label.text = "On <color=\"cyan\">Play</color>";
  }
  
  void Update () {
  	base.Update();
  }

  public override void OnTick() {
    base.OnTick ();
  }
}

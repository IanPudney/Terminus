using UnityEngine;
using System.Collections;

public class BaseStatement : StmtBlock {

  // Use this for initialization
  public override void Start () {
    TimeControl.OnStart += OnTick;
    stack = new System.Collections.Stack ();
    base.Start ();
    info.text = "execute program:";
  }
  
  void Update () {
  	base.Update();
  }

  public override void OnTick() {
    base.OnTick ();
  }
}

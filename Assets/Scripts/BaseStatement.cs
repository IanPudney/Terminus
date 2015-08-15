using UnityEngine;
using System.Collections;

public class BaseStatement : StmtBlock {

  // Use this for initialization
  public override void Start () {
    TimeControl.OnStart += OnTick;
    stack = new System.Collections.Stack ();
    base.Start ();
  }
  
  // Update is called once per frame
  void Update () {
  
  }

  public override void OnTick() {
    base.OnTick ();
  }
}

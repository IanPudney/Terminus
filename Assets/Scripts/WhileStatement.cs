using UnityEngine;
using System.Collections;

public class WhileStatement : StmtBlock {

  public override void Start () {
    base.Start ();
    label.text = "While True:";
  }
  
  protected override void Update () {
  	base.Update();
  }

  public override void OnTick() {
    base.OnTick ();
  }

  public override bool ShouldPop() {
    return false;
  }


}

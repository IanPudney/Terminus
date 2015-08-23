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

  public virtual bool ShouldPop() {
    return false;
  }

	public override void Reset() {
		base.Reset();
		
	}
}

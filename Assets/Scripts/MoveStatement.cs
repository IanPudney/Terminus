using UnityEngine;
using System.Collections;

public class MoveStatement : Statement {
  public int robotID = 0;
  
  protected override void Start() {
    base.Start ();
    info.text = "move ROBO" + (robotID + 1) + " fwd";
  }

  public override void OnTick() {
    Debug.Log ("Moving robot " + robotID);
    ObjectDict.obj.robots[robotID].StartAction ();
    base.OnTick ();
  }

  public override void OnTelegraph() {
	ObjectDict.obj.robots[robotID].MoveForwardAction ();
    base.OnTelegraph ();
  }
}

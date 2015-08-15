using UnityEngine;
using System.Collections;

//THIS SCRIPT HAS BEEN DEPRECATED.

//NOTHING TO SEE HERE.

public class Wall : MonoBehaviour {
  public const float PRIORITY = 5f;
  
  void OnEnable() {
    TimeControl.OnTelegraph += SendReport;
  }
    
  void OnDisable() {
    TimeControl.OnTelegraph -= SendReport;
  }
  
  public void SendReport() {
    SpaceControl.obj.AttemptAction(PRIORITY * Vector3.forward,
                                   Mathf.FloorToInt(transform.position.x),
                                   Mathf.FloorToInt(transform.position.y));
  }
}

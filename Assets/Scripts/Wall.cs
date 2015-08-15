using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
  const float PRIORITY = 5f;
  
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

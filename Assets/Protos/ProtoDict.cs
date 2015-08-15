using UnityEngine;
using System.Collections;

public class ProtoDict : MonoBehaviour {
  public static ProtoDict obj;
  //World statics
  public GameObject grid, backdrop;
  //Child objects
  public GameObject doorBrace, infoText;
  //World objects
  public GameObject wall;
  
  public GameObject placeholder;
  
  void Awake () {
    obj = this;
  }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ProtoDict : MonoBehaviour {
  public static ProtoDict obj;
  //World statics
  public GameObject grid, backdrop;
  //Child objects
  public GameObject doorBrace, indentationHint;
	public Text label;
  //World objects
  public GameObject wall;
  
  public GameObject placeholder;
  
  void Awake () {
    obj = this;
  }
}

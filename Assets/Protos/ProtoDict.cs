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
	public StmtBlock block;
  //World objects
  public GameObject wall;
  
  public GameObject placeholder;
  //Block objects
  public GameObject robotMove, robotLeft, robotRight,
  doorOpen, doorClose, doorToggle,
  whileBlock, ifElse, entryPoint, triggerPoint;
  
  void Awake () {
    obj = this;
  }
}

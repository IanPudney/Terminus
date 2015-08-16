using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ObjectDict.obj.AddToPaintsDict(this);
	}
}

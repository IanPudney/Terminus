using UnityEngine;
using System.Collections;

public class MoveStatement : Statement {
	public GameObject robot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnTick() {
		Debug.Log ("Moving robot");
		robot.GetComponent<MainBot> ().StartAction ();
		base.OnTick ();
	}
}

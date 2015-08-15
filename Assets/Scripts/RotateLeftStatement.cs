using UnityEngine;
using System.Collections;

public class RotateLeftStatement : Statement {
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
	
	public override void OnTelegraph() {
		robot.GetComponent<MainBot> ().RotateLeftAction ();
		base.OnTelegraph ();
	}
}

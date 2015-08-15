using UnityEngine;
using System.Collections;

public class DoorCloseStatement : Statement {
	public GameObject doorway;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void OnTick() {
		doorway.GetComponent<Doorway> ().StartAction ();
		base.OnTick ();
	}
	
	public override void OnTelegraph() {
		doorway.GetComponent<Doorway> ().DoorClose ();
		base.OnTelegraph ();
	}
}

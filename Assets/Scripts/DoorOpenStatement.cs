using UnityEngine;
using System.Collections;

public class DoorOpenStatement : Statement {
	public GameObject doorway;
	// Use this for initialization
	void Start () {
		base.Start ();
		label.text = "Open Door <color=\"cyan\">X</color>";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void OnTick() {
		doorway.GetComponent<Doorway> ().StartAction ();
		base.OnTick ();
	}
	
	public override void OnTelegraph() {
		doorway.GetComponent<Doorway> ().DoorOpen ();
		base.OnTelegraph ();
	}
}

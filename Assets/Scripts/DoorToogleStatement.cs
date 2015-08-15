using UnityEngine;
using System.Collections;

public class DoorToogleStatement : Statement {
	public GameObject doorway;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void OnTick() {
		Debug.Log ("Tick Door Toogle");
		doorway.GetComponent<Doorway> ().StartAction ();
		base.OnTick ();
	}
	
	public override void OnTelegraph() {
		Debug.Log ("Telegraph Door Toogle");
		doorway.GetComponent<Doorway> ().DoorToogle ();
		base.OnTelegraph ();
	}
}

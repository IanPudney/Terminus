using UnityEngine;
using System.Collections;

public class ControlDoor : Statement {
	public GameObject doorway;
	// Use this for initialization
	void Start () {
		base.Start ();
		label.text = "Close Door <color=\"cyan\">"+ doorway.GetComponent<Doorway>().doorNumber + "</color>";
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void OnTick() {
		doorway.GetComponent<Doorway> ().StartAction ();
	}
}

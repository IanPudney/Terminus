using UnityEngine;
using System.Collections;

public class DoorToogleStatement : ControlDoor {
	public void OnTelegraph() {
		Debug.Log ("Telegraph Door Toogle");
		doorway.GetComponent<Doorway> ().DoorToogle ();
	}
}

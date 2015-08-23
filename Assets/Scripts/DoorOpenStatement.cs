using UnityEngine;
using System.Collections;

public class DoorOpenStatement : ControlDoor {
	public void OnTelegraph() {
		doorway.GetComponent<Doorway> ().DoorOpen ();
	}
}

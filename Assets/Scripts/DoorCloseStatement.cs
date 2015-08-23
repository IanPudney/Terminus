using UnityEngine;
using System.Collections;

public class DoorCloseStatement : ControlDoor {
	public void OnTelegraph() {
		doorway.GetComponent<Doorway> ().DoorClose ();
	}
}

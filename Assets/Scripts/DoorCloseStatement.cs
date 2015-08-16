using UnityEngine;
using System.Collections;

public class DoorCloseStatement : ControlDoor {
	public override void OnTelegraph() {
		doorway.GetComponent<Doorway> ().DoorClose ();
		base.OnTelegraph ();
	}
}

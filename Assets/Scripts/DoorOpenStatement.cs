using UnityEngine;
using System.Collections;

public class DoorOpenStatement : ControlDoor {
	public override void OnTelegraph() {
		doorway.GetComponent<Doorway> ().DoorOpen ();
		base.OnTelegraph ();
	}
}

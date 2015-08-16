using UnityEngine;
using System.Collections;

public class AestheticRotation : MonoBehaviour {
	void FixedUpdate() {
		transform.Rotate(Vector3.forward * 4f * TimeControl.obj.aesthetic_speed);
	}
}

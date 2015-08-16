using UnityEngine;
using System.Collections;

public class AestheticRotation : MonoBehaviour {
	public float baseRate = 4f;
	void FixedUpdate() {
		transform.Rotate(Vector3.forward * baseRate * TimeControl.obj.aesthetic_speed);
	}
}

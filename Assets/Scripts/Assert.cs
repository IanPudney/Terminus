using UnityEngine;
using System.Collections;

public class Assert {
	public static void IsTrue(bool input) {
		if(!input) throw new UnityException("Assertion failed");
	}
}

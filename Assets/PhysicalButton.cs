using UnityEngine;
using System.Collections;

public class PhysicalButton : MonoBehaviour {
	public int workflow = 1;
	
	void OnEnable() {
		TimeControl.OnStop += CheckForPlayer;
	}
	
	void OnDisable() {
		TimeControl.OnStop -= CheckForPlayer;
	}
	
	public void CheckForPlayer() {
		foreach (MainBot bot in ObjectDict.obj.robots) {
			if (Vector3.Distance(bot.transform.position, transform.position) < 0.1f) {
				//Activate.
			}
		}
	}
}


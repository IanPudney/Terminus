using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {
	void OnEnable() {
		TimeControl.OnStop += StopAction;
	}
	
	void OnDisable() {
		TimeControl.OnStop -= StopAction;
	}
	
	public void StopAction() {
		foreach (MainBot bot in ObjectDict.obj.robots) {
			if (bot.transform.position == transform.position) {
				print ("Win!");
			}
		}
	}
}

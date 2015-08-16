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
			if (Vector3.Distance(bot.transform.position, transform.position) < 0.1f) {
				Application.LoadLevel(LevelMap());
			}
		}
	}
	
	string LevelMap() {
		switch (Application.loadedLevelName) {
			case "Level2": return "Level3";
			case "Level3": return "Level4";
			case "Level4": return "Level5";
			case "Level5": return "Level2";
		}
		return "";
	}
}

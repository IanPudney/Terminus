using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhysicalButton : MonoBehaviour {
	public TriggerStatement statement;
	public int buttonNumber;
	bool isTriggered = false;
	void Start() {
		GetComponentInChildren<TextMesh>().text = buttonNumber.ToString ();
		if (statement) {
			statement.setButtonNumber(buttonNumber);
		}
	}
	void OnEnable() {
		TimeControl.OnStop += CheckForPlayer;
	}
	
	void OnDisable() {
		TimeControl.OnStop -= CheckForPlayer;
	}
	
	public void CheckForPlayer() {
		bool found = false;
		foreach (MainBot bot in ObjectDict.obj.robots) {
			if (Vector2.Distance(bot.transform.position, transform.position) < 0.1f) {
				found = true;
				if(isTriggered) {
					return;
				} else {
					isTriggered = true;
					new StatementVisitor(statement);
				}
			}
		}
		if(!found) {
			isTriggered = false;
		}
	}
	
	public void Reset() {
			//TODO
	}
}


using UnityEngine;
using System.Collections;

public class TimeControl : MonoBehaviour {
	public delegate void StartAction();
	public delegate void StopAction();
	public static event StartAction OnStart;
	public static event StopAction OnStop;
	
	static public TimeControl obj;
	
	public int tick = 0;
	
	//Handles time controls.
	float time_since_update = 0;
	float transition_time = 0.5f;
	float time_between_steps = 1f;
	
	enum state {
		between,
		transition
	};
	
	state State = state.between;

	void Empty() {}
	
	void Start() {
		obj = this;
		OnStart += Empty;
		OnStop += Empty;
	}
	
	void Update () {
		time_since_update += Time.deltaTime	;
		if (State == state.between) {
			if (time_since_update > time_between_steps) {
				time_since_update -= time_between_steps;
				tick += 1;
				State = state.transition;
				OnStart();
			}
		} else { //state is transitioning
			if (time_since_update > transition_time) {
				time_since_update -= time_since_update;
				State = state.between;
				OnStop();
			}
		}
	}
	
	//Properties are awesome
	public float fraction {
		get { return time_since_update / transition_time; }
	}
}

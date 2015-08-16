using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {
	public delegate void TelegraphAction();
	public delegate void StartAction();
	public delegate void StopAction();
	public delegate void SetUp();
	
	//Universal
	public static event SetUp OnSetup;
	public static event TelegraphAction OnTelegraph;
	public static event StartAction OnStart;
	public static event StopAction OnStop;
	
	static public TimeControl obj;
	static public int tick = 0;
	bool isPlaying = false;
	
	//Handles time controls.
	float time_since_update = 0;
	//public float transition_time = 0.5f;
	//public float time_between_steps = 1f;
	
	float slider_rate;
	public float time_between_steps {
		get { return slider_rate / (1f - Mathf.Pow(slider_rate, 6f)); }
	}
	public float transition_time {
		get { return 1f / (10f * (1f - slider_rate)); }
	}
	
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
		if (!isPlaying) {
		return;
		}
		slider_rate = 1f - FindObjectOfType<Slider>().value;
		time_since_update += Time.deltaTime	;
		if (State == state.between) {
			if (time_since_update > time_between_steps) {
				time_since_update -= time_between_steps;
				tick += 1;
				State = state.transition;
			if(OnSetup != null) OnSetup();
			if(OnTelegraph != null) OnTelegraph();
			if(OnStart != null) OnStart();
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
	
	public void Play() {
		isPlaying = true;
		foreach (ProgBlock block in FindObjectsOfType<ProgBlock>()) {
			block.boundToParent = true;
		}
	}
	public void Pause() {
		isPlaying = false;
	}
	public void Reset() {
		Application.LoadLevel (Application.loadedLevelName);
	}
}

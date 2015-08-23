using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeControl : MonoBehaviour {
	public delegate void StartCycle();
	public delegate void TelegraphAction();
	public delegate void StartAction();
	public delegate void StopAction();
	public delegate void EndCycle();
	public delegate void SetUp();
	
	//Universal
	public static event StartCycle OnStartCycle;
	public static event SetUp OnSetup;
	public static event TelegraphAction OnTelegraph;
	public static event StartAction OnStart;
	public static event StopAction OnStop;
	public static event EndCycle OnEndCycle;
	
	static public TimeControl obj;
	static public int tick = 0;
	public float throttleMultiplier = 1.0f;
	bool isPlaying = false;
	
	GameObject StartButton = null;
	
	//Handles time controls.
	float time_since_update = 0;
	//public float transition_time = 0.5f;
	//public float time_between_steps = 1f;
	
	Slider timeSlider;
	public float time_between_steps = 0.5f;
	public float transition_time = 1f;
	
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
		timeSlider = FindObjectOfType<Slider>();
	}
	
	void Update () {
		if (!isPlaying) {
			return;
		}
		Time.timeScale = timeSlider.value;
		time_since_update += Time.deltaTime	;
		if (State == state.between) {
			if (time_since_update > time_between_steps) {
				time_since_update -= time_between_steps;
				tick += 1;
				State = state.transition;
				if(OnStartCycle != null) OnStartCycle();
				if(OnSetup != null) OnSetup();
				if(OnTelegraph != null) OnTelegraph();
				if(OnStart != null) OnStart();
				if(OnEndCycle != null) OnEndCycle();
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
	public float transition_fraction {
		get { return time_since_update / transition_time; }
	}
	public float between_fraction {
		get { return time_since_update / time_between_steps; }
	}
	
	public void Play() {
		isPlaying = true;
		foreach (ProgBlock block in FindObjectsOfType<ProgBlock>()) {
			block.boundToParent = true;
		}
		if (StartButton == null) {
			StartButton = GameObject.Find ("StartButton");
		}
		StartButton.GetComponent<Button>().enabled = false;
		StartButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		StartButton.GetComponentInChildren<Text>().color = Color.clear;
		
	}
	public void CallReset() {
		foreach (ProgBlock block in FindObjectsOfType<ProgBlock>()) {
			block.BroadcastMessage("Reset");
		}
		foreach (MainBot bot in FindObjectsOfType<MainBot>()) {
			bot.BroadcastMessage("Reset");
		}
		foreach (Doorway door in FindObjectsOfType<Doorway>()) {
			door.BroadcastMessage("Reset");
		}
		gameObject.BroadcastMessage("Reset");
	}
	public void Reset() {
		print ("reset");
		isPlaying = false;
		StartButton.GetComponent<Button>().enabled = true;
		StartButton.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
		StartButton.GetComponentInChildren<Text>().color = Color.black;
	}
	public void Clear() {
		Application.LoadLevel (Application.loadedLevelName);
	}
	
	public void Menu() {
		Application.LoadLevel ("Title");
	}
}

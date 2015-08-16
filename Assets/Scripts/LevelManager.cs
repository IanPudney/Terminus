using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadMainMenu() {
		Application.LoadLevel ("Title");
	}

	public void LoadLevel(string level) {
		Application.LoadLevel (level);
	}
}

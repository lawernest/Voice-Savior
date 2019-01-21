using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static bool isPause = false; //change to true when start menu is added
	public WaveManager wave_manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			PauseGame ();
		} else if (Input.GetMouseButtonDown (2)) {
			ResumeGame();
		}
			
	}

	void PauseGame() {
		isPause = true;
		Time.timeScale = 0;
	}

	void ResumeGame() {
		isPause = false;
		Time.timeScale = 1;
	}
}

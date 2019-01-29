using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance { get; private set; }

	public int enemies_on_field = 0;
	public bool isPause = false;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad (this.gameObject);

		InitGame();
	}
		
	void InitGame() {
		
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetMouseButtonDown (1)) {
			PauseGame ();
		} else if (Input.GetMouseButtonDown (2)) {
			ResumeGame();
		}*/
			
	}

	public void PauseGame() {
		isPause = true;
		Time.timeScale = 0;
	}

	public void ResumeGame() {
		isPause = false;
		Time.timeScale = 1;
	}
}

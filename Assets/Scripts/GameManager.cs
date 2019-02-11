using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance { get; private set; }

	[HideInInspector]public int enemies_on_field;
	[HideInInspector]public bool isPause { get; private set; }
	[HideInInspector]public GameObject player_base { get; private set; }

	[Header("Game Mode")]
	public int mode = 0; //0 = Normal, 1 = Timed

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
		InitGame();
	}

	void InitGame() {
		player_base = GameObject.Find("Player Base");
		enemies_on_field = 0;
		isPause = false;
	}

	void Update() {
		
	}

	public void PauseGame() {
		this.isPause = true;
		Time.timeScale = 0;
	}

	public void ResumeGame() {
		this.isPause = false;
		Time.timeScale = 1;
	}
}

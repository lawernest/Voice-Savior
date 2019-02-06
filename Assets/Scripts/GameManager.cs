using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance { get; private set; }

	public Text waveText = null;
	public Text testText;

	[Header("Game Mode")]
	public int mode = 0; //0 = Normal, 1 = Timed
	[HideInInspector]public int enemies_on_field = 0;
	[HideInInspector]public bool isPause = false;

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
		if (Input.GetMouseButtonDown (1)) {
			PauseGame ();
		} else if (Input.GetMouseButtonDown (2)) {
			ResumeGame();
		}

		testText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(GameObject.Find ("1").transform.position.x, GameObject.Find ("1").transform.position.y + 3.0f, GameObject.Find ("1").transform.position.z));

	}

	public void PauseGame() {
		this.isPause = true;
		Time.timeScale = 0;
	}

	public void ResumeGame() {
		this.isPause = false;
		Time.timeScale = 1;
	}

	public void UpdateWaveText(int waveNum, int finalWave) {
		this.waveText.text = "Wave " + waveNum + "/" + finalWave;
	}
}

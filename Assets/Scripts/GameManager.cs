using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public enum Mode
	{
		Normal,
		Timed
	};

	public static GameManager instance { get; private set; }

	[Header("Game Mode")]
	[SerializeField] private Mode mode;

	[HideInInspector] public int enemies_on_field;
	[HideInInspector] public bool isPause { get; private set; }
	[HideInInspector] public bool inGame { get; private set; }
	[HideInInspector] public GameObject playerBase;
	private GameObject waveManager;
	private float countDown;

	public Mode GameMode {
		get {
			return mode;
		}
	}
		
	private void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		InitGame();
	}

	private void InitGame() {
		enemies_on_field = 0;
		isPause = false;
		inGame = false;
		countDown = 1.2f;

		// Testing
		inGame = true;
		playerBase = GameObject.Find ("Player Base");
		waveManager = GameObject.Find ("Wave Manager");
	}

	private void Update() {
		if (inGame) {
			if (playerBase == null) {
				this.countDown -= Time.deltaTime;
				if(this.countDown <= 0.0f) {
					Debug.Log ("Lose");
					//PauseGame();
					//UIManager.instance.DisplayGameEndScene();
				}
			}
			if (!waveManager.activeSelf && enemies_on_field == 0) {
				Debug.Log ("Win");
			}
		}
	}

	IEnumerator LoadGameScene(string level) {
		string path = "Scene/Levels/" + level + "/" + level;
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (path, LoadSceneMode.Single);

		// wait until the scene is loaded
		while(!asyncLoad.isDone) {
			yield return null;
		}

		inGame = true;
		playerBase = GameObject.Find("Player Base");
		waveManager = GameObject.Find("Wave Manager");
	}

	public void PauseGame() {
		this.isPause = true;
		Time.timeScale = 0;
	}

	public void ResumeGame() {
		this.isPause = false;
		Time.timeScale = 1;
	}

	public void StartGame() {
		StartCoroutine(LoadGameScene("Level 0"));
	}

	public void DisplayGameEndScene() {
		SceneManager.LoadSceneAsync("Scene/Menu/Game End", LoadSceneMode.Additive);
	}

	public void DisplayTitleScene() {
		SceneManager.LoadSceneAsync("Scene/Menu/Start Scene", LoadSceneMode.Single);
	}

	public void ResumeGameScene() {
		SceneManager.UnloadSceneAsync("Scene/Menu/Game End");
	}

	public void QuitGame() {
		Application.Quit();
	}
}

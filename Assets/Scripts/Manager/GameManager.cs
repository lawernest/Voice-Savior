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

	public enum GameState 
	{
		StartMenu,
		EndMenu,
		Game,
		Pause
	};

	public static GameManager instance { get; private set; }

	[Header("Game Mode")]
	[SerializeField] private Mode mode;

	[HideInInspector] public int enemies_on_field;
	[HideInInspector] public GameObject playerBase;
	private GameObject waveManager;
	private float countDown;
	private GameState state;

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
		this.state = GameState.StartMenu;
		DontDestroyOnLoad(this.gameObject);
	}

	public bool isPause() {
		return this.state == GameState.Pause || this.state == GameState.EndMenu;
	}

	public bool isOnStartMenu() {
		return this.state == GameState.StartMenu;
	}

	public bool isOnEndMenu() {
		return this.state == GameState.EndMenu;
	}

	public bool inGame() {
		return this.state == GameState.Game;
	}

	private void InitGame() {
		this.enemies_on_field = 0;
		this.countDown = 1.2f;
		this.state = GameState.Game;
		this.playerBase = GameObject.Find("Player Base");
		this.waveManager = GameObject.Find("Wave Manager");
		Controller.weaponInfo = GameObject.Find("WeaponInfo");
		Controller.weaponInfo.SetActive(false);
		VoiceController.instance.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		VoiceController.instance.StopRecognitionSystem();
	}

	private void Update() {
		if (this.state == GameState.Game) {
			if (playerBase == null) {
				this.countDown -= Time.deltaTime;
				this.waveManager.SetActive(false);
				if(this.countDown <= 0.0f) {
					PauseGame();
					DisplayGameOverScene();
				}
			}
			if (!this.waveManager.activeSelf && this.enemies_on_field == 0) {
				this.countDown -= Time.deltaTime;
				if(this.countDown <= 0.0f) {
					DisplayWinningScene();
				}
			}
		}
	}

	IEnumerator LoadGameScene(string level) {
		string path = "Scene/Levels/" + level + "/" + level;
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(path, LoadSceneMode.Single);

		// wait until the scene is loaded
		while(!asyncLoad.isDone) {
			yield return null;
		}

		InitGame();
	}

	public void PauseGame() {
		this.state = GameState.Pause;
		Time.timeScale = 0;
	}

	public void ResumeGame() {
		this.state = GameState.Game;
		Time.timeScale = 1;
	}

	public void StartGameInNormalMode() {
		this.mode = Mode.Normal;
		StartCoroutine(LoadGameScene("Level 0"));
	}

	public void StartGameInTimedMode() {
		this.mode = Mode.Timed;
		StartCoroutine(LoadGameScene("Level 0"));
	}

	public void DisplayWinningScene() {
		this.state = GameState.EndMenu;
		SceneManager.LoadSceneAsync("Scene/Menu/Winning Scene", LoadSceneMode.Additive);
	}

	public void DisplayGameOverScene() {
		this.state = GameState.EndMenu;
		SceneManager.LoadSceneAsync("Scene/Menu/GameOver Scene", LoadSceneMode.Additive);
	}

	public void DisplayTitleScene() {
		ResumeGame();
		this.state = GameState.StartMenu;
		SceneManager.LoadSceneAsync("Scene/Menu/Start Scene", LoadSceneMode.Single);
		SceneManager.UnloadSceneAsync("Scene/Levels/Level 0/Level 0");
	}

	public void QuitGame() {
		Application.Quit();
	}
}

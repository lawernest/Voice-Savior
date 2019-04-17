using System.Collections;
using System.Collections.Generic;
using System.Linq; // for toArray
using UnityEngine;
using UnityEngine.Windows.Speech; // For speech recognition

public class VoiceController : Controller {
	
	public enum Command { 
		None, 
		Select,
		Buy
	};

	public static VoiceController instance { get; private set; }
	private KeywordRecognizer recognizer;
	private Command cur_command = Command.None;
	public CameraMovement mainCamera;
	private List<string> commands_list;

	public Command command {
		get { return cur_command; }
		set { cur_command = value; }
	}

	private void Start() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
			
		PhraseRecognitionSystem.OnError += EncounterError;
		commands_list = new List<string>();
		AddBasicCommands();

		StartRecognitionSystem();
	}

	private void Update() {
		if(recognizer != null && !recognizer.IsRunning && GameManager.instance.inGame()) {
			AddCommandsForCurrentLevel();
			StartRecognitionSystem();
		}
	}

	private bool CheckKeywordInList(string keyword) {
		return recognizer.Keywords.Contains(keyword);
	}

	private void StartRecognitionSystem() {
		StopRecognitionSystem();
		recognizer = new KeywordRecognizer(commands_list.ToArray(), ConfidenceLevel.Low);
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;

		#if UNITY_STANDALONE_WIN
			recognizer.Start();
		#endif
	}

	public void StopRecognitionSystem() {
		if (recognizer != null && recognizer.IsRunning) {
			recognizer.OnPhraseRecognized -= OnKeywordsRecognized;
			recognizer.Dispose(); // remove the recognizer
		}
	}

	private void AddBasicCommands() {
		commands_list.Add("Play Normal");
		commands_list.Add("Play Timed");
		commands_list.Add("Quit");
		commands_list.Add("Continue");
		commands_list.Add("Pause");
		commands_list.Add("Resume");
		commands_list.Add("Upgrade");
		commands_list.Add("Sell");
		commands_list.Add("Cancel");
		commands_list.Add("Move Up");
		commands_list.Add("Move Down");
		commands_list.Add("Move Left");
		commands_list.Add("Move Right");
		commands_list.Add("Stop");
	}

	private void AddCommandsForCurrentLevel() {
		GameObject[] towers = ModelManager.instance.Towers;
		GameObject[] weapons = ModelManager.instance.WeaponPrefabs;

		for (int i = 0; i < towers.Length; i++) {
			string tower_name = towers[i].name;
			if (!CheckKeywordInList ("Select " + tower_name)) {
				commands_list.Add("Select " + tower_name);
			} 
			if (!CheckKeywordInList ("Place on " + tower_name)) {
				commands_list.Add("Place on " + tower_name);
			}
			if (!CheckKeywordInList("Look " + tower_name)) {
				commands_list.Add("Look " + tower_name);
			}
		}

		for (int i = 0; i < weapons.Length; i++) {
			if (!CheckKeywordInList ("Buy " + weapons[i].name)) {
				commands_list.Add("Buy " + weapons[i].name);
			}
		}
	}

	private void ProcessStartMenuCommand(string command) {
		if (command == "Play Normal") {
			GameManager.instance.StartGameInNormalMode();
		}
		else if (command == "Play Timed") {
			GameManager.instance.StartGameInTimedMode();
		}
		else if(command == "Quit") {
			GameManager.instance.QuitGame ();
		}
	}

	private void ProcessEndGameMenuCommand(string command) {
		if (command == "Continue") {
			GameManager.instance.DisplayTitleScene();
		}
	}

	private void ProcessPauseMenuCommand(string command) {
		if (command == "Resume") {
			GameLog.instance.UpdateLog(command);
			GameManager.instance.ResumeGame();
		}
	}


	private void OnKeywordsRecognized(PhraseRecognizedEventArgs args) {
		string[] words = args.text.Split(' ');

		if (GameManager.instance.isOnStartMenu()) { // On Start menu
			if (args.text == "Play Normal") {
				GameManager.instance.StartGameInNormalMode();
			}
			else if (args.text == "Play Timed") {
				GameManager.instance.StartGameInTimedMode();
			}
			else if(args.text == "Quit") {
				GameManager.instance.QuitGame ();
			}
		} 
		else if (GameManager.instance.isOnEndMenu()) { // On End Game menu
			if (args.text == "Continue") {
				GameManager.instance.DisplayTitleScene();
			}
		}
		else if (GameManager.instance.isPause()) { // On Pause Menu
			if (args.text == "Resume") {
				GameLog.instance.UpdateLog(args.text);
				GameManager.instance.ResumeGame();
			}
		} 
		else if(GameManager.instance.inGame()) { // In Game
			MoveCamera("Stop");
			switch(words[0]) {
			case "Pause":
				GameLog.instance.UpdateLog(args.text);
				GameManager.instance.PauseGame();
				break;
			case "Move":
				GameLog.instance.UpdateLog(args.text);
				MoveCamera(words[1]);
				ResetCommand();
				break;
			case "Look":
				GameLog.instance.UpdateLog(args.text);
				LookAt (words[1]);
				ResetCommand();
				break;
			case "Stop":
				GameLog.instance.UpdateLog(args.text);
				ResetCommand();
				break;
			case "Select":
				GameLog.instance.UpdateLog(args.text);
				Select(words[1]);
				break;
			case "Buy":
				GameLog.instance.UpdateLog(args.text);
				Buy(words[1]);
				break;
			case "Cancel":
				GameLog.instance.UpdateLog(args.text);
				ResetCommand();
				break;
			default:
				break;
			}

			if (cur_command == Command.None) {
				UIManager.ShowWeaponInfo(false);
			}

			switch(cur_command) {
			case Command.Select:
				if (words[0] == "Upgrade") {
					GameLog.instance.UpdateLog(args.text);
					Upgrade();
					UIManager.ShowWeaponInfo(false);
					ResetCommand();
				} else if (words[0] == "Sell") {
					GameLog.instance.UpdateLog(args.text);
					Sell();
					UIManager.ShowWeaponInfo(false);
					ResetCommand();
				}
				break;
			case Command.Buy:
				if (words[0] == "Place") {
					GameLog.instance.UpdateLog(args.text);
					PlaceOn(words[2]);
					ResetCommand();
				}
				break;
			default:
				break;
			}
		}
	}

	private void ResetCommand() {
		cur_command = Command.None;
		Controller.selected = null;
		Controller.weaponIndex = -1;
	}

	private void LookAt(string target) {
		Transform tower = ModelManager.instance.GetTowerByName(target).transform;
		mainCamera.LookAt(tower);
	}

	public void MoveCamera(string direction) {
		switch (direction) {
		case "Up":
			CameraMovement.moveDirection = CameraMovement.Direction.North;
			break;
		case "Down":
			CameraMovement.moveDirection = CameraMovement.Direction.South;
			break;
		case "Right":
			CameraMovement.moveDirection = CameraMovement.Direction.East;
			break;
		case "Left":
			CameraMovement.moveDirection = CameraMovement.Direction.West;
			break;
		case "Stop":
			CameraMovement.moveDirection = CameraMovement.Direction.Center;
			break;
		}
	}
		
	private void Select(string name) {
		Controller.selected = ModelManager.instance.GetTowerByName(name);
		if (Controller.selected != null) {
			cur_command = Command.Select;
			LookAt(name);
			UIManager.ShowWeaponInfo(true);
		}

	}
		
	public override void Buy(string name) {
		Controller.weaponIndex = Shop.instance.ProductOnHold(name);
		if (Controller.weaponIndex > -1) {
			cur_command = Command.Buy;
		}
	}

	private void Upgrade() {
		bool success = Shop.instance.UpgradeWeapon(Controller.selected.transform);
		if (success) {
			LookAt(Controller.selected.name);
		}
	}

	private void Sell() {
		int cost = Shop.instance.Sell(Controller.selected);
		if (cost > 0) {
			GameLog.instance.UpdateLog("Recieved " + cost + " gold");
		}
	}
		
	private void PlaceOn(string name) {
		Transform tower = ModelManager.instance.GetTowerByName(name).transform;
		bool success = Shop.instance.Purchase(Controller.weaponIndex, tower);
		if (!success) {
			GameLog.instance.UpdateLog ("Tower " + name + " already has a turret");
		} else {
			LookAt(name);
		}
		Controller.weaponIndex = -1;
	}

	private void EncounterError(SpeechError code) {
		GameManager.instance.QuitGame();
	}

	// Stop the recognizer before the application is closed
	private void OnApplicationQuit() {
		StopRecognitionSystem();
		Debug.Log ("Closed");
	}
}

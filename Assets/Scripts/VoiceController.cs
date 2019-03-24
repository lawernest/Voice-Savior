using System; // for Action
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

	private List<string> commands_list;
	private KeywordRecognizer recognizer;
	private Command cur_command;
	private CameraMovement mainCamera;

	private void Start() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		// Initialize a list to store the commands
		this.commands_list = new List<string>();
		this.cur_command = Command.None;
		this.mainCamera = GameObject.Find("Main Camera").GetComponent<CameraMovement>();

		AddCommandsToList();

		// Add the keywords to the recogniezr and set the confidence level
		recognizer = new KeywordRecognizer(commands_list.ToArray(), ConfidenceLevel.Low);
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;

		recognizer.Start();
	}

	private void AddCommandsToList() {
		commands_list.Add("Pause");
		commands_list.Add("Resume");
		commands_list.Add("Upgrade");
		commands_list.Add("Sell");
		commands_list.Add("Cancel");
		// To-Do: camera commands 
		commands_list.Add("Move Up");
		commands_list.Add("Move Down");
		commands_list.Add("Move Left");
		commands_list.Add("Move Right");
		commands_list.Add ("Stop");
		// To-Do: Center camera to object

		Transform towers = GameObject.Find("Towers").transform;
		GameObject[] weapons = ModelManager.instance.WeaponPrefabs;
		string tower_name;

		for (int i = 0; i < towers.childCount; i++) {
			tower_name = towers.GetChild(i).name;
			commands_list.Add("Select " + tower_name);
			commands_list.Add("Place on " + tower_name);
			commands_list.Add("Look " + tower_name);
		}

		for (int i = 0; i < weapons.Length; i++) {
			commands_list.Add("Buy " + weapons[i].name);
		}
	}

	private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log ("Command: " + args.text);
		String[] words = args.text.Split(' ');

		switch (words[0]) {
		case "Move":
			MoveCamera(words[1]);
			cur_command = Command.None;
			break;
		case "Look": //To-Do
			LookAt(words[1]);
			break;
		case "Stop":
			MoveCamera("Stop");
			cur_command = Command.None;
			break;
		case "Pause":
			Pause();
			cur_command = Command.None;
			break;
		case "Resume":
			Resume ();
			cur_command = Command.None;
			break;
		case "Select":
			Select (words[1]);
			cur_command = Command.Select;
			break;
		case "Buy":
			Buy(words[1]);
			if (weaponIndex!= -1) {
				cur_command = Command.Buy;
			}
			break;
		case "Cancel":
			cur_command = Command.None;
			break;
		}

		if (weaponIndex != -1) {
			cur_command = Command.Buy;
		}

		switch (cur_command) {
		case Command.Select:
			if (words [0] == "Upgrade") {
				Upgrade ();
				cur_command = Command.None;
			} else if (words [0] == "Sell") {
				Sell ();
				cur_command = Command.None;
			}
			break;
		case Command.Buy:
			if (words[0] == "Place") {
				PlaceOn (words [2]);
				cur_command = Command.None;
			}
			break;
		}
	}

	private void LookAt(string target) {
		Transform tower = GameObject.Find(target).transform;
		mainCamera.LookAt(tower);
	}

	private void MoveCamera(string direction) {
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
		GameLog.instance.UpdateLog("Moving " + direction);
	}
		
	private void Pause() {
		GameManager.instance.PauseGame();
	}

	private void Resume() {
		GameManager.instance.ResumeGame();
	}

	// select by tower name
	private void Select(string name) {
		Controller.selected = GameObject.Find(name);
		GameLog.instance.UpdateLog("Selected Tower " + name);
	}
		
	protected override void Buy(string name) {
		Controller.weaponIndex = Shop.instance.ProductOnHold(name);
		GameLog.instance.UpdateLog("Purchased " + name);
	}

	private void Upgrade() {
		/*if (MouseController.selected != null) {
			selected_weapon = MouseController.selected;
		} else {
			selected_weapon = this.selected;
		}*/

		DefenseTower tower = Controller.selected.GetComponent<DefenseTower>();

		if (Controller.selected != null) {
			Upgrade upgrade = tower.GetTurret().GetComponent<Upgrade>();
			if (upgrade.Upgradable()) {
				upgrade.UpgradeUnit(tower);
			}
		}
	}

	private void Sell() {
		int cost = Shop.instance.Sell(Controller.selected);
		if (cost > 0) {
			GameLog.instance.UpdateLog("recieved " + cost + " gold");
		}
	}
		
	private void PlaceOn(string name) {
		Transform tower = GameObject.Find(name).transform;
		Shop.instance.Purchase (Controller.weaponIndex, tower);
		/*GameObject turret = ModelManager.instance.CreateWeapon(Controller.weaponIndex, tower.GetChild(0)); 
		tower.GetComponent<DefenseTower>().PlaceTurret(turret.transform);
		Player.instance.ReduceGold(turret.GetComponent<Unit>().Cost);*/
		Controller.weaponIndex = -1;
	}

	// Stop the recognizer before the application is closed
	private void OnApplicationQuit() {
		if (recognizer != null && recognizer.IsRunning) {
			recognizer.OnPhraseRecognized -= OnKeywordsRecognized;
			recognizer.Stop();
			Debug.Log ("Closed");
		}
	}
}

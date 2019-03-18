using System; // for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; // for toArray
using UnityEngine;
using UnityEngine.Windows.Speech; // For speech recognition

public class VoiceController : Controller {

	public enum Command { None, Select, Buy };

	public static int weapon_index = -1;

	private List<string> commands_list;
	private KeywordRecognizer recognizer;
	private Command cur_command;

	void Start () {
		// Initialize a list to store the commands
		commands_list = new List<string>();
		cur_command = Command.None;
		// Add all required game commands to the commands_list
		AddCommandsToList();
		// Add the keywords to the recogniezr and set the confidence level
		recognizer = new KeywordRecognizer(commands_list.ToArray(), ConfidenceLevel.Low);
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		// Start the recognizer
		recognizer.Start();
	}

	private void AddCommandsToList() {
		commands_list.Add("Pause");
		commands_list.Add("Resume");
		commands_list.Add("Upgrade");
		commands_list.Add("Sell");
		commands_list.Add("Cancel");
		// To-Do: camera commands 
		commands_list.Add("Move up");
		commands_list.Add("Move down");
		commands_list.Add("Move left");
		commands_list.Add("Move right");

		Transform towers = GameObject.Find("Towers").transform;
		GameObject[] weapons = ModelManager.instance.weaponPrefabs;
		string tower_name;

		for (int i = 0; i < towers.childCount; i++) {
			tower_name = towers.GetChild(i).name;
			commands_list.Add("Select " + tower_name);
			commands_list.Add("Place on " + tower_name);
		}

		for (int i = 0; i < weapons.Length; i++) {
			commands_list.Add("Buy " + weapons[i].name);
		}
	}

	private void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log ("Command: " + args.text);
		String[] words = args.text.Split (' ');

		switch (words[0]) {
		case "Move":
			MoveCamera(words[1]);
			cur_command = Command.None;
			break;
		case "Pause":
			Pause ();
			cur_command = Command.None;
			break;
		case "Resume":
			Resume ();
			cur_command = Command.None;
			break;
		case "Select":
			Select (words [1]);
			cur_command = Command.Select;
			break;
		case "Buy":
			Buy(words [1]);
			if (weapon_index != -1) {
				cur_command = Command.Buy;
			}
			break;
		case "Cancel":
			cur_command = Command.None;
			break;
		}

		if (weapon_index != -1) {
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
			if (words [0] == "Place") {
				PlaceOn (words [2]);
				cur_command = Command.None;
			}
			break;
		}
	}

	private void MoveCamera(string direction) {

	}
		
	private void Pause() {
		GameManager.instance.PauseGame();
	}

	private void Resume() {
		GameManager.instance.ResumeGame();
	}

	private void Select(string name) {
		Controller.selected = GameObject.Find(name);
	}
		
	private void Buy(string name) {
		weapon_index = ModelManager.instance.SearchForWeapon(name);
		Shop.instance.ProductOnHold(weapon_index);
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
		if (Controller.selected.GetComponent<DefenseTower>().GetTurret() != null) {
			Shop.instance.SellTurret(Controller.selected);
		}
	}
		
	private void PlaceOn(string name) {
		Transform tower = GameObject.Find(name).transform;
		GameObject turret = ModelManager.instance.CreateWeapon(weapon_index, tower.GetChild(0)); 
		tower.GetComponent<DefenseTower>().PlaceTurret(turret.transform);
		Player.instance.ReduceGold(turret.GetComponent<Unit>().Cost);
		weapon_index = -1;
	}

	protected override void UpdateLog() {

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

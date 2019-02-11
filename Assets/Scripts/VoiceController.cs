using System; // for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; // for toArray
using UnityEngine;
using UnityEngine.Windows.Speech; // For speech recognition

public class VoiceController : MonoBehaviour {

	public enum Command { None, Select, Buy };

	public static int weapon_index = -1;

	private List<string> commands_list;
	private KeywordRecognizer recognizer;
	private GameObject selected;
	private Command cur_command;

	// Use this for initialization
	void Start () {
		commands_list = new List<string>();
		cur_command = Command.None;

		AddCommandsToList();

		recognizer = new KeywordRecognizer(commands_list.ToArray(), ConfidenceLevel.Low);
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		recognizer.Start();
	}

	void AddCommandsToList() {
		commands_list.Add("Pause");
		commands_list.Add("Resume");
		commands_list.Add("Upgrade");
		commands_list.Add("Sell");
		commands_list.Add("Cancel");

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

	void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log("Command: " + args.text);
		String[] words = args.text.Split(' ');

		switch(words[0]) {
			case "Pause":
				Pause();
				cur_command = Command.None;
				break;
			case "Resume":
				Resume();
				cur_command = Command.None;
				break;
			case "Select":
				Select(words[1]);
				cur_command = Command.Select;
				break;
			case "Buy":
				Buy(words[1]);
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
				if (words[0] == "Upgrade") {
					Upgrade();
					cur_command = Command.None;
				} else if (words[0] == "Sell") {
					Sell();
					cur_command = Command.None;
				}
				break;
			case Command.Buy:
				if (words [0] == "Place") {
					PlaceOn(words[2]);
					cur_command = Command.None;
				}
				break;
		}
	}
		
	void Pause() {
		GameManager.instance.PauseGame();
	}

	void Resume() {
		GameManager.instance.ResumeGame();
	}

	void Select(string name) {
		this.selected = GameObject.Find(name);
	}
		
	void Buy(string name) {
		weapon_index = ModelManager.instance.SearchForWeapon(name);
		Shop.instance.ProductOnHold(weapon_index);
	}

	void Upgrade() {
		/*if (MouseController.selected != null) {
			selected_weapon = MouseController.selected;
		} else {
			selected_weapon = this.selected;
		}*/

		DefenseTower tower = this.selected.GetComponent<DefenseTower>();

		if (this.selected != null) {
			Upgrade upgrade = tower.GetTurret().GetComponent<Upgrade>();
			if (upgrade.Upgradable()) {
				upgrade.UpgradeUnit(tower);
			}
		}
	}

	void Sell() {
		if (selected.GetComponent<DefenseTower>().GetTurret() != null) {
			Shop.instance.SellTurret(selected);
		}
	}
		
	void PlaceOn(string name) {
		Transform tower = GameObject.Find(name).transform;
		GameObject turret = ModelManager.instance.CreateWeapon(weapon_index, tower.GetChild(0)); 
		tower.GetComponent<DefenseTower>().PlaceTurret(turret.transform);
		Player.instance.UpdateGoldAmount(-turret.GetComponent<Unit>().cost);
		weapon_index = -1;
	}

	// Stop the recognizer before the application is closed
	void OnApplicationQuit() {
		if (recognizer != null && recognizer.IsRunning) {
			recognizer.OnPhraseRecognized -= OnKeywordsRecognized;
			recognizer.Stop();
			Debug.Log ("Closed");
		}
	}

}

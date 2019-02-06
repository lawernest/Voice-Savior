using System; // for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; // for toArray
using UnityEngine;
using UnityEngine.Windows.Speech; // For speech recognition

public class VoiceController : MonoBehaviour {

	public enum Command { None, Select, Buy, Place };

	private Dictionary<string, Action> commands;
	private KeywordRecognizer recognizer;
	private String[] words;
	private GameObject selected;
	private Command cur_mode;

	// Use this for initialization
	void Start () {
		commands = new Dictionary<string, Action>();
		cur_mode = Command.None;

		AddKeywords();
	
		recognizer = new KeywordRecognizer(commands.Keys.ToArray(), ConfidenceLevel.Low);
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		recognizer.Start();
	}

	void AddKeywords() {
		commands.Add("Pause", Pause);
		commands.Add("Resume", Resume);
		commands.Add("Upgrade", Upgrade);

		Transform towers = GameObject.Find("Towers").transform;
		GameObject[] weapons = ModelManager.instance.weaponPrefabs;

		for (int i = 0; i < towers.childCount; i++) {
			commands.Add("Select " + towers.GetChild(i).name, Select);
			commands.Add("Place at " + towers.GetChild (i).name, PlaceWeapon);
		}
			
		for (int i = 0; i < weapons.Length; i++) {
			commands.Add("Buy " + weapons [i].name, BuyWeapon);
		}
	}

	// Stop the recognizer before the application is closed
	void OnApplicationQuit() {
		if (recognizer != null && recognizer.IsRunning) {
			recognizer.OnPhraseRecognized -= OnKeywordsRecognized;
			recognizer.Stop();
			Debug.Log ("Closed");
		}
	}

	void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		words = args.text.Split(' ');
		if(words [0] == "Select") {
			selected = GameObject.Find(words[1]);
		}

		if(words[0] == "Buy") {
		}

		if(words [0] == "Put") {

		}
			

		Debug.Log("Command: " + args.text);
		commands[args.text].Invoke();
	}

	void Select() {
		Debug.Log(selected.name);
	}

	void Pause() {
		GameManager.instance.PauseGame();
	}

	void Resume() {
		GameManager.instance.ResumeGame();
	}

	void Upgrade() {
		if (MouseController.selected != null) {
			Upgrade upgrade = MouseController.selected.GetComponent<Upgrade> ();
			if (upgrade.Upgradable()) {
				upgrade.UpgradeUnit ();
			}
		}
	}

	void BuyWeapon() {
		//TO-DO
		Shop.instance.PurchaseCannon();
	}

	void PlaceWeapon() {
		Debug.Log ("Place");
	}
}

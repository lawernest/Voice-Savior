using System; // for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; // For nothing
using UnityEngine;
using UnityEngine.Windows.Speech; // For speech recognition

public class VoiceController : MonoBehaviour {
	
	private Dictionary<string, Action> commands;
	private KeywordRecognizer recognizer;
	private String[] words;
	private GameObject selected;

	// Use this for initialization
	void Start () {
		commands = new Dictionary<string, Action>();

		commands.Add("Pause", Pause);
		commands.Add("Resume", Resume);
		commands.Add("Upgrade", Upgrade);
		//commands.Add("Select", Select);
		commands.Add("Buy", BuyWeapon);

		Transform towers = GameObject.Find("Towers").transform;
		for (int i = 0; i < towers.childCount; i++) {
			commands.Add("Select " + towers.GetChild(i).name, Select);
		}

		GameObject[] weapons = ModelManager.instance.weaponPrefabs;
		for (int i = 0; i < weapons.Length; i++) {
			commands.Add("Buy " + weapons [i].name, BuyWeapon);
		}

		commands.Add("B1", Test);

		recognizer = new KeywordRecognizer(commands.Keys.ToArray(), ConfidenceLevel.Low);
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		recognizer.Start();
	}

	// Stop the recognizer before the application is closed
	void OnApplicationQuit() {
		if (recognizer.IsRunning) {
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

		if (words [0] == "Buy") {
		}
			

		Debug.Log("Command: " + args.text);
		commands[args.text].Invoke();
	}

	void Select() {
		Debug.Log(selected.name);
	}

	void Test() {
		Debug.Log ("Works");
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
			if (upgrade.Upgradable ()) {
				upgrade.UpgradeUnit ();
			}
		}
	}

	void BuyWeapon() {
		Shop.instance.PurchaseCannon();
	}
}

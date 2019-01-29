using System; // for Action
using System.Collections;
using System.Collections.Generic;
using System.Linq; // For nothing
using UnityEngine;
using UnityEngine.Windows.Speech; // For speech recognition

public class VoiceController : MonoBehaviour {
	
	private Dictionary<string, Action> commands;
	private KeywordRecognizer recognizer;

	// Use this for initialization
	void Start () {
		commands = new Dictionary<string, Action>();

		commands.Add("Wait", Pause);
		commands.Add("Start", Resume);
		commands.Add("Upgrade", Upgrade);

		recognizer = new KeywordRecognizer(commands.Keys.ToArray());
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		recognizer.Start();
	}

	/*void Update() {
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		if (Input.GetKeyDown (KeyCode.T)) {
			recognizer.Start();
			//recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		} else if (Input.GetKeyDown (KeyCode.S)) {
			recognizer.Stop();
		}
	}*/

	void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log("Command: " + args.text);
		commands[args.text].Invoke();
	}

	void Select() {

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
}

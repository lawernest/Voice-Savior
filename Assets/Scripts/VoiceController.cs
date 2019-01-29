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

		commands.Add("Pause", Pause);
		commands.Add("Resume", Resume);
		commands.Add("Upgrade", Upgrade);

		recognizer = new KeywordRecognizer(commands.Keys.ToArray());
		recognizer.OnPhraseRecognized += OnKeywordsRecognized;
		recognizer.Start();
	}

	void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
	{
		Debug.Log("Command: " + args.text);
		commands[args.text].Invoke();
	}

	void Select() {

	}

	void Pause() {
		GameManager.isPause = true;
	}

	void Resume() {
		GameManager.isPause = false;
	}

	void Upgrade() {

	}
}

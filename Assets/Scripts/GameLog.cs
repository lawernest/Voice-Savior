using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour {

	public static GameLog instance { get; private set; }

	private Text logText;
	private Queue<string> messages;
	private const int MAX_MESSAGE =	7;

	private void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		this.messages = new Queue<string>();
		this.logText = this.transform.GetChild(0).GetComponent<Text>();
	}

	public void UpdateLog(string action) {
		string message = "You have " + action;

		this.messages.Enqueue(message);

		if (messages.Count > MAX_MESSAGE) {
			messages.Dequeue();
		}

		this.logText.text = string.Join ("\n", messages.ToArray());
	}
}

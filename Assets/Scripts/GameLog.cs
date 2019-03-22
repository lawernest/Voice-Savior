using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLog : MonoBehaviour {

	public Text logText;
	public Queue<string> messages;
	private const int MAX_MESSAGE =	5;

	// Use this for initialization
	private void Start () {
		messages = new Queue<string>();
	}

	public void UpdateLog() {

	}
}

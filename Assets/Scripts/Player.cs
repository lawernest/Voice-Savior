using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public static Player instance { get; private set; }

	public int gold = 0;
	public Text goldText;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
		UpdateGoldText();
	}

	public void UpdateGoldAmount(int amount) {
		this.gold += amount;
		UpdateGoldText();
	}

	void UpdateGoldText() {
		this.goldText.text = this.gold.ToString();
	}
}

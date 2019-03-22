using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public static Player instance { get; private set; }

	[SerializeField] private int gold = 0;
	[SerializeField] private Text goldText;

	public int Gold {
		get {
			return gold;
		}
	}

	private void Start() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
		UpdateGoldText();
	}

	public void ReduceGold(int amount) {
		this.gold -= amount;
		UpdateGoldText();
	}

	public void IncreaseGold(int amount) {
		this.gold += amount;
		UpdateGoldText();
	}

	private void UpdateGoldText() {
		this.goldText.text = this.gold.ToString();
	}
}

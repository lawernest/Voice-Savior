using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public static Shop instance { get; private set; }

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	public void PurchaseCannon() {
		Debug.Log ("Purchased Cannon");
	}

	public void PurchaseMachineGun() {
		Debug.Log ("Purchased Machine Gun");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public static Shop instance { get; private set; }

	public Transform tower;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	public void PurchaseCannon() {
		//To-Do

		ModelManager.instance.CreateWeapon(0, tower.GetChild(0));
		//ModelManager.instance.CreateWeapon(0, MouseController.selected.transform.GetChild(0));
		Debug.Log ("Purchased Cannon");
	}

	public void CannonButton() {

	}

	public void PurchaseMachineGun() {
		Debug.Log ("Purchased Machine Gun");
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

	public GameObject next = null;

	public void UpgradeUnit() {
		GameObject nextUnit = Instantiate(next, this.transform.position, this.transform.rotation);
		nextUnit.SetActive(true);
		Destroy(this.gameObject);
	}

	public bool Upgradable() {
		return this.next != null;
	}
}

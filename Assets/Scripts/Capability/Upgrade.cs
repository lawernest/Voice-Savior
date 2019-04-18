using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

	[SerializeField] private Unit next = null;
	private int upgradeCost;

	public int UpgradeCost {
		get {
			return upgradeCost;
		}
	}

	private void Start() {
		if (next != null) {
			upgradeCost = next.Cost;
		}
	}

	public GameObject UpgradeUnit() {;
		GameObject nextUnit = Instantiate(next.gameObject, this.transform.position, this.transform.rotation);
		nextUnit.SetActive(true);
		Destroy(this.gameObject);
		return nextUnit;
	}
		
	public bool Upgradable() {
		return this.next != null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {

	[SerializeField] private GameObject next = null;

	public void UpgradeUnit(DefenseTower tower) {
		Transform place_point = tower.transform.GetChild(0);
		GameObject nextUnit = Instantiate(next, place_point.position, place_point.rotation);
		tower.PlaceTurret(nextUnit.transform);
		nextUnit.SetActive(true);
		Destroy(this.gameObject);
	}

	public bool Upgradable() {
		return this.next != null;
	}
}

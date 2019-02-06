using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTower : MonoBehaviour {

	public Transform place_point;

	private Transform turret = null;

	public void PlaceTurret(Transform prefab) {
		Transform turret = Instantiate (prefab, place_point.position, place_point.rotation);
		turret.gameObject.SetActive(true);
		this.turret = turret;
	}

	public void RemoveTurret() {
		Destroy(this.turret);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseTower : MonoBehaviour {

	public Transform place_point;
	public Text label;

	private Transform turret = null;

	void Update() {
		label.transform.position = CameraMovement.main_camera.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 5.0f, this.transform.position.z));
	}

	public void PlaceTurret(Transform prefab) {
		this.turret = prefab;
	}

	public Transform GetTurret() {
		return this.turret;
	}

	public void RemoveTurret() {
		Destroy(this.turret.gameObject);
		this.turret = null;
	}
}

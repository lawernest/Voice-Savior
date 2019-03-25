using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseTower : MonoBehaviour {

	[SerializeField] private Transform place_point;
	[SerializeField] private Text label;
	private Transform turret = null;

	public Transform Turret {
		get {
			return turret;
		}
	}

	private void Start() {
		this.turret = null;
	}

	private void Update() {
		label.transform.position = UIManager.LabelPosition(new Vector3 (this.transform.position.x, this.transform.position.y + 5.0f, this.transform.position.z));
	}

	public void PlaceTurret(Transform turret) {
		this.turret = turret;
		turret.SetParent(this.transform);
	}

	public void RemoveTurret() {
		Destroy(this.turret.gameObject);
		this.turret = null;
	}
}

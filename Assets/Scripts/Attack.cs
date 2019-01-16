using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public float radius = 5.0f;

	private GameObject target;
	private Unit unit;

	// Use this for initialization
	void Start () {
		this.unit = this.GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null) {
			SearchEnemy();
		} else {
			AddDamage();
		}
	}

	void SearchEnemy() {
		//search for enemy
		Collider[] enemy = Physics.OverlapSphere (this.transform.position, radius);

		if (enemy.Length == 0) {
			return;
		}

		target = enemy[0].gameObject;
	}

	void AddDamage() {
		this.transform.LookAt (target.transform.position);
		target.gameObject.GetComponent<Unit>().hp -= this.unit.damage; 	
	}
}

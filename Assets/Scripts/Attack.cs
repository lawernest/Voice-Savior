using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

	public float radius = 10.0f;
	public float power = 1.0f;

	private GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SearchEnemy();
		if (target != null) {
			AddDamage();
		}
	}

	// Search for enemy
	void SearchEnemy() {
		Collider[] enemy = Physics.OverlapSphere (this.transform.position, radius);
		if (enemy.Length == 0) {
			target = null;
		} else {
			target = enemy[0].gameObject;
			this.transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
		}
	}

	// Attack the target
	void AddDamage() {
		target.gameObject.GetComponent<Unit>().ReduceHP(this.power);	
	}
}

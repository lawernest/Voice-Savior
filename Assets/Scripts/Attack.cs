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
		Collider[] enemies = Physics.OverlapSphere (this.transform.position, radius);
		target = FindTheClosestTarget(enemies);
		if(target != null) {
			TargetEnemy();
		}
	}

	GameObject FindTheClosestTarget(Collider[] enemies) {
		float closest = radius;
		GameObject target = null;

		foreach (Collider enemy in enemies) {
			float distance = Vector3.Distance (this.transform.position, enemy.transform.position);
			if (distance < closest) {
				closest = distance;
				target = enemy.gameObject;
			}
		}
		/*for (int i = 0; i < enemies.Length; i++) {
			float distance = Vector3.Distance (this.transform.position, enemies [i].transform.position);
			if (distance < closest) {
				closest = distance;
				target = enemies[i].gameObject;
			}
		}*/
		return target;
	}

	// Attack the target
	void AddDamage() {
		target.gameObject.GetComponent<Unit>().ReduceHP(this.power);	
	}

	// Look at the enemy direction
	void TargetEnemy() {
		Vector3 direction = target.transform.position - this.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp (this.transform.rotation, lookRotation, Time.deltaTime * 10.0f).eulerAngles;
		this.transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
	}
}
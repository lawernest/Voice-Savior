using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {

	public float radius = 10.0f;
	public float rotationSpeed = 10.0f;

	private Transform target;
	private Damager damager;

	// Use this for initialization
	void Start () {
		this.damager = this.GetComponent<Damager>();
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.isPause) {
			return;
		}

		SearchEnemy();
		if (target != null) {
			this.damager.SetTarget(target);
		} else {
			this.damager.SetTarget(null);
		}
	}

	// Search for enemies
	void SearchEnemy() {
		Collider[] enemies = Physics.OverlapSphere (this.transform.position, radius);
		if (enemies.Length == 0) {
			target = null;
			return;
		} 

		FindTheClosestTarget(enemies);
		LookAtTarget();
	}

	// Find the closest target to attack
	void FindTheClosestTarget(Collider[] enemies) {
		if (enemies.Length == 1) {
			target = enemies[0].transform;
			return;
		}

		float closest = radius;
		float distance;

		foreach (Collider enemy in enemies) {
			distance = Vector3.Distance (this.transform.position, enemy.transform.position);
			if (distance <= closest) {
				closest = distance;
				target = enemy.transform;
			}
		}
	}

	// Look at the enemy direction
	void LookAtTarget() {
		Vector3 direction = target.position - this.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp (this.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		this.transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
	}
}
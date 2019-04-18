using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour {

	[SerializeField] private float radius = 10.0f;
	[SerializeField] private float rotationSpeed = 10.0f;
	private Transform target;
	private Damager damager;

	private void Start () {
		this.damager = this.GetComponent<Damager>();
	}
		
	private void Update () {
		if (GameManager.instance.isPause ()) {
			return;
		}
		SearchEnemy();
		this.damager.Target = target;
	}

	// Search for enemies
	private void SearchEnemy() {
		// only search for enemy layer
		Collider[] enemies = Physics.OverlapSphere(this.transform.position, radius, LayerMask.GetMask("Enemy")); 
		if (enemies.Length == 0) {
			target = null;
			return;
		}
		FindTheClosestTarget(enemies);
		if (target != null) {
			LookAtTarget();
		}
	}

	// Find the closest target to attack
	private void FindTheClosestTarget(Collider[] enemies) {
		if (enemies.Length == 1 && enemies[0].tag == "Enemy") {
			target = enemies[0].transform;
			return;
		}

		float closest = radius;
		float distance;
		foreach (Collider enemy in enemies) {
			if(enemy.tag == "Enemy") {
				distance = Vector3.Distance(this.transform.position, enemy.transform.position);
				if (distance <= closest) {
					closest = distance;
					target = enemy.transform;
				}
			}
		}
	}

	// Look at the enemy direction
	private void LookAtTarget() {
		Vector3 direction = target.position - this.transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp (this.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
		this.transform.rotation = Quaternion.Euler(0.0f, rotation.y, 0.0f);
	}
}
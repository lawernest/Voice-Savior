using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

	public Transform ammoPrefab;
	public Transform firePoint;
	public float power = 1.0f;
	public float fireRate = 1.0f;

	private Transform target;
	private float countDown = 0.0f;
	private bool attack = false;
	
	// Update is called once per frame
	void Update () {
		if (attack) {
			if (countDown <= 0.0f) {
				Fire();
				countDown = 1.0f / fireRate;
			}

			countDown -= Time.deltaTime;
		}
	}

	public void SetAttack(bool b, Transform enemy) {
		this.attack = b;
		this.target = enemy;
	}

	public void Attack(Unit target) {
		target.ReduceHP(this.power);
	}

	public void Fire() {
		Transform bullet = Instantiate(ammoPrefab, firePoint.position, firePoint.rotation);
		Bullet b = bullet.GetComponent<Bullet>();
		b.Init(this.power, this.target);
	}
}

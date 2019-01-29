﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

	[Header("Basic Setting")]
	public Transform firePoint;

	[Header("Laser Setting")]
	public bool laser = false;
	public LineRenderer laserBeam;

	[Header("Bullet Setting")]
	public bool bullet = true;
	public Transform ammoPrefab;
	public float fireRate = 1.0f;

	private Transform target;
	private float countDown = 0.0f;
	private Unit unit;

	void Start() {
		if (laser) {
			this.laserBeam.gameObject.SetActive(false);
		}
		this.unit = this.gameObject.GetComponent<Unit>();
	}

	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isPause) return;

		if (target != null) {
			if (bullet) {
				if (countDown <= 0.0f) {
					countDown = 1.0f / fireRate;
					FireBullet();
				}
		
				countDown -= Time.deltaTime;

			} else if (laser) {
				FireLaserBeam();
			}
		} else {
			if (laser) {
				this.laserBeam.gameObject.SetActive(false);
			}
		}
	}

	public void SetTarget(Transform enemy) {
		this.target = enemy;
	}

	void FireBullet() {
		Transform bullet = Instantiate (ammoPrefab, this.firePoint.position, this.firePoint.rotation);
		Bullet b = bullet.GetComponent<Bullet> ();
		b.Init(this.unit.damage, this.target);
	}

	void FireLaserBeam() {
		if (this.target != null) {

		}

		if (!this.laserBeam.gameObject.activeInHierarchy) {
			this.laserBeam.gameObject.SetActive(true);
		}

		this.laserBeam.SetPosition(0, firePoint.position);
		this.laserBeam.SetPosition(1, target.position);
		this.target.GetComponent<Unit>().ReduceHP(this.unit.damage);
	}
}
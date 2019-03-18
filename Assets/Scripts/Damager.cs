using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

	[Header("Basic Setting")]
	[SerializeField] private Transform firePoint;

	[Header("Laser Setting")]
	[SerializeField] private bool useLaser = false;
	[SerializeField] private LineRenderer laserBeam;

	[Header("Bullet Setting")]
	[SerializeField] private bool useBullet = true;
	[SerializeField] private Transform ammoPrefab;
	[SerializeField] private float fireRate = 1.0f;

	private Transform target;
	private float countDown = 0.0f;
	private Unit unit;

	private void Start() {
		this.unit = this.gameObject.GetComponent<Unit>();
	}
		
	private void Update () {
		if (GameManager.instance.isPause) 
			return;

		if (this.target != null) {
			if (this.useBullet) {
				if (this.countDown <= 0.0f) {
					this.countDown = 1.0f / this.fireRate;
					FireBullet();
				}
				this.countDown -= Time.deltaTime;

			} else if(this.useLaser) {
				FireLaserBeam();
			}
		} else {
			if (this.useLaser) {
				this.laserBeam.gameObject.SetActive(false);
			} else {
				this.countDown = 0.0f;
			}
		}
	}

	public void SetTarget(Transform enemy) {
		this.target = enemy;
	}

	private void FireBullet() {
		Transform bulletPrefab = Instantiate(ammoPrefab, this.firePoint.position, this.firePoint.rotation);
		Bullet bullet = bulletPrefab.GetComponent<Bullet>();
		bullet.Initialize(this.unit.Damage, this.target);
	}

	private void FireLaserBeam() {
		if (!this.laserBeam.gameObject.activeInHierarchy) {
			this.laserBeam.gameObject.SetActive(true);
		}

		this.laserBeam.SetPosition(0, firePoint.position);
		this.laserBeam.SetPosition(1, target.position);
		this.target.GetComponent<Unit>().ReduceHP(this.unit.Damage);
	}
}
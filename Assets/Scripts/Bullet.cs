using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private float speed = 10.0f;
	private float damage;
	private Transform target;

	public void Initialize(float damage, Transform target) {
		this.damage = damage;
		this.target = target;
	}

	private void FixedUpdate () {
		if (GameManager.instance.isPause) {
			return;
		}

		if (this.target == null) {
			Destroy(this.gameObject);
			return;
		}

		Vector3 direction = this.target.position - this.transform.position;
		float cur_distance = this.speed * Time.deltaTime;

		if (direction.magnitude <= cur_distance) {
			HitTarget();
			return;
		}

		this.transform.Translate(direction.normalized * cur_distance, Space.World);
	}

	private void HitTarget() {
		this.target.GetComponent<Unit>().ReduceHP(this.damage);
		Destroy(this.gameObject);
	}
}
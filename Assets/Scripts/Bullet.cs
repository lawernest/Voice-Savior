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
		if (GameManager.instance.isPause) 
			return;

		if (target == null) {
			Destroy(this.gameObject);
			return;
		}

		Vector3 direction = target.position - this.transform.position;
		float cur_distance = speed * Time.deltaTime;

		if (direction.magnitude <= cur_distance) {
			HitTarget();
			return;
		}

		this.transform.Translate(direction.normalized * cur_distance, Space.World);
	}

	void HitTarget() {
		this.target.GetComponent<Unit>().ReduceHP(this.damage);
		Destroy(this.gameObject);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	[SerializeField] private GameObject explosionPrefab;
	[SerializeField] private float scale = 1.0f;
	private Unit unit;

	private void Start () {
		this.unit = this.GetComponent<Unit>();
	}

	private void Update () {
		if (GameManager.instance.isPause()) {
			return;
		}

		if (this.unit.isDead()) {
			Explode();
			Destroy(this.gameObject);
		}
	}

	private void Explode() {
		GameObject effect = (GameObject)Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
		effect.transform.localScale *= this.scale;
		effect.SetActive(true);
		Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public GameObject explosionPrefab;
	public float scale = 1.0f;

	private Unit unit;

	// Use this for initialization
	void Start () {
		this.unit = this.GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.isPause) {
			return;
		}

		if (this.unit.isDead()) {
			Explode();

			if (this.tag == "Enemy") {
				GameManager.instance.enemies_on_field--;
			}

			Destroy(this.gameObject);
		}
	}

	public void Explode() {
		GameObject effect = (GameObject)Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
		effect.transform.localScale *= scale;
		effect.SetActive(true);
		Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);
	}
}
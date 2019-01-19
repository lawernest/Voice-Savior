using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public GameObject effect_prefab = null;
	public float scale = 1.0f;

	private Unit unit;

	// Use this for initialization
	void Start () {
		this.unit = this.GetComponent<Unit>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.unit.hp <= 0.0f) {
			Explode();
			Destroy(this.gameObject);
		}
	}

	public void Explode() {
		GameObject effect = (GameObject)Instantiate(effect_prefab, this.transform.position, this.transform.rotation);
		effect.transform.localScale *= scale;
		effect.SetActive(true);
		Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);
	}
}

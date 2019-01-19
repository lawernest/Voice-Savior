using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float hp = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.hp <= 0.0f) {
			Death();
		}
	}

	public void Death() {
		Destroy(this.gameObject);
	}

	public void ReduceHP(float damage) {
		this.hp -= damage;
	}
}

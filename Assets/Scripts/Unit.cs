using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float hp = 100.0f;
	public float cost = 10.0f;
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReduceHP(float damage) {
		this.hp -= damage;
	}
}
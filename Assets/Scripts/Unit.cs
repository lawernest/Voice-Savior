using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float hp = 100.0f;
	public float cost = 10.0f;
	public float damage = 0.0f;

	public void InitUnit(float hp, float damage, float cost) {
		this.hp = hp;
		this.damage = damage;
		this.cost = cost;
	}

	public void ReduceHP(float damage) {
		this.hp -= damage;
	}

	public float GetCost() {
		return this.cost;
	}
}
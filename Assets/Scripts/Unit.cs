using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public float hp = 100.0f;
	public float cost = 10.0f;
	public float damage = 0.0f;

	// Default constructor
	public void Init() {
		this.hp = 100.0f;
		this.cost = 10.0f;
		this.damage = 0.0f;
	}

	public void Init(float hp, float cost, float damage) {
		this.hp = hp;
		this.cost = cost;
		this.damage = damage;
	}

	public void ReduceHP(float damage) {
		this.hp -= damage;
	}

	public float GetCost() {
		return this.cost;
	}
}
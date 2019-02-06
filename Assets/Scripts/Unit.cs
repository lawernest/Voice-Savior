using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

	public float max_hp = 100.0f;
	public float cost = 10.0f;
	public float damage = 0.0f;
	public Canvas health_UI;

	private float current_hp;
	private Image health_bar;

	void Start() {
		this.current_hp = this.max_hp;
		if (health_UI != null) {
			health_bar = health_UI.transform.GetChild(1).GetComponent<Image>();
		}
	}
		
	public void InitUnit(float hp, float damage, float cost) {
		this.max_hp = hp;
		this.damage = damage;
		this.cost = cost;
	}

	void Update() {
		if (health_UI != null) {
			UIManager.instance.FaceToCamera(health_UI.transform);
		}
	}

	public void ReduceHP(float damage) {
		this.current_hp -= damage;
		health_bar.fillAmount = current_hp / max_hp;
		UIManager.instance.ChangeHealthBarColor(health_bar);
	}

	public bool isDead() {
		return this.current_hp <= 0.0f;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

	public float max_hp = 100.0f;
	public int cost = 100;
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
		
	public void InitUnit(float hp, float damage, int cost) {
		this.max_hp = hp;
		this.damage = damage;
		this.cost = cost;
	}

	void Update() {
		if (GameManager.instance.isPause) return;

		if (this.isDead ()) {
			if (this.tag == "Enemy") {
				Player.instance.UpdateGoldAmount(this.cost);
				GameManager.instance.enemies_on_field--;
			}
		}

		if (health_UI != null) {
			UIManager.instance.FaceToCamera(health_UI.transform);
		}
	}

	public void ReduceHP(float damage) {
		this.current_hp -= damage;
		if (health_bar != null) {
			health_bar.fillAmount = current_hp / max_hp;
			UIManager.instance.ChangeHealthBarColor (health_bar);
		}
	}

	public bool isDead() {
		return this.current_hp <= 0.0f;
	}
}
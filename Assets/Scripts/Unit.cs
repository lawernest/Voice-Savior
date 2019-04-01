using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour {

	[SerializeField] private float max_hp = 100.0f;
	[SerializeField] private int cost = 100;
	[SerializeField] private float damage = 0.0f;
	[SerializeField] private Canvas health_UI;
	private float current_hp;
	private Image health_bar;

	public int Cost {
		get {
			return cost;
		}
	}

	public float Damage {
		get {
			return damage;
		}
	}

	protected virtual void Start() {		
		this.current_hp = this.max_hp;
		if (health_UI != null) {
			health_bar = health_UI.transform.GetChild(1).GetComponent<Image>();
		}
	}
		
	public virtual void Initialize(float hp, float damage, int cost) {
		this.max_hp = hp;
		this.damage = damage;
		this.cost = cost;
	}

	protected virtual void Update() {
		if (GameManager.instance.isPause()) {
			return;
		}

		if (isDead() && this.tag == "Enemy") {
			Player.instance.IncreaseGold(this.cost);
			GameManager.instance.enemies_on_field--;
		}
		if (health_UI != null) {
			UIManager.FaceToCamera(health_UI.transform);
		}
	}

	public void ReduceHP(float damage) {
		this.current_hp -= damage;
		if (health_bar != null) {
			health_bar.fillAmount = current_hp / max_hp;
			UIManager.ChangeHealthBarColor(health_bar);
		}
	}

	public bool isDead() {
		return this.current_hp <= 0.0f;
	}
}
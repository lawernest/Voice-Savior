using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance { get; private set; }

	private Camera main_camera;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		main_camera = Camera.main;
	}

	public void FaceToCamera(Transform ui_element) {
		ui_element.LookAt(main_camera.transform);
		ui_element.Rotate(0, 180, 0);
	}

	public void ChangeHealthBarColor(Image health_bar) {
		float health = health_bar.fillAmount;
		if (health >= 0.2f && health <= 0.5f) {
			health_bar.color = Color.yellow;
		} else if (health < 0.2f) {
			health_bar.color = Color.red;
		}
	}
}

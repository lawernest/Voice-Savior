using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public static UIManager instance { get; private set; }

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	public void FaceToCamera(Transform ui_element) {
		ui_element.LookAt(CameraMovement.main_camera.transform);
		ui_element.Rotate(0, 180, 0);
	}

	public void ChangeHealthBarColor(Image health_bar) {
		float health = health_bar.fillAmount;
		if (health >= 0.2f && health <= 0.5f)
			health_bar.color = Color.yellow;
		else if (health < 0.2f)
			health_bar.color = Color.red;
	}

	public string TimeFormat(float time) {
		int minutes = (int) time / 60;
		int seconds = (int) time - 60 * minutes;

		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	public void StartGame() {
		SceneManager.LoadScene("Level 0");
	}
}

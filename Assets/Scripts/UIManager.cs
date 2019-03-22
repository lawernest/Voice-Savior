using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager {

	public static void FaceToCamera(Transform ui_element) {
		ui_element.LookAt(CameraMovement.mainCamera.transform);
		ui_element.Rotate(0, 180, 0);
	}

	public static Vector3 LabelPosition(Vector3 pos) {
		return CameraMovement.mainCamera.WorldToScreenPoint(new Vector3(pos.x, pos.y, pos.z));
	}

	public static void ChangeHealthBarColor(Image health_bar) {
		float health = health_bar.fillAmount;
		if (health >= 0.2f && health <= 0.5f)
			health_bar.color = Color.yellow;
		else if (health < 0.2f)
			health_bar.color = Color.red;
	}

	public static string TimeFormat(float time) {
		int minutes = (int) time / 60;
		int seconds = (int) time - 60 * minutes;

		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}

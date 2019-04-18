using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public void NormalButton() {
		GameManager.instance.StartGameInNormalMode();
	}

	public void TimedButton() {
		GameManager.instance.StartGameInTimedMode();
	}

	public void QuitButton() {
		GameManager.instance.QuitGame();
	}

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

	public static string DisplayWaveText(int curWave, int total) {
		return "Wave " + curWave + "/" + total;
	}

	// Display the weapon information
	public static void ShowWeaponInfo(bool show) {
		Controller.weaponInfo.SetActive (false);
		if (show && Controller.selected != null) {
			DefenseTower tower = Controller.selected.GetComponent<DefenseTower>();
			if (tower.Turret != null) {
				Text upgradePrice = Controller.weaponInfo.transform.GetChild (1).GetChild (0).GetComponent<Text> ();
				Text goldObtain = Controller.weaponInfo.transform.GetChild (2).GetChild (0).GetComponent<Text> ();
				Upgrade weapon = tower.Turret.GetComponent<Upgrade> ();

				upgradePrice.text = weapon.Upgradable () ? weapon.UpgradeCost + " Gold" : "N/A";
				goldObtain.text = ((int)tower.Turret.GetComponent<Unit> ().Cost / 2) + " Gold";
				Controller.weaponInfo.transform.position = CameraMovement.mainCamera.WorldToScreenPoint (tower.transform.position) + new Vector3 (0.0f, 80.0f);
				Controller.weaponInfo.SetActive (true);
			}
		}
	}
}

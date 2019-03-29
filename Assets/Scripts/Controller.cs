using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour {

	public static GameObject selected;
	private static GameObject weaponInfo;
	public static int weaponIndex;

	protected virtual void Start() {
		weaponInfo = GameObject.Find("WeaponInfo");
		weaponInfo.SetActive(false);
	}

	protected abstract void Buy(string name);

	protected void ShowWeaponInfo(bool show) {
		if (show && Controller.selected != null) {
			DefenseTower tower = Controller.selected.GetComponent<DefenseTower>();
			if(tower.Turret != null) {
				Text upgradePrice = weaponInfo.transform.GetChild(1).GetChild(0).GetComponent<Text>();
				Text goldObtain = weaponInfo.transform.GetChild(2).GetChild(0).GetComponent<Text>();
				Upgrade weapon = tower.Turret.GetComponent<Upgrade>();
				upgradePrice.text = "N/A";
				if (weapon.Upgradable()) {
					upgradePrice.text = weapon.UpgradeCost + " Gold";
				}
				goldObtain.text = ((int)tower.Turret.GetComponent<Unit>().Cost/2) + " Gold";
				Vector3 offset = new Vector3 (0.0f, 80.0f);
				weaponInfo.transform.position = CameraMovement.mainCamera.WorldToScreenPoint (Controller.selected.transform.position) + offset;
				weaponInfo.SetActive (true);
			}
		} else {
			weaponInfo.SetActive(false);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public static Shop instance { get; private set; }

	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	public void CannonButton() {
		int index = ModelManager.instance.SearchForWeapon("Cannon");
		ProductOnHold(index);
	}

	public void MachineGunButton() {
		int index = ModelManager.instance.SearchForWeapon("Turret");
		ProductOnHold(index);
	}

	public void ProductOnHold(int index) {
		if (index == -1) {
			return;
		}

		GameObject unit = ModelManager.instance.GetWeaponPrefab(index);
		if (unit != null) {
			if (Player.instance.Gold - unit.GetComponent<Unit>().Cost >= 0) {
				VoiceController.weapon_index = index;
			} else {
				VoiceController.weapon_index = -1;
			}
		}
	}

	public void SellTurret(GameObject tower) {
		DefenseTower defense_tower = tower.GetComponent<DefenseTower>();
		Player.instance.IncreaseGold((int)defense_tower.GetTurret().GetComponent<Unit>().Cost/2);
		defense_tower.RemoveTurret();
	}
}

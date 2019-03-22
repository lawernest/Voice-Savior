using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public static Shop instance { get; private set; }

	private void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	public void BuyCannon() {
		VoiceController.instance.WeaponIndex = ProductOnHold("Cannon");
	}

	public void BuyTurret() {
		VoiceController.instance.WeaponIndex = ProductOnHold("Turret");
	}

	public int ProductOnHold(string name) {
		int index = ModelManager.instance.SearchForWeapon(name);
		GameObject unit = ModelManager.instance.GetWeaponPrefab(index);

		if (unit != null) {
			if (Player.instance.Gold - unit.GetComponent<Unit>().Cost >= 0) {
				return index;
			}
		}
		return -1;
	}

	public bool SellTurret(GameObject tower) {
		DefenseTower defense_tower = tower.GetComponent<DefenseTower>();
		if(defense_tower.GetTurret() != null) {
			Player.instance.IncreaseGold((int)defense_tower.GetTurret().GetComponent<Unit>().Cost/2);
			defense_tower.RemoveTurret();
			return true;
		}
		return false;
	}
}

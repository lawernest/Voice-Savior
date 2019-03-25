using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public static Shop instance { get; private set; }

	private void Start() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
	}

	public int ProductOnHold(string name) {
		int index = ModelManager.instance.SearchForWeapon(name);
		GameObject unit = ModelManager.instance.GetWeaponPrefab(index);

		if (unit != null) {
			if (Player.instance.EnoughGold(unit.GetComponent<Unit> ().Cost)) {
				return index;
			}
		}
		return -1;
	}

	public bool Purchase(int weaponIndex, Transform tower) {
		DefenseTower selectedTower = tower.GetComponent<DefenseTower>();
		if (selectedTower.Turret == null) {
			GameObject turret = ModelManager.instance.CreateWeapon(Controller.weaponIndex, tower.GetChild (0)); 
			selectedTower.PlaceTurret(turret.transform);
			Player.instance.ReduceGold(turret.GetComponent<Unit> ().Cost);
			return true;
		}
		return false;
	}

	public bool UpgradeWeapon(Transform tower) {
		DefenseTower selectedTower = tower.GetComponent<DefenseTower>();
		Upgrade turret = selectedTower.Turret.GetComponent<Upgrade>();

		if (selectedTower.Turret != null && turret.Upgradable() && Player.instance.EnoughGold(turret.UpgradeCost)) {
			GameObject upgradedTurret = turret.UpgradeUnit();
			selectedTower.PlaceTurret(upgradedTurret.transform);
			Player.instance.ReduceGold(turret.UpgradeCost);
			return true;
		}
		return false;
	}

	public int Sell(GameObject tower) {
		int gold = 0;
		DefenseTower defense_tower = tower.GetComponent<DefenseTower>();
		if(defense_tower.Turret != null) {
			gold = (int)defense_tower.Turret.GetComponent<Unit>().Cost / 2;
			Player.instance.IncreaseGold(gold);
			defense_tower.RemoveTurret();
		}
		return gold;
	}
}

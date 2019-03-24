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
			if (Player.instance.Gold - unit.GetComponent<Unit>().Cost >= 0) {
				return index;
			}
		}
		return -1;
	}

	public void Purchase(int weaponIndex, Transform tower) {
		GameObject turret = ModelManager.instance.CreateWeapon(Controller.weaponIndex, tower.GetChild(0)); 
		tower.GetComponent<DefenseTower>().PlaceTurret(turret.transform);
		Player.instance.ReduceGold(turret.GetComponent<Unit>().Cost);
	}

	public int Sell(GameObject tower) {
		int gold = 0;
		DefenseTower defense_tower = tower.GetComponent<DefenseTower>();
		if(defense_tower.GetTurret() != null) {
			gold = (int)defense_tower.GetTurret().GetComponent<Unit>().Cost / 2;
			Player.instance.IncreaseGold(gold);
			defense_tower.RemoveTurret();
		}
		return gold;
	}
}

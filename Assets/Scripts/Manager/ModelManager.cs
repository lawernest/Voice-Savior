﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour {

	public static ModelManager instance { get; private set; }

	[SerializeField] private GameObject[] enemyPrefabs;
	[SerializeField] private GameObject[] weaponPrefabs; // Store all the weapons prefab (except the one for upgrade)
	[SerializeField] private GameObject[] towers;

	public GameObject[] WeaponPrefabs {
		get {
			return weaponPrefabs;
		}
	}

	public GameObject[] Towers {
		get {
			return towers;
		}
	}
		
	private void Awake() {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this);
		}
	}

	public GameObject CreateEnemy(int index, Transform place) {
		return Instantiate(enemyPrefabs[index], place.position, place.rotation);
	}

	public GameObject CreateWeapon(int index, Transform place) {
		return Instantiate(weaponPrefabs[index], place.position, place.rotation);
	}

	public int SearchForWeapon(string name) {
		for (int i = 0; i < weaponPrefabs.Length; i++) {
			if (weaponPrefabs[i].name == name) {
				return i;
			}
		}
		return -1;
	}

	public GameObject GetWeaponPrefab(int index) {
		if (index >= 0 && index < weaponPrefabs.Length) {
			return this.weaponPrefabs[index];
		}
		return null;
	}

	public GameObject GetTowerByName(string name) {
		for (int i = 0; i < towers.Length; i++) {
			if (towers[i].name == name) {
				return towers[i];
			}
		}
		return null;
	}
}
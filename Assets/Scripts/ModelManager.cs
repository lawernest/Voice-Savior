using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour {

	public static ModelManager instance;

	public GameObject[] enemyPrefabs;
	public GameObject[] weaponPrefabs; // Store all the weapons prefab (except the one for uprade)

	// Use this for initialization
	void Awake () {
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
		return Instantiate(weaponPrefabs [index], place.position, place.rotation);
	}
}

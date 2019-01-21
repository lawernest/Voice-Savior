using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	[System.Serializable]
	public struct WaveInfo {
		public Transform[] enemy;
		public float[] hp;
		public float[] damage;
		public float cost;
	}
		
	public WaveInfo waveInfo;
	public float nextWaveTime = 10.0f;
	public int totalEnemies;
	[HideInInspector]public int size;

	void Start() {
		this.size = waveInfo.enemy.Length;
	}

	public float GetEnemyHP(int index) {
		return this.waveInfo.hp[index];
	}

	public float GetEnemyDamage(int index) {
		return this.waveInfo.damage[index];
	}

	public float GetEnemyCost() {
		return this.waveInfo.cost;
	}

	public Transform GetEnemyPrefab(int index) {
		return this.waveInfo.enemy[index];
	}
}

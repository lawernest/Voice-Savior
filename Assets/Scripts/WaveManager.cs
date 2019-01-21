using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	[Header("Enemy Setting")]
	public Transform destination;
	public Transform player_base;

	[Header("Wave Setting")]
	public Wave[] waves;
	public Transform spawnPoint;

	private float countdown = 1.0f;
	private int waveNum = 0;
	private Wave cur_Wave;

	public static int enemies_on_field = 0;

	void Start() {
		cur_Wave = waves[waveNum];
	}

	// Update is called once per frame
	void Update() {
		if (GameManager.isPause || waveNum == waves.Length) {
			return;
		}

		//TimedWave();
		NormalWave();
	}

	// Start the wave
	IEnumerator StartWave() {
		int enemyPrefab;
		for (int i = 0; i < cur_Wave.totalEnemies; i++) {
			enemyPrefab = Random.Range(0, cur_Wave.size);
			SpawnEnemy(enemyPrefab);
			WaveManager.enemies_on_field++;
			yield return new WaitForSeconds (1.0f); // Wait for a second before spawning the next enemy
		}
		waveNum++;
	}

	// Wave start according to time
	void TimedWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(StartWave());
			countdown = cur_Wave.nextWaveTime;
			cur_Wave = waves[waveNum];
		}

		countdown -= Time.deltaTime;
	}

	// Wave start after all enemies are clear 
	void NormalWave() {
		if (WaveManager.enemies_on_field == 0) {
			TimedWave ();
		}

	}

	void SpawnEnemy(int prefabIndex) {
		Transform prefab = cur_Wave.GetEnemyPrefab(prefabIndex);
		Transform newEnemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

		EnemyAI enemy_ai = newEnemy.GetComponent<EnemyAI>();
		Unit unit = newEnemy.GetComponent<Unit>();

		unit.Init(cur_Wave.GetEnemyHP(prefabIndex), cur_Wave.GetEnemyCost(), cur_Wave.GetEnemyDamage(prefabIndex));
		enemy_ai.Init(destination, player_base);

		newEnemy.gameObject.SetActive(true);
	}
}
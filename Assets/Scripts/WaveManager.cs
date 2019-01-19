using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public Transform[] enemyPrefab;
	public Transform spawnPoint;
	public Transform destination;
	public Unit player_base;
	public int totalWaves = 5;
	public float nextWaveTime = 5.0f;

	private float countdown = 1.0f;
	private int waveNumber = 1;
	
	// Update is called once per frame
	void Update() {
		if (countdown <= 0.0f && waveNumber <= totalWaves) {
			StartCoroutine(StartWave());
			countdown = nextWaveTime;
		}

		countdown -= Time.deltaTime;
	}

	// Start the wave
	IEnumerator StartWave() {
		for (int i = 0; i < waveNumber; i++) {
			SpawnEnemy ();
			yield return new WaitForSeconds (1.0f);
		}
		waveNumber++;
	}
		
	// Spawn enemy for the wave
	void SpawnEnemy() {
		int type = Random.Range (0, 2);
		Transform newEnemy = Instantiate (enemyPrefab[type], spawnPoint.position, spawnPoint.rotation);
		EnemyAI ai = newEnemy.GetComponent<EnemyAI>();
		ai.Init(this.destination, this.player_base);
		newEnemy.gameObject.SetActive(true);
	}
}
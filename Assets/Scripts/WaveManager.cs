using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	public Transform[] enemyPrefab;
	public Transform spawnPoint;

	public int totalWaves = 5;
	public float nextWaveTime = 5.0f;

	private float countdown;
	private int waveNumber;

	// Use this for initialization
	void Start () {
		countdown = 1.0f;
		waveNumber = 1;
	}
	
	// Update is called once per frame
	void Update () {
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
		Transform newEnemy = Instantiate (enemyPrefab[0], spawnPoint.position, spawnPoint.rotation);
		newEnemy.gameObject.SetActive(true);
	}
}
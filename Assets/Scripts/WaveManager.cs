using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	[Header("Level Setting")]
	public Transform[] enemyPrefabs;
	public Transform spawnPoint;
	public Transform destination;
	public Transform attack_point;

	[Header("Wave Setting")]
	public Wave[] waves;

	private float countdown = 1.0f;
	private int waveNum = 0;

	public bool mode = false; //Normal wave

	// Update is called once per frame
	void Update() {
		if (GameManager.isPause || waveNum >= waves.Length) {
			//Debug.Log("End");
			return; 
		}

		if (mode)
			TimedWave ();
		else
			NormalWave();
	}

	// Initialize the wave
	IEnumerator InitWave() {
		int index;
		string[] sequence = waves[waveNum].enemySequence.Split(' ');

		foreach (string type in sequence) {
			index = System.Int32.Parse(type);
			SpawnEnemy(index);
			GameManager.enemies_on_field++;
			yield return new WaitForSeconds (1.0f); 
		}
			
		waveNum++;
		//Debug.Log (waveNum);
	}

	// Wave start according to time
	void TimedWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			countdown = waves[waveNum].nextWaveTime;
		}

		countdown -= Time.deltaTime;
	}

	// Wave start after all enemies are clear 
	void NormalWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			countdown = waves[waveNum].nextWaveTime;
		}

		if (GameManager.enemies_on_field == 0) {
			Debug.Log (countdown);
			countdown -= Time.deltaTime;
		} 
	}

	void SpawnEnemy(int prefabIndex) {
		Transform newEnemy = Instantiate(enemyPrefabs[prefabIndex], spawnPoint.position, spawnPoint.rotation);

		EnemyAI enemy_ai = newEnemy.GetComponent<EnemyAI>();
		Unit unit = newEnemy.GetComponent<Unit>();

		// Initialization
		unit.InitUnit(waves[waveNum].hpData[prefabIndex], waves[waveNum].damageData[prefabIndex], waves[waveNum].moneyDrop);
		enemy_ai.InitEnemyAI(destination, attack_point);

		newEnemy.gameObject.SetActive(true);
	}
}
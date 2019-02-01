using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

	[Header("Level Setting")]
	public Transform spawnPoint;
	public Transform destination;
	public Transform attack_point;

	[Header("Wave Setting")]
	public Wave[] waves;

	private float countdown = 1.0f;
	private int waveNum = 0;

	// Update is called once per frame
	void Update() {
		if (GameManager.instance.isPause) {
			//Debug.Log("End");
			return; 
		}

		if (waveNum >= waves.Length) {
			this.gameObject.SetActive(false);
		}

		if (GameManager.instance.mode == 1)
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
			GameManager.instance.enemies_on_field++;
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
			GameManager.instance.UpdateWaveText(waveNum + 1, waves.Length);
		}

		if (GameManager.instance.enemies_on_field == 0) {
			//Debug.Log (countdown);
			countdown -= Time.deltaTime;
		} 
	}

	void SpawnEnemy(int prefabIndex) {
		GameObject newEnemy = ModelManager.instance.CreateEnemy(prefabIndex, spawnPoint);

		EnemyAI enemy_ai = newEnemy.GetComponent<EnemyAI>();
		Unit unit = newEnemy.GetComponent<Unit>();

		// Initialization
		unit.InitUnit(waves[waveNum].hpData[prefabIndex], waves[waveNum].damageData[prefabIndex], waves[waveNum].moneyDrop);
		enemy_ai.InitEnemyAI(destination, attack_point);

		newEnemy.SetActive(true);
	}
}
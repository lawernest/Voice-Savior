using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	[Header("Level Setting")]
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Transform destination;

	[Header("Wave Setting")]
	[SerializeField] private Wave[] waves;
	[SerializeField] private Text waveText;
	[SerializeField] private Text counter;

	private Transform attack_point;
	private const float normalWaitTime = 10.0f;
	private float countdown;
	private int waveNum;
	private Text timer;

	void Start() {
		this.attack_point = GameManager.instance.player_base.transform;
		this.timer = this.counter.transform.GetChild(0).GetComponent<Text>();
		this.waveNum = 0;
		this.countdown = normalWaitTime;
		UpdateWaveText();
	}

	// Update is called once per frame
	void Update() {
		if (GameManager.instance.isPause)
			return;

		if (waveNum >= waves.Length)
			this.gameObject.SetActive(false);

		if (GameManager.instance.mode == 1)
			TimedWave();
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
	}

	// Wave start according to time
	void TimedWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			countdown = waves[waveNum].nextWaveTime;
			UpdateWaveText();
		}

		countdown -= Time.deltaTime;
	}

	// Wave start after all enemies are clear 
	void NormalWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			countdown = normalWaitTime;
			UpdateWaveText();
		}

		if (GameManager.instance.enemies_on_field == 0) {
			countdown -= Time.deltaTime;
			UpdateCounter();
		}
	}

	private void SpawnEnemy(int prefabIndex) {
		GameObject newEnemy = ModelManager.instance.CreateEnemy(prefabIndex, spawnPoint);
		EnemyAI enemy_ai = newEnemy.GetComponent<EnemyAI>();
		Unit unit = newEnemy.GetComponent<Unit>();
		Wave curWave = waves[waveNum];

		// Initialization
		unit.Initialize(curWave.hpData[prefabIndex], curWave.damageData[prefabIndex], curWave.moneyDrop);
		enemy_ai.Initialize(destination, attack_point);
		newEnemy.SetActive(true);
	}

	private void UpdateWaveText() {
		this.waveText.text = "Wave " + (waveNum + 1) + "/" + waves.Length;
	}

	private void UpdateCounter() {
		if(!counter.IsActive()) {
			counter.gameObject.SetActive(true);
		}

		this.timer.text = UIManager.instance.TimeFormat(countdown);
		if (countdown <= 0.0f) {
			counter.gameObject.SetActive(false);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	[Header("Level Setting")]
	public Transform spawnPoint;
	public Transform destination;

	[Header("Wave Setting")]
	public Wave[] waves;
	public Text waveText;
	public Text counter;

	private Transform attack_point;
	private const float normalWaitTime = 10.0f;
	private float countdown;
	private int waveNum;
	private Text timer;

	void Start() {
		attack_point = GameManager.instance.player_base.transform;
		timer = counter.transform.GetChild(0).GetComponent<Text>();
		waveNum = 0;
		countdown = normalWaitTime;
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

	void SpawnEnemy(int prefabIndex) {
		GameObject newEnemy = ModelManager.instance.CreateEnemy(prefabIndex, spawnPoint);
		EnemyAI enemy_ai = newEnemy.GetComponent<EnemyAI>();
		Unit unit = newEnemy.GetComponent<Unit>();

		// Initialization
		unit.InitUnit(waves[waveNum].hpData[prefabIndex], waves[waveNum].damageData[prefabIndex], waves[waveNum].moneyDrop);
		enemy_ai.InitEnemyAI(destination, attack_point);
		newEnemy.SetActive(true);
	}

	void UpdateWaveText() {
		this.waveText.text = "Wave " + (waveNum + 1) + "/" + waves.Length;
	}

	void UpdateCounter() {
		if(!counter.IsActive()) {
			counter.gameObject.SetActive(true);
		}

		this.timer.text = UIManager.instance.TimeFormat(countdown);

		if (countdown <= 0.0f) {
			counter.gameObject.SetActive(false);
		}
	}
}
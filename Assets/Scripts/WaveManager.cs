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

	private Transform enemyParent;
	private const float normalWaitTime = 6.0f;
	private float countdown;
	private int waveNum;
	private bool isLast;

	private delegate void WaveMode();
	private WaveMode startWave;

	private void Start() {
		this.enemyParent = GameObject.Find("Enemy").transform;
		this.waveNum = 1;
		this.countdown = normalWaitTime;
		this.isLast = false;
		UpdateWaveText();

		if (GameManager.instance.GameMode == GameManager.Mode.Timed) {
			startWave = new WaveMode(TimedWave);
		} else {
			startWave = new WaveMode(NormalWave);
		}
	}

	// Update is called once per frame
	private void Update() {
		if (GameManager.instance.isPause) {
			return;
		}

		if (this.waveNum > this.waves.Length) {
			this.gameObject.SetActive(false);
		}

		startWave();
	}

	// Initialize the wave
	IEnumerator InitWave() {
		int index;
		string[] sequence = this.waves[waveNum-1].enemySequence.Split(',');

		if (GameManager.instance.GameMode == GameManager.Mode.Timed && this.waveNum >= this.waves.Length) {
			this.isLast = true;
		}

		foreach (string type in sequence) {
			index = System.Int32.Parse(type);
			SpawnEnemy(index);
			GameManager.instance.enemies_on_field++;
			yield return new WaitForSeconds(2.0f); 
		}

		if (this.waveNum <= this.waves.Length) {
			this.waveNum++;
		}
	}

	// Wave start according to time
	private void TimedWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			countdown = waves[waveNum-1].nextWaveTime;
			UpdateWaveText();
		}
			
		if (!this.isLast) {
			countdown -= Time.deltaTime;
			UpdateCounter();
		} else {
			this.counter.gameObject.SetActive(false);
		}

	}

	// Wave start after all enemies are clear 
	private void NormalWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			this.countdown = normalWaitTime;
			UpdateWaveText();
		}

		if (GameManager.instance.enemies_on_field == 0) {
			this.countdown -= Time.deltaTime;
			UpdateCounter();
		}
	}

	private void SpawnEnemy(int prefabIndex) {
		GameObject newEnemy = ModelManager.instance.CreateEnemy(prefabIndex, spawnPoint);
		EnemyAI enemy_ai = newEnemy.GetComponent<EnemyAI>();
		Unit unit = newEnemy.GetComponent<Unit>();
		Wave curWave = waves[waveNum-1];

		// Initialization
		unit.Initialize(curWave.hpData, curWave.damageData, curWave.moneyDrop);
		enemy_ai.Initialize(destination, GameManager.instance.playerBase.transform);
		newEnemy.transform.SetParent(enemyParent);
		newEnemy.SetActive(true);
	}

	private void UpdateWaveText() {
		this.waveText.text = "Wave " + waveNum + "/" + waves.Length;
	}

	private void UpdateCounter() {
		if(!this.counter.IsActive()) {
			this.counter.gameObject.SetActive(true);
		}
		this.counter.text = UIManager.TimeFormat(countdown);
		if (this.countdown <= 0.0f && GameManager.instance.GameMode == GameManager.Mode.Normal) {
			this.counter.gameObject.SetActive(false);
		}
	}
}
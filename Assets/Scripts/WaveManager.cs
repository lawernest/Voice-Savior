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
	private const float normalWaitTime = 5.0f;
	private float countdown;
	private int waveNum;

	private delegate void WaveMode();
	private WaveMode startWave;

	private void Start() {
		this.enemyParent = GameObject.Find("Enemy").transform;
		this.waveNum = 0;
		this.countdown = normalWaitTime;
		UpdateWaveText();

		if (GameManager.instance.mode == GameManager.Mode.Timed) {
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

		if (this.waveNum >= this.waves.Length) {
			this.gameObject.SetActive (false);
		}

		startWave ();
	}

	// Initialize the wave
	IEnumerator InitWave() {
		int index;
		string[] sequence = this.waves[waveNum].enemySequence.Split(',');

		foreach (string type in sequence) {
			index = System.Int32.Parse(type);
			SpawnEnemy(index);
			GameManager.instance.enemies_on_field++;
			yield return new WaitForSeconds(1.0f); 
		}
			
		this.waveNum++;
	}

	// Wave start according to time
	private void TimedWave() {
		if (countdown <= 0.0f) {
			StartCoroutine(InitWave());
			countdown = waves[waveNum].nextWaveTime;
			UpdateWaveText();
		}

		countdown -= Time.deltaTime;
		UpdateCounter();
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
		Wave curWave = waves[waveNum];

		// Initialization
		unit.Initialize(curWave.hpData[prefabIndex], curWave.damageData[prefabIndex], curWave.moneyDrop);
		enemy_ai.Initialize(destination, GameManager.instance.playerBase.transform);
		newEnemy.transform.SetParent(enemyParent);
		newEnemy.SetActive(true);
	}

	private void UpdateWaveText() {
		this.waveText.text = "Wave " + (waveNum + 1) + "/" + waves.Length;
	}

	private void UpdateCounter() {
		if(!this.counter.IsActive()) {
			this.counter.gameObject.SetActive(true);
		}
		this.counter.text = UIManager.TimeFormat(countdown);
		if (this.countdown <= 0.0f && GameManager.instance.mode == GameManager.Mode.Normal) {
			this.counter.gameObject.SetActive(false);
		}
	}
}
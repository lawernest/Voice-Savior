﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	[Header("Wave Setting")]
	public string enemySequence; // order of how the enemies is going to spawn
	public float nextWaveTime; // time to start the next wave when this wave is ended

	[Header("Enemy Setting")]
	// enemy status for this wave
	public float hpData;
	public float damageData;
	public int moneyDrop;
}

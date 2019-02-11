using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	[Header("Wave Setting")]
	public int enemyType;
	public string enemySequence;
	public float nextWaveTime = 10.0f;

	[Header("Enemy Setting")]
	public float[] hpData;
	public float[] damageData;
	public int moneyDrop;
}

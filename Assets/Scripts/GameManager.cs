using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Unit player_base;
	public static bool isPause = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			isPause = true;
		} else if (Input.GetMouseButtonDown (2)) {
			isPause = false;
		}
	}
}

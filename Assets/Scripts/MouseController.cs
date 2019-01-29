using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	private RaycastHit hitInfo;
	private bool hit;
	private Camera main_camera;

	// Use this for initialization
	void Start () {
		hitInfo = new RaycastHit();
		main_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics.Raycast(main_camera.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) {
				Debug.Log ("Hit " + hitInfo.transform.gameObject.name);
			}
		}
	}
}

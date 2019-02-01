using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	public static GameObject selected;

	private RaycastHit hitInfo;
	private bool hit;
	private Camera main_camera;

	// Use this for initialization
	void Awake () {
		hitInfo = new RaycastHit();
		main_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			hit = Physics.Raycast (main_camera.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if(hit) {
				//To-Do
				if (hitInfo.transform.tag == "Weapon") {
					MouseController.selected = hitInfo.transform.gameObject;
				} else if (hitInfo.transform.tag == "Defense Tower") {
					MouseController.selected = hitInfo.transform.gameObject;
				}
				Debug.Log ("Hit " + hitInfo.transform.name);
			} else {
				MouseController.selected = null;
			}
		} 
	}
}

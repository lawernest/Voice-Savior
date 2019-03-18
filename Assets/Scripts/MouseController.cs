using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Controller {

	private RaycastHit hitInfo;
	private bool hit;

	// Use this for initialization
	private void Awake () {
		hitInfo = new RaycastHit();
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButtonDown (0)) {
			hit = Physics.Raycast (CameraMovement.main_camera.ScreenPointToRay (Input.mousePosition), out hitInfo);
			if (hit) {
				//To-Do
				if (hitInfo.transform.tag == "Weapon") {
					Controller.selected = hitInfo.transform.gameObject;
				} else if (hitInfo.transform.tag == "Defense Tower") {
					Controller.selected = hitInfo.transform.gameObject;
				}
				Debug.Log ("Hit " + hitInfo.transform.name);
			} else {
				Controller.selected = null;
			}
		} 
	}	

	protected override void UpdateLog() {

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Controller {

	private RaycastHit hitInfo;

	// Use this for initialization
	private void Start () {
		this.hitInfo = new RaycastHit();
	}
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButtonDown (0)) {
			bool hit = Physics.Raycast (CameraMovement.mainCamera.ScreenPointToRay (Input.mousePosition), out this.hitInfo);
			if(hit) {
				//To-Do
				if (this.hitInfo.transform.tag == "Weapon") {
					Controller.selected = this.hitInfo.transform.gameObject;
				} else if (hitInfo.transform.tag == "Defense Tower") {
					Controller.selected = this.hitInfo.transform.gameObject;
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

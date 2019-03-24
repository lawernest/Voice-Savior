using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : Controller {

	public static MouseController instance { get; private set; }
	private RaycastHit hitInfo;

	// Use this for initialization
	private void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}

		this.hitInfo = new RaycastHit();
	}

	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButtonDown (0)) {
			bool hit = Physics.Raycast (CameraMovement.mainCamera.ScreenPointToRay (Input.mousePosition), out this.hitInfo);
			if(hit /*&& this.hitInfo.transform.tag == "Defense Tower"*/) {
				Controller.selected = this.hitInfo.transform.gameObject;
				GameLog.instance.UpdateLog("Selected Tower " + Controller.selected.name);
			} else {
				Controller.selected = null;
			}
		} 
	}

	public void BuyCannon() {
		Buy("Cannon");
	}

	public void BuyTurret() {
		Buy("Turret");
	}

	protected override void Buy(string name) {
		Controller.weaponIndex= Shop.instance.ProductOnHold(name);
		GameLog.instance.UpdateLog("Purchased " + name);
	}
}

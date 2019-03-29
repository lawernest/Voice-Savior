using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseController : Controller {

	public static MouseController instance { get; private set; }
	private RaycastHit hitInfo;

	// Use this for initialization
	protected override void Start () {
		//base.Start();
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(this.gameObject);
		}
		this.hitInfo = new RaycastHit();
	}

	// To-Do select weapon to upgrade/see
	private void Update () {
		if (Input.GetMouseButtonDown(0)) {
			bool hit = Physics.Raycast (CameraMovement.mainCamera.ScreenPointToRay (Input.mousePosition), out this.hitInfo);
			if(hit && this.hitInfo.transform.tag == "Defense Tower" && !EventSystem.current.IsPointerOverGameObject()) {
				CameraMovement.movable = false;
				Controller.selected = this.hitInfo.transform.gameObject;
				GameLog.instance.UpdateLog("selected Tower " + Controller.selected.name);
				ShowWeaponInfo(true);
			} else {
				CameraMovement.movable = true;
				Controller.selected = null;
				ShowWeaponInfo(false);
			}
		}
	}

	public void BuyCannon() {
		this.Buy("Cannon");
	}

	public void BuyTurret() {
		this.Buy("Turret");
	}

	protected override void Buy(string name) {
		Controller.weaponIndex = Shop.instance.ProductOnHold(name);
		GameLog.instance.UpdateLog("purchased " + name);
	}
}

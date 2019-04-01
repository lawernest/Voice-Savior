using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

	// To-Do select weapon to upgrade/see
	private void Update() {
		if (Input.GetMouseButtonDown(0) && GameManager.instance.inGame()) {
			CameraMovement.moveDirection = CameraMovement.Direction.Center;
			bool hit = Physics.Raycast (CameraMovement.mainCamera.ScreenPointToRay(Input.mousePosition), out this.hitInfo);
			if(hit && this.hitInfo.transform.tag == "Defense Tower" && !EventSystem.current.IsPointerOverGameObject()) {
				TargetIsSelected(true);
				GameLog.instance.UpdateLog("Selected Tower " + Controller.selected.name);
			} else {
				TargetIsSelected(false);
			}
		}
	}

	private void TargetIsSelected(bool isSelected) {
		VoiceController.instance.command = isSelected ? VoiceController.Command.Select : VoiceController.Command.None;
		Controller.selected = isSelected ? this.hitInfo.transform.gameObject : null;
		CameraMovement.movable = !isSelected;
		UIManager.ShowWeaponInfo(isSelected);

	}

	public override void Buy(string name) {
		Controller.weaponIndex = Shop.instance.ProductOnHold(name);
		VoiceController.instance.command = VoiceController.Command.Buy;
		GameLog.instance.UpdateLog("Buy " + name);
	}
}

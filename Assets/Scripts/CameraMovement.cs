using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	[Header("Camera Setting")]
	public float dragSpeed = 1.5f;

	public float MIN_X = 40.0f;
	public float MAX_X = 80.0f;
	public float MIN_Y = 30.0f;
	public float MAX_Y = 75.0f;

	private Vector3 dragPosition;

	// Update is called once per frame
	void Update () {
		// Skip all the updates when the game is paused
		if (GameManager.isPause) {
			return;
		}

		// Get the mouse position when the player has left cliked
		if (Input.GetMouseButtonDown(0)) {
			dragPosition = Input.mousePosition;
		}

		// return when the player released the left click
		if (!Input.GetMouseButton(0)) {
			return;
		}

		//Move the camera based on the mouse dragged position and current mouse position
		Vector3 pos = Camera.main.ScreenToViewportPoint(dragPosition - Input.mousePosition);
		this.transform.Translate(pos.x * dragSpeed, 0.0f, pos.y * dragSpeed, Space.World);

		// Limit the camera movement within the restricted area
		this.transform.position = new Vector3(Mathf.Clamp (this.transform.position.x, MIN_X, MAX_X), this.transform.position.y, Mathf.Clamp (this.transform.position.z, MIN_Y, MAX_Y));
	}
}

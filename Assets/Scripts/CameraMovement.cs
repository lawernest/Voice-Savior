using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public enum Direction { 
		Center,
		North,
		East,
		South, 
		West
	};

	public static Camera mainCamera;
	public static Direction moveDirection;
	public static bool movable;

	[Header("Camera Setting")]
	[SerializeField] private float dragSpeed = 1.5f;
	[SerializeField] private float MIN_X = 40.0f;
	[SerializeField] private float MAX_X = 80.0f;
	[SerializeField] private float MIN_Y = 30.0f;
	[SerializeField] private float MAX_Y = 75.0f;
	private Vector3 dragPosition;

	private void Start() {
		CameraMovement.mainCamera = Camera.main;
		CameraMovement.moveDirection = Direction.Center;
		CameraMovement.movable = true;
	}

	// Update is called once per frame
	private void Update () {
		// Skip all the updates when the game is paused
		if (GameManager.instance.isPause) 
			return;

		// For voice control
		if (moveDirection != Direction.Center) {
			Move();
			return;
		}

		if (!CameraMovement.movable) {
			return;
		}

		// Get the mouse position when the player has left cliked
		if (Input.GetMouseButtonDown(0)) {
			dragPosition = Input.mousePosition;
			Controller.selected = null;
		}

		// return when the player released the left click
		if (!Input.GetMouseButton(0)) {
			return;
		}

		//Move the camera based on the mouse dragged position and current mouse position
		Vector3 position = mainCamera.ScreenToViewportPoint(dragPosition - Input.mousePosition);
		this.transform.Translate(position.x * dragSpeed, 0.0f, position.y * dragSpeed, Space.World);

		// Limit the camera movement within the restricted area
		this.transform.position = new Vector3(
			Mathf.Clamp (this.transform.position.x, MIN_X, MAX_X), 
			this.transform.position.y, 
			Mathf.Clamp (this.transform.position.z, MIN_Y, MAX_Y));
	}

	public void LookAt(Transform target) {
		this.transform.position = new Vector3(
			Mathf.Clamp (target.position.x, MIN_X, MAX_X), 
			this.transform.position.y, 
			Mathf.Clamp (target.position.z - 10, MIN_Y, MAX_Y));
	}

	private void Move() {
		switch (moveDirection) {
		case Direction.North:
			this.transform.Translate(0.0f, 0.0f, dragSpeed/5, Space.World);
			break;
		case Direction.East:
			this.transform.Translate(dragSpeed/5, 0.0f, 0.0f, Space.World);
			break;
		case Direction.South:
			this.transform.Translate(0.0f, 0.0f, -dragSpeed/5, Space.World);
			break;
		case Direction.West:
			this.transform.Translate(-dragSpeed/5, 0.0f, 0.0f, Space.World);
			break;
		}

		this.transform.position = new Vector3(
			Mathf.Clamp (this.transform.position.x, MIN_X, MAX_X), 
			this.transform.position.y, 
			Mathf.Clamp (this.transform.position.z, MIN_Y, MAX_Y));

		if ((this.transform.position.x == MIN_X && moveDirection == Direction.West) ||
		    (this.transform.position.x == MAX_X && moveDirection == Direction.East) ||
		    (this.transform.position.z == MIN_Y && moveDirection == Direction.South) ||
		    (this.transform.position.z == MAX_X && moveDirection == Direction.North)) {
			moveDirection = Direction.Center;
		}
	}
}
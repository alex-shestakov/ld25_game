using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {
	
	public float movementSpeed = 1f;
	public float accelerationSpeed = 1f;
	public float friction = 0.6f;
	
	public float rotationSpeed = 1f;
	
	private float initialYValue;
	private CharacterController controller;
	private Vector3 movementDirection = Vector3.zero;
	
	// Use this for initialization
	void Start() {
		controller = GetComponent<CharacterController>();
		initialYValue = transform.position.y;
	}
	
	void Update() {
		HandleInput();
		ProcessMovement();
	}
	
	void HandleInput() {
		float threshold = 0.3f;
		if (Mathf.Abs(Input.GetAxis("Horizontal")) > threshold) {
			movementDirection.x += Input.GetAxis("Horizontal") * Time.deltaTime * accelerationSpeed;
			movementDirection.x = Mathf.Clamp(movementDirection.x, -movementSpeed, movementSpeed);
		}
		else if (Mathf.Abs(movementDirection.x) > 0.05f)
			movementDirection.x *= friction;
		else 
			movementDirection.x = 0f;
		
		if (Mathf.Abs(Input.GetAxis("Vertical")) > threshold) {
			movementDirection.z += Input.GetAxis("Vertical") * Time.deltaTime * accelerationSpeed;
			movementDirection.z = Mathf.Clamp(movementDirection.z, -movementSpeed, movementSpeed);
		}
		else if (Mathf.Abs(movementDirection.z) > 0.05f)
			movementDirection.z *= friction;
		else 
			movementDirection.z = 0f;
	}
	
	void ProcessMovement() {
		if (movementDirection.sqrMagnitude > movementSpeed * movementSpeed) {
			movementDirection.Normalize();
			movementDirection *= movementSpeed;
		}
		controller.Move(movementDirection);
		
	}
	
	 void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.CompareTag("RidingHood")) {
			hit.gameObject.SendMessage("OnKilled");
		}
    }
}

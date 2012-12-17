using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {
	
	public float movementSpeed = 1f;
	public float accelerationSpeed = 1f;
	public float friction = 0.6f;
	
	public float rotationSpeed = 1f;
	
	public float bonusSpeedMultiplier = 1.6f;
	
	private float initialYValue;
	private CharacterController controller;
	private Vector3 movementDirection = Vector3.zero;
	
	private PlayerStateManager stateManager;
	private bool paralized;
	
	private AudioSource munchSound;
	
	void Awake() {
		stateManager = GetComponent<PlayerStateManager>();		
	}
	
	void Start() {
		controller = GetComponent<CharacterController>();
		initialYValue = transform.position.y;
		paralized = true;
		munchSound = GetComponent<AudioSource>();
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
		
		if (paralized)
			movementDirection *= 0.8f;
		
		controller.Move(movementDirection);
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.CompareTag("RidingHood")) {
			AudioSource.PlayClipAtPoint(munchSound.clip, transform.position);
			stateManager.MetObject(HealthController.FoodType.RedHood);
			hit.gameObject.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
		}
		
		if (hit.gameObject.CompareTag("Hunter")) {
			if (stateManager.MetObject(HealthController.FoodType.Hunter)) {
				AudioSource.PlayClipAtPoint(munchSound.clip, transform.position);
				hit.gameObject.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
			}
			else {
				//SendMessage("OnCollisionWithProjectile", SendMessageOptions.DontRequireReceiver);
			}
		}
		
		if (hit.gameObject.CompareTag("Granny")) {
			AudioSource.PlayClipAtPoint(munchSound.clip, transform.position);
			stateManager.MetObject(HealthController.FoodType.Granny);
			hit.gameObject.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
		}
		
		if (hit.gameObject.CompareTag("Projectile")) {
			print ("OMG! I'm hit!");	
		}
    }
	
	void OnCollisionWithProjectile() {
		print ("Hit!");
	}
	
	void BonusActivated() {
		movementSpeed *= bonusSpeedMultiplier;
	}
	
	void BonusDeactivated() {
		movementSpeed /= bonusSpeedMultiplier;
	}
	
	void OnCountdownFinished () {
		paralized = false;	
	}
}

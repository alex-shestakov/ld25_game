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
	private bool paralized = true;
	
	public AudioSource munchSound;
	public AudioSource  stepSound;
	
	void Awake() {
		stateManager = GetComponent<PlayerStateManager>();		
	}
	
	void Start() {
		controller = GetComponent<CharacterController>();
		initialYValue = transform.position.y;
		//paralized = true;
		//munchSound = GetComponent<AudioSource>();
	}
	
	void Update() {
		HandleInput();
		ProcessMovement();
	}
	
	private void UpdateStepSound(bool play)
	{
		if (play && ! stepSound.isPlaying && paralized)
		{
			stepSound.Play();
		}
		
		if ((! play || ! paralized) && stepSound.isPlaying)
		{
			stepSound.Stop();
		}
	}
	
	void HandleInput() {
		bool isMoving = false;
		float threshold = 0.3f;
		float deltaTime = Time.deltaTime;
		
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");
		
		if (Mathf.Abs(hAxis) > threshold) 
		{
			movementDirection.x += hAxis * deltaTime * accelerationSpeed;
			movementDirection.x = Mathf.Clamp(movementDirection.x, -movementSpeed, movementSpeed);
			isMoving = true;
		}
		else if (Mathf.Abs(movementDirection.x) > 0.05f)
		{
			movementDirection.x *= friction;
			isMoving = true;
		}
		else 
			movementDirection.x = 0f;
		
		if (Mathf.Abs(vAxis) > threshold) 
		{
			movementDirection.z += vAxis * deltaTime * accelerationSpeed;
			movementDirection.z = Mathf.Clamp(movementDirection.z, -movementSpeed, movementSpeed);
			isMoving = true;
		}
		else if (Mathf.Abs(movementDirection.z) > 0.05f)
		{
			movementDirection.z *= friction;
			isMoving = true;
		}
		else
		{
			movementDirection.z = 0f;
		}
		UpdateStepSound(isMoving);
	}
	
	void ProcessMovement() 
	{
		if (movementDirection.sqrMagnitude > movementSpeed * movementSpeed) {
			movementDirection.Normalize();
			movementDirection *= movementSpeed;
		}
		
		if (paralized)
			movementDirection *= 0.8f;
		
		controller.Move(movementDirection);
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) 
	{
		GameObject collider = hit.gameObject;
		
		if (collider.CompareTag("RidingHood")) 
		{
			//AudioSource.PlayClipAtPoint(munchSound.clip, transform.position);
			munchSound.Play();
			stateManager.MetObject(HealthController.FoodType.RedHood);
			collider.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
		}
		else
		if (collider.CompareTag("Hunter")) {
			if (stateManager.MetObject(HealthController.FoodType.Hunter)) {
				//AudioSource.PlayClipAtPoint(munchSound.clip, transform.position);
				munchSound.Play();
				collider.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
			}
			else {
				//SendMessage("OnCollisionWithProjectile", SendMessageOptions.DontRequireReceiver);
			}
		}
		else
		if (collider.CompareTag("Granny")) {
			//AudioSource.PlayClipAtPoint(munchSound.clip, transform.position);
			munchSound.Play();
			stateManager.MetObject(HealthController.FoodType.Granny);
			hit.gameObject.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
		}
		//else
		//if (collider.CompareTag("Projectile")) {
		//	print ("OMG! I'm hit!");	
		//}
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

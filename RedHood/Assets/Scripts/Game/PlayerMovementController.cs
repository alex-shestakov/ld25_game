using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {
	
	public float maxMovementSpeed = 1f;
	public float accelerationSpeed = 1f;
	public float deccelerationSpeed = 1f;
	public float friction = 0.6f;
		
	public float bonusSpeedMultiplier = 1.6f;
	
	private float initialYValue;
	private CharacterController controller;
	private Vector3 movementSpeed = Vector3.zero;
	
	private PlayerStateManager stateManager;
	private bool paralized = true;
	
	public AudioSource munchSound;
	public AudioSource  stepSound;
	
	private float paralizeRatio = 0.3f;
	private float threshold = 0.3f;
	
	private float maxMovementSpeedSqr = 100.0f;
	
	void Awake() {
		stateManager = GetComponent<PlayerStateManager>();
		maxMovementSpeed *= paralizeRatio;
		accelerationSpeed *= paralizeRatio;
		maxMovementSpeedSqr = maxMovementSpeed * maxMovementSpeed;
	}
	
	void Start() {
		controller = GetComponent<CharacterController>();
		initialYValue = transform.position.y;
		//movementSpeed = Vector3.zero;
		
		//paralized = true;
		//munchSound = GetComponent<AudioSource>();
	}
	
	void Update() {
		HandleInput();
		ProcessMovement();
	}
	
	private void UpdateStepSound(bool play)
	{
		if (play && ! stepSound.isPlaying)
		{
			stepSound.Play();
		}
		
		if (! play && stepSound.isPlaying)
		{
			stepSound.Stop();
		}
	}
	
	void HandleInput() 
	{
		float deltaTime = Time.deltaTime;
		
		float hAxis = Input.GetAxis("Horizontal");
		float vAxis = Input.GetAxis("Vertical");
		
		if (Mathf.Abs(hAxis) > threshold) 
		{
			movementSpeed.x += hAxis * deltaTime * accelerationSpeed;
			movementSpeed.x = Mathf.Clamp(movementSpeed.x, -maxMovementSpeed, maxMovementSpeed);
		}
		else if (Mathf.Abs(movementSpeed.x) > 0.05f)
		{
			if (movementSpeed.x < 0)
				movementSpeed.x = Mathf.Min(0f, movementSpeed.x + deltaTime * deccelerationSpeed);
			else
				movementSpeed.x = Mathf.Max (0f, movementSpeed.x - deltaTime * deccelerationSpeed);
		}
		else 
			movementSpeed.x = 0f;
		
		if (Mathf.Abs(vAxis) > threshold) 
		{
			movementSpeed.z += vAxis * deltaTime * accelerationSpeed;
			movementSpeed.z = Mathf.Clamp(movementSpeed.z, -maxMovementSpeed, maxMovementSpeed);
		}
		else if (Mathf.Abs(movementSpeed.z) > 0.05f)
		{
			if (movementSpeed.z < 0)
				movementSpeed.z = Mathf.Min(0f, movementSpeed.z + deltaTime * deccelerationSpeed);
			else
				movementSpeed.z = Mathf.Max (0f, movementSpeed.z - deltaTime * deccelerationSpeed);
		}
		else
		{
			movementSpeed.z = 0f;
		}
		
		if (paralized)
		{
			UpdateStepSound((movementSpeed.x == 0) && (movementSpeed.z == 0));
		}
	}
	
	void ProcessMovement() 
	{
		//if (movementSpeed.sqrMagnitude > maxMovementSpeedSqr) 
		//{
		//	movementSpeed.Normalize();
		//	movementSpeed *= maxMovementSpeed;
		//}
		
		//movementSpeed *= (paralized ? paralizeRatio : 1.0f);
		
		//print (movementSpeed);
		controller.Move(movementSpeed * Time.deltaTime);
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
		maxMovementSpeed *= bonusSpeedMultiplier;
	}
	
	void BonusDeactivated() {
		maxMovementSpeed /= bonusSpeedMultiplier;
	}
	
	void OnCountdownFinished () {
		paralized = false;	
		maxMovementSpeed /= paralizeRatio;
		accelerationSpeed /= paralizeRatio;
		
		if (stepSound.isPlaying)
		{
			stepSound.Stop();
		}
	}
}

using UnityEngine;
using System.Collections;

public class RidingHoodWanderingBehaviour : MonoBehaviour {
	
	public Rigidbody body;
	public float pushesInterval = 1f;
	public float pushForce = 1f;
	public float maxSpeed = 3f;
	
	private float pushTimer = 0f;
	
	void Start () {
		
	}
	
	void Update () {
		pushTimer -= Time.deltaTime;
		if (pushTimer < 0f) {
			Vector3 pushDirection = Random.insideUnitSphere;
			pushDirection.y = 0f;
			body.AddForce(pushForce * pushDirection, ForceMode.Force);
			pushTimer = pushesInterval * Random.Range(0.8f, 1.2f);
		}
		
		if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed) {
			Vector3 currentVelocity = body.velocity;
			currentVelocity.Normalize();
			body.velocity = currentVelocity * maxSpeed;
		}
	}
}

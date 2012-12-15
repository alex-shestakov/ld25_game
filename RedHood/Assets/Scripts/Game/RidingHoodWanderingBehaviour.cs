using UnityEngine;
using System.Collections;

public class RidingHoodWanderingBehaviour : MonoBehaviour {
	
	public Rigidbody body;
	public float pushesInterval = 1f;
	public float pushForce = 1f;
	
	private float pushTimer = 0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pushTimer -= Time.deltaTime;
		if (pushTimer < 0f) {
			Vector3 pushDirection = Random.insideUnitSphere;
			pushDirection.y = 0f;
			body.AddForce(pushForce * pushDirection);
			pushTimer = pushesInterval * Random.Range(0.8f, 1.2f);
		}
	}
}

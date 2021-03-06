using UnityEngine;
using System.Collections;

public class HunterAttackingBehaviour : MonoBehaviour {
	
	public float followingForce = 3f;
	public float reloadTime = 1f;
	public float projectileLaunchForce = 500f;
	public float suspension = 20f;
	
	public Rigidbody projectilePrefab;
	
	private Transform targetTransform;
	private Transform parentTransform;
	private Rigidbody parentBody;
	private float reloadTimer;
	private AudioSource shotSound;
	private bool targetWithinRange;
	
	void Start () {
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
		parentTransform = transform.parent.transform;
		parentBody = transform.parent.rigidbody;
		shotSound = parentTransform.GetComponent<AudioSource>();
		reloadTimer = Random.value * reloadTime;
	}
	
	void FixedUpdate () 
	{
		
		if (targetTransform == null)
			return;
		
		reloadTimer += Time.deltaTime;
		
		Vector3 followDirection = targetTransform.position - parentTransform.position;
		followDirection.Normalize();
		
		if (targetWithinRange && reloadTimer > reloadTime) {
			Vector3 launchStartingPosition = transform.position + 4 * followDirection;
			Rigidbody projectile = Instantiate(projectilePrefab, launchStartingPosition, Quaternion.identity) as Rigidbody;
			projectile.AddForce(followDirection * projectileLaunchForce, ForceMode.Impulse);
			parentBody.AddForce(followDirection * -suspension, ForceMode.Impulse);
			reloadTimer = 0f;	
			//AudioSource.PlayClipAtPoint(shotSound.clip, transform.position);
			shotSound.Play();
		}
		else
		{
			parentBody.AddForce(followDirection * followingForce);
		}
	}
	
	void OnTriggerEnter(Collider other) {
     	if (other.CompareTag("Player")) {			
			targetWithinRange = true;
		}
    }
	
	void OnTriggerStay(Collider other) {
		if (other.CompareTag("Player")) {
			targetWithinRange = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			targetWithinRange = false;
		}
	}
}

using UnityEngine;
using System.Collections;

public class PlaneToMovementDirectionRotator : MonoBehaviour {
	
	public Transform plane;
	
	public float directionCheckInterval = 0.1f;
	private float directionCheckTimer;
	
	private float previousX;
	private float previousDirection;
	
	void Start () {
		previousX = transform.position.x;
		previousDirection = 1f;
	}
	
	void Update () {
		
		directionCheckTimer += Time.deltaTime;
		if (directionCheckTimer > directionCheckInterval) {
			directionCheckTimer = 0f;
			float desiredDirection;
			if (transform.position.x > previousX) 
				desiredDirection = 1f;
			else 
				desiredDirection = -1f;
			
			if (desiredDirection != previousDirection) {
				Vector3 newScale = transform.localScale;
				transform.localScale = new Vector3(desiredDirection *  Mathf.Abs(newScale.x), newScale.y, newScale.z);
			}
			
			previousDirection = desiredDirection;
			previousX = transform.position.x;	
		}
	}
}

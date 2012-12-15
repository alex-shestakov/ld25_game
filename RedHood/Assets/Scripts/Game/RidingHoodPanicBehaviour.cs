using UnityEngine;
using System.Collections;

public class RidingHoodPanicBehaviour : MonoBehaviour {
	
	public float fleeForce = 10f;
	
	private Transform targetTransform;
	private Transform parentTransform;
	private Rigidbody parentBody;
	
	void Start () {
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
		parentTransform = transform.parent.transform;
		parentBody = transform.parent.rigidbody;
	}
	
	void Update () {
		if (targetTransform == null)
			return;
		
		Vector3 fleeDirection = parentTransform.position - targetTransform.position;
		fleeDirection.Normalize();
		parentBody.AddForce(fleeDirection * fleeForce);
	}
}

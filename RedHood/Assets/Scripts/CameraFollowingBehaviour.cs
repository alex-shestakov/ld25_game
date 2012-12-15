using UnityEngine;
using System.Collections;

public class CameraFollowingBehaviour : MonoBehaviour {

	public Transform target;
	public float smoothTime = 1f;
	public Vector3 initialOffset = Vector3.zero;
	
	void Start() {
		transform.position = target.position + initialOffset;
	}
	
	void Update() {
		Vector3 moveCameraTo = target.position + initialOffset;
		iTween.MoveUpdate(gameObject, moveCameraTo, smoothTime);	
	}
}

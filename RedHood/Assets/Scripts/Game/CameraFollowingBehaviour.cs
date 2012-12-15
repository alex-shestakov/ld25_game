using UnityEngine;
using System.Collections;

public class CameraFollowingBehaviour : MonoBehaviour {

	public Transform target;
	public float smoothTime = 1f;
	public Vector3 initialOffset = Vector3.zero;
	public Vector3 bonusOffset = Vector3.zero;
	
	private bool bonusActive = false;
	
	void Start() {
		transform.position = target.position + initialOffset;
	}
	
	void Update() {
		Vector3 moveCameraTo;
		if (bonusActive == false)
			moveCameraTo = target.position + initialOffset;
		else
			moveCameraTo = target.position + bonusOffset;
		
		iTween.MoveUpdate(gameObject, moveCameraTo, smoothTime);	
	}
	
	void BonusActivated() {
		bonusActive = true;
	}
	
	void BonusDeactivated() {
		bonusActive = false;	
	}
}

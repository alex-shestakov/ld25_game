using UnityEngine;
using System.Collections;

public class ProjectileCollisionHandler : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player")) {
			other.gameObject.SendMessage("OnCollisionWithProjectile", SendMessageOptions.DontRequireReceiver);
		}
	}
}

using UnityEngine;
using System.Collections;

public class ProjectileCollisionHandler : MonoBehaviour {
	void OnCollisionEnter(Collision collision) {
        collider.gameObject.SendMessage("OnCollisionWithProjectile", SendMessageOptions.DontRequireReceiver);
	}
}

using UnityEngine;
using System.Collections;

public class RidingHoodAI : MonoBehaviour {
	
	private RidingHoodPanicBehaviour panic;
	private RidingHoodWanderingBehaviour wandering;
	
	void Start () {
		panic = GetComponent<RidingHoodPanicBehaviour>();
		wandering = GetComponent<RidingHoodWanderingBehaviour>();
	}	
	
	void OnTriggerEnter(Collider other) {
     	if (other.CompareTag("Player")) {
			wandering.enabled = false;
			panic.enabled = true;
		}
    }
	
	void OnTriggerStay(Collider other) {
		if (other.CompareTag("Player") && wandering.enabled == true) {
			wandering.enabled = false;
			panic.enabled = true;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			wandering.enabled = true;
			panic.enabled = false;
		}
	}
}

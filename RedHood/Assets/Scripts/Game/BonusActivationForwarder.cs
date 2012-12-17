using UnityEngine;
using System.Collections;

public class BonusActivationForwarder : MonoBehaviour {
	
	public string tagOfTargetObjects;
	
	void BonusActivated() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag(tagOfTargetObjects);	
		foreach (GameObject target in targets) {
			if (target.active)
				target.BroadcastMessage("BonusActivated", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	void BonusDeactivated() {
		GameObject[] targets = GameObject.FindGameObjectsWithTag(tagOfTargetObjects);	
		foreach (GameObject target in targets) {
			if (target.active)
				target.BroadcastMessage("BonusDeactivated", SendMessageOptions.DontRequireReceiver);
		}
	}
}

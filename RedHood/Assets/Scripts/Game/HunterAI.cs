using UnityEngine;
using System.Collections;

public class HunterAI : MonoBehaviour {
	
	HunterAttackingBehaviour attacker;
	RidingHoodPanicBehaviour panic;
	
	void Awake() {
		attacker = GetComponent<HunterAttackingBehaviour>();
		panic = GetComponent<RidingHoodPanicBehaviour>();
	}
	
	// God mode on
	public void BonusActivated() { 		
		attacker.enabled = false;
		panic.enabled = true;
	}
	
	// God mode off
	public void BonusDeactivated() {
		attacker.enabled = true;
		panic.enabled = false;
	}
}

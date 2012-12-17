using UnityEngine;
using System.Collections;

public class GodModeBonusForwarder : MonoBehaviour {
	
	private PlayerStateManager playerStateManager;
	
	// Use this for initialization
	void Awake() 
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		playerStateManager = playerObj.GetComponent<PlayerStateManager>();
	}
	
	void BonusActivated() {
	}
	
	void BonusDeactivated() {
		playerStateManager.OnGodModeBonusDeactivated();
	}
}

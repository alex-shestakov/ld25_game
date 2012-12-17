using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject[] objectsToInform;
	public string loadingFinishedMessage = "OnLoadingFinished";
	
	public float loadingTime = 3f;
	
	private float startTimer = 0f;
	private GameObject mobs;
	private bool loaded;
	
	void Start () {
		//mobs = GameObject.Find("Mobs");
		//mobs.SetActiveRecursively(false);
	}
	
	void Update () {
		startTimer += Time.deltaTime;
		if (startTimer > loadingTime && !loaded) {
			foreach (GameObject target in objectsToInform) {
				loaded = true;
				target.BroadcastMessage(loadingFinishedMessage, SendMessageOptions.DontRequireReceiver);
			}
		}
	}
	
	void OnCountdownFinished () {
		//mobs.SetActiveRecursively(true);
		foreach (GameObject target in objectsToInform) {
			target.BroadcastMessage("OnCountdownFinished", SendMessageOptions.DontRequireReceiver);
		}
	}
}

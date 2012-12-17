using UnityEngine;
using System.Collections;

public class ReadySteadyDisplayer : MonoBehaviour {
	
	public string[] phrasesToShow;
	public float phrasesInterval;
	public float finalInterval;
	public GameObject targetToInformAboutReadiness;
	public string messageForTarget = "OnCountdownFinished";
	
	private float phrasesTimer = 0f;
	private int currentPhraseIndex = 0;
	private UITKText label;
	private bool loadingFinished;
	
	void Start () {
		gameObject.SetActiveRecursively(true);
		label = GetComponent<UITKText>();
	}
	
	void Update () {
		if (!loadingFinished)
			return;
		
		phrasesTimer += Time.deltaTime;
		if (phrasesTimer > phrasesInterval) {
			if (currentPhraseIndex < phrasesToShow.Length) {
				label.setText(phrasesToShow[currentPhraseIndex]);
				currentPhraseIndex++;
				print ("Phrase index = " + currentPhraseIndex);
				phrasesTimer = 0f;
				
				if (currentPhraseIndex == phrasesToShow.Length) {
					phrasesInterval = finalInterval;	
				}
			}
			else
			{
				targetToInformAboutReadiness.BroadcastMessage(messageForTarget, SendMessageOptions.DontRequireReceiver);
				label.setText("");
				phrasesTimer = 0f;
				enabled = false;
				gameObject.SetActiveRecursively(false);
			}
		}
	}
	
	void OnLoadingFinished () {
		loadingFinished = true;
	}
}

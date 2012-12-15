using UnityEngine;
using System.Collections;

public class BonusActivator : MonoBehaviour {
	
	private UITKProgressBar progress;
	public GameObject      bonusListener;
	private bool isBonusActive = false;

	void Awake()
	{
		progress = GetComponent<UITKProgressBar>();
	}
	
	public void onBonusCollected() 
	{
		isBonusActive = true;
		progress.resetTimer();
		if (bonusListener != null)
		{
			bonusListener.SendMessage("BonusActivated", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	private void OnProgressTimerExpired() 
	{
		if (bonusListener != null)
		{
			bonusListener.SendMessage("BonusDeactivated", SendMessageOptions.DontRequireReceiver);
		}
		isBonusActive = false;
	}
	
	public bool getIsActive()
	{
		return isBonusActive;
	}
}

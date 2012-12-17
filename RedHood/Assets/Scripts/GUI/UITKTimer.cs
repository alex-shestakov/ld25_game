using UnityEngine;
using System.Collections;

public class UITKTimer : MonoBehaviour {
	
	protected UIToolkitCommonAssets toolkitAssets;
	protected UIText toolkitText;
	
	private UITextInstance labelTimer;
	
	private float levelStart = 0F;
	private float timePassed = 0F;
	
	private bool countdownFinished;
	
	// Use this for initialization
	void Awake () {
		toolkitAssets  = GameObject.FindGameObjectWithTag("GUIToolkit").GetComponent(
						typeof(UIToolkitCommonAssets)) as UIToolkitCommonAssets;
		
	}
	
	void Start()
	{
		toolkitText = toolkitAssets.getLabelText();	
		
		labelTimer = toolkitText.addTextInstance("0:00.0", 0, 0, 1f, 15);
		labelTimer.positionFromTopLeft(0.03f, 0.5f);
		
		//remove this if you will send restart level message
		// onRestartLevel();
	}
	
	private IEnumerator updateTimer() 
	{
		while (true)
		{	
			yield return new WaitForSeconds(0.1f);
			if (labelTimer != null)
			{
				timePassed     = Time.time - levelStart;
				float minutes  = Mathf.FloorToInt(timePassed / 60.0F);
				float seconds  = timePassed - minutes * 60.0F; 
				labelTimer.text = string.Format("{0:00}:{1:00.0}", minutes, seconds);	
			}
		}
	}
	
	public void onRestartLevel()
	{
		if (labelTimer != null)
		{
			labelTimer.text = string.Format("{0:00}:{1:00.0}", 0, 0);
			//enabled = false;
		}
	}
	
	public void resetTime()
	{
		levelStart = Time.time;
	}
	
	public float getCountedTime()
	{
		return timePassed;
	}
	
	void OnDisable() 
	{
		StopCoroutine("updateTimer");
    }
	
	void OnEnable() 
	{
		StartCoroutine("updateTimer");
    }
	
	void OnLoadingFinished() 
	{
		//OnEnable();	
	}
	
	void LateUpdate() {
		if (!countdownFinished)
			resetTime();
	}
	
	void OnCountdownFinished() {
		countdownFinished = true;
		onRestartLevel();
	}
}

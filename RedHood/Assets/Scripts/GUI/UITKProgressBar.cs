using UnityEngine;
using System.Collections;

public class UITKProgressBar : UITKControl {

	private UIProgressBar progressBar;
	private UISprite      progressBack;
	private UITKText      label;
	public float  fullDecreaseTime = 15.0f;
	private float updatePeriodTime;
	public float  updateDeltaValue = 0.01f;
	public bool rightToLeft = false;
	public float initialValue = 1.0f;
	
	public string backImageName;
	
	// Use this for initialization
	protected override void initButton()
	{
		progressBar = UIProgressBar.create(toolkitManager, buttonNormalState, 0, 0, rightToLeft, 30);
		progressBar.resizeTextureOnChange = true;
		
		mainControl = progressBar;
		
		label = GetComponent<UITKText>() as UITKText;
		
		if (backImageName != null && ! backImageName.Equals(""))
		{
			progressBack = toolkitManager.addSprite(backImageName, 0, 0, 40);
		}
	}
	
	protected override void initButtonPosition()
	{
		progressBar.positionFromTopLeft(posFromTop, posFromLeft);
		progressBar.value = initialValue;
		resetLabel();
		startAutoTimer();
		
		if (progressBack != null)
		{
			progressBack.positionFromTopLeft(posFromTop, posFromLeft);
		}
	}
	
	protected override bool getMainControlDisabled()
	{
		return false;
	}
	protected override void setMainControlDisabled(bool disabled)
	{
	}
	
	public float getValue()
	{
		return progressBar.value;
	}
	
	public float setValue(float newValue)
	{
		return progressBar.value = newValue;
		resetLabel();
	}
	
	private IEnumerator autoDecrease()
	{
		while (true)
		{
			if (enabled && progressBar.value > 0)
			{
				progressBar.value = progressBar.value - updateDeltaValue;
				if (progressBar.value <= 0)
				{
					progressBar.value = 0;
					gameObject.SendMessage("OnProgressTimerExpired", SendMessageOptions.DontRequireReceiver);
				}
				resetLabel();
			}
			
			yield return new WaitForSeconds(updatePeriodTime);	
		}
	}
	
	private void startAutoTimer()
	{
		updatePeriodTime = fullDecreaseTime * updateDeltaValue;
		StartCoroutine("autoDecrease");
	}
	
	public void resetTimer()
	{
		progressBar.value = 1.0f;
		resetLabel();
	}
	
	public void addValueToTimer(float addValue)
	{
		progressBar.value = Mathf.Max(Mathf.Min(progressBar.value + addValue, 1.0f), 0f);
		resetLabel();
	}
	
	private void resetLabel()
	{
		if (label != null)
		{
			label.setHidden(progressBar.value <= 0);
		}
	}
}

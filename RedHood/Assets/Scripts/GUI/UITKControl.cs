using UnityEngine;
using System.Collections;

public class UITKControl : MonoBehaviour {

	public float posFromTop;
	public float posFromLeft;
	
	public string buttonNormalState;
	public string buttonPressedState;
	public string buttonDisabledState;
	
	protected UIButton gameButton;
	protected UISprite mainControl;
	protected UIToolkitCommonAssets toolkitAssets;
	protected UIToolkit toolkitManager;
	
	protected bool isInputDenied = false;
	void Start() 
	{		
		toolkitAssets  = GameObject.FindGameObjectWithTag("GUIToolkit").GetComponent(
						typeof(UIToolkitCommonAssets)) as UIToolkitCommonAssets;
		toolkitManager = toolkitAssets.getToolkitManagerScreenButtons();
		
		onStart();
		
		if (buttonNormalState.Equals(""))
		{
			Debug.Log("No frame name is set for UIToolkit button/sprite");
		}
		else
		{
			if (buttonPressedState.Equals(""))
			{
				buttonPressedState = buttonNormalState;
			}
			if (buttonDisabledState.Equals(""))
			{
				buttonDisabledState = buttonNormalState;
			}
			
			createButton();
			LockStateChanged();
		}
		
	}
	
	protected virtual void onStart()
	{
	}
	
	protected void createButton()
	{
		initButton();
		initButtonPosition();
	}
	
	protected virtual void initButton()
	{
		gameButton = UIButton.create(toolkitManager,
					   			     buttonNormalState,
           						     buttonPressedState,
									 0, 
									 0,
									 35);
		gameButton.onTouchUpInside += onClick;
		
		mainControl = gameButton;
	}
	
	protected virtual void initButtonPosition()
	{
		gameButton.positionFromTopLeft(0f, 0f);
	}
	
	private void OnEnable() 
	{
		LockStateChanged();
	}
	
	private void OnDisable() 
	{
		LockStateChanged();
	}
	
	protected virtual bool getMainControlDisabled()
	{
		return gameButton.disabled;
	}
	protected virtual void setMainControlDisabled(bool disabled)
	{
		gameButton.disabled = disabled;
	}
	
	protected virtual void updateControlImage()
	{
		mainControl.setSpriteImage(getMainControlDisabled() ? buttonDisabledState: buttonNormalState);
	}
	
	protected virtual void onButtonEnabled()
	{
	}
	
	protected virtual void onButtonDisabled()
	{
	}
	
	protected virtual void showButton(bool show)
	{
		mainControl.hidden = ! show;
		//gameButton.refreshPosition();
	}
	
	protected virtual bool showOnScreen()
	{
		return enabled;
	}
	
	protected virtual void onClick( UIButton sender )
    {	
		print ("onclick base Toolkit menu item");
	}
	
	protected void LockStateChanged() 
	{
		//if (buttonNormalState != null && ! buttonNormalState.Equals(""))
		//{
		//	print(gameButton + " "+ buttonNormalState + " LockStateChanged");
		//}
		if (mainControl != null)
		{
			if (showOnScreen())
			{
				if (mainControl.hidden)
				{
					showButton(true);
				}
			
				if (isInputDenied != getMainControlDisabled())
    			{     		
					setMainControlDisabled(isInputDenied);
					updateControlImage();
				}
				
				if (getMainControlDisabled())
				{
					onButtonDisabled();
				}
				else
				{
					onButtonEnabled();
				}
				
				mainControl.refreshPosition();
				mainControl.updateTransform();
			}
			else if (! mainControl.hidden)
			{
				showButton(false);
			}
		}
	}
}

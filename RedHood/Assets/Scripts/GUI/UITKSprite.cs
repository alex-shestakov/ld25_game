using UnityEngine;
using System.Collections;

public class UITKSprite : UITKControl {

	protected override void initButton()
	{
		mainControl = toolkitManager.addSprite(buttonNormalState, 0, 0, 35);
	}
	
	protected override void initButtonPosition()
	{
		mainControl.positionFromTopLeft(posFromTop, posFromLeft);
	}
	
	protected override bool getMainControlDisabled()
	{
		return false;
		//return mainControl.hidden;
	}
	protected override void setMainControlDisabled(bool disabled)
	{
		//mainControl.hidden = disabled;
	}
}

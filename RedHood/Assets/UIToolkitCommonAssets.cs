using UnityEngine;
using System.Collections;

public class UIToolkitCommonAssets : MonoBehaviour 
{
	public string texPackerConfigScreenButtons = "level_gui";
	public string texPackerConfigLabels        = "level_text";
	
	public UIToolkit toolKitManagerLabels;
	public UIToolkit toolkitManagerScreenButtons;

	private UIText labelText;

	public UIText getLabelText() 
	{
		if (labelText == null)
		{
			string fontTxtFile = "GameLabelFnt";
			labelText = new UIText(getToolkitManagerLabels(), fontTxtFile, "GameLabelFnt.png");
		}
		return labelText;
	}
	
	
	public UIToolkit getToolkitManagerLabels() 
	{
		if (toolKitManagerLabels.guiTexture == null)
		{
			toolKitManagerLabels.texturePackerConfigName = texPackerConfigLabels;
			toolKitManagerLabels.loadTextureAndPrepareForUse();
		}
		return toolKitManagerLabels;
	}
	
	public UIToolkit getToolkitManagerScreenButtons() 
	{
		if (toolkitManagerScreenButtons.guiTexture == null)
		{
			toolkitManagerScreenButtons.texturePackerConfigName = texPackerConfigScreenButtons;
			toolkitManagerScreenButtons.loadTextureAndPrepareForUse();
		}
		return toolkitManagerScreenButtons;
	}
}
using UnityEngine;
using System.Collections;

public class UITKText : MonoBehaviour {

	protected UIToolkitCommonAssets toolkitAssets;
	protected UIText toolkitText;
	
	private UITextInstance label;
	
	public float posFromTop;
	public float posFromLeft;
	public float fontScale = 1.0f;
	public bool  initiallyHidden = false;
	
	public string initialText = "";
	
	// Use this for initialization
	void Awake () {
		toolkitAssets  = GameObject.FindGameObjectWithTag("GUIToolkit").GetComponent(
						typeof(UIToolkitCommonAssets)) as UIToolkitCommonAssets;
		
	}
	
	void Start()
	{
		toolkitText = toolkitAssets.getLabelText();	
		
		label = toolkitText.addTextInstance(initialText, 0, 0, fontScale, 15);
		label.positionFromTopLeft(posFromTop, posFromLeft);
		if (initiallyHidden)
		{
			label.hidden = true;
		}
	}
	
	public void setText(string newText)
	{
		label.text = newText;
	}
	
	public void setHidden(bool isHidden)
	{
		if (label != null)
		{
			label.hidden = isHidden;
		}
	}
}

using UnityEngine;
using System.Collections;

public class UITKText : MonoBehaviour {

	protected UIToolkitCommonAssets toolkitAssets;
	protected UIText toolkitText;
	
	private UITextInstance label;
	
	public float posFromTop;
	public float posFromLeft;
	
	public string initialText = "";
	
	// Use this for initialization
	void Awake () {
		toolkitAssets  = GameObject.FindGameObjectWithTag("GUIToolkit").GetComponent(
						typeof(UIToolkitCommonAssets)) as UIToolkitCommonAssets;
		
	}
	
	void Start()
	{
		toolkitText = toolkitAssets.getLabelText();	
		
		label = toolkitText.addTextInstance(initialText, 0, 0, 1f, 15);
		label.positionFromTopLeft(posFromTop, posFromLeft);
	}
	
	public void setText(string newText)
	{
		label.text = newText;
	}
}

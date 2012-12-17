using UnityEngine;
using System.Collections;

public class WolfLookController : MonoBehaviour {
	
	private Material  lookMat;
	private Texture   baseTexture;
	public  Texture   godModeTexture;
	
	private bool      isGodMode = false;
	
	// Use this for initialization
	void Awake () 
	{
		lookMat = (GetComponent<Renderer>() as MeshRenderer).material;
		baseTexture = lookMat.mainTexture;
	}
	
	public void setGodMode(bool godModeValue)
	{
		isGodMode = godModeValue;
		updateTexture();
	}
	
	private void updateTexture()
	{
		lookMat.mainTexture = isGodMode ? godModeTexture : baseTexture;
	}
}

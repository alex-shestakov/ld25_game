using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {
	
	public enum FoodType
	{
    	Rabbit = 0,
		RedHood,
		Granny, 
		Hunter
	}
	
	public float redHoodNutritionalValue = 0.2f;
	public float grannyNutritionalValue  = 0.3f;
	public float hunterNutritionalValue  = 0.3f;
	public float rabbitNutritionalValue  = 0.05f;
	private UITKProgressBar progress;
	
	private bool startedDecreasing;
	
	private GameObject playerObj;
	private UITKTimer timer;
	
	//GameController;

	// Use this for initialization
	void Awake() 
	{
		progress  = GetComponent<UITKProgressBar>();
		playerObj = GameObject.FindGameObjectWithTag("Player");
		timer = GameObject.FindObjectOfType(typeof(UITKTimer)) as UITKTimer;
	}
	
	void LateUpdate() {
		if (!startedDecreasing)
			progress.setValue(1f);
	}
	
	public void OnEatenFood(FoodType food)
	{
		switch(food)
		{
			case FoodType.Rabbit:
				progress.addValueToTimer(rabbitNutritionalValue);
				break;
			case FoodType.RedHood:
				progress.addValueToTimer(redHoodNutritionalValue);
				break;
			case FoodType.Granny:
				progress.addValueToTimer(grannyNutritionalValue);
				break;
			case FoodType.Hunter:
				progress.addValueToTimer(hunterNutritionalValue);
				break;
			default:
				break;
		}
	}
	
	private void OnProgressTimerExpired() 
	{
		print ("progress timer expired!");
		playerObj.SendMessage("OnKilled", SendMessageOptions.DontRequireReceiver);
		timer.StopAllCoroutines();
	}
	
	public void OnProjectile()
	{
		//print ("HealthController: OnProjectile called, current progress is " + progress.getValue());
		progress.addValueToTimer(-0.33f);
		//print ("HealthController: OnProjectile called, the progress is " + progress.getValue() + " now");
		if (progress.getValue() <= 0f)
		{
			OnProgressTimerExpired();
		}
	}
	
	void OnCountdownFinished() {
		startedDecreasing = true;
	}
}

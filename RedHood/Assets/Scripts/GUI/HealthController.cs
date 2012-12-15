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
	
	//GameController;

	// Use this for initialization
	void Awake() 
	{
		progress = GetComponent<UITKProgressBar>();
	
	}
	
	public void onEatenFood(FoodType food)
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
		//gameController.playerStarved();
	
	}
}

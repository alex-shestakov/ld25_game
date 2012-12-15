using UnityEngine;
using System.Collections;

public class PlayerStateManager : MonoBehaviour {

	public HealthController healthController;
	public BonusActivator capBonusActivator;
	public BonusActivator stickBonusActivator;
	public BonusActivator glassBonusActivator;
	public BonusActivator godModeBonusActivator;
	
	public UITKText godModeCounterText;
	
	//public PlayerMovementController player;
	
	public int godModeParm = 10;
	private int godModeCounter = 0;
	
	public enum BonusType
	{
    	None = 0,
		Glass,
		Stick,
    	Cap
	}
	
	//public enum FoodType
	//{
    //	Rabbit = 0,
	//	RedHood,
	//	Granny, 
	//	Hunter
	//}
	
	// Use this for initialization
	void Start () 
	{
		godModeCounterText.setText(godModeCounter+"/"+godModeParm);
	}
	
	public bool MetObject(HealthController.FoodType foodObj)
	{
		bool ret = false;
		switch (foodObj)
		{
			case HealthController.FoodType.Hunter:
				if (godModeBonusActivator.getIsActive())
				{
					healthController.OnEatenFood(foodObj);
					ret = true;
				}
				else
				{
					//gameController.playerKilled();
				}
				break;
			
			case HealthController.FoodType.RedHood:
				godModeCounter++;
			    godModeCounterText.setText(godModeCounter+"/"+godModeParm);
				giveBonus(false);
				healthController.OnEatenFood(foodObj);	
				ret = true;
				break;
			
			case HealthController.FoodType.Granny:	
				giveBonus(true);
				healthController.OnEatenFood(foodObj);	
				ret = true;
				break;
			
			default:
				healthController.OnEatenFood(foodObj);	
				ret = true;
				break;
		}
		return ret;
	}
	
	private BonusType intToType(int intValue)
	{
		switch(intValue)
		{
			case 1:
				return BonusType.Glass;
			case 2:
				return BonusType.Stick;
			case 3:
				return BonusType.Cap;
			default:
				return BonusType.None;
		}
	}
	
	private void giveBonus(bool isGranny)
	{
		if (isGranny)
		{
			BonusType bonus = intToType(Random.Range(1, 3));
			
			switch (bonus)
			{
				case BonusType.Cap:
					capBonusActivator.onBonusCollected();
					break;
				case BonusType.Glass:
					glassBonusActivator.onBonusCollected();
					break;
				case BonusType.Stick:
					stickBonusActivator.onBonusCollected();
					break;
				//case BonusType.Teeth:
				//capBonusActivator.onBonusCollected();
				//	break;
			}
		}
		else if (godModeCounter == godModeParm)
		{
			godModeCounter = 0;
			godModeCounterText.setText(godModeCounter+"/"+godModeParm);
			godModeBonusActivator.onBonusCollected();
			//godModeEnabled = true;
			//progressGodMode.resetTimer();
		}
	}
	
	public void OnProjectile()
	{
		healthController.OnProjectile();
	}
}

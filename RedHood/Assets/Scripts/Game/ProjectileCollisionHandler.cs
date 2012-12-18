using UnityEngine;
using System.Collections;

public class ProjectileCollisionHandler : MonoBehaviour 
{
	private AutoDestruction autoDestruction;
	
	void Awake()
	{
		Transform parent = transform.parent;
		autoDestruction = parent.GetComponent<AutoDestruction>();
	}
	
	
	void OnTriggerEnter(Collider other) 
	{
		GameObject otherObj = other.gameObject;
		if (otherObj.CompareTag("Player")) 
		{
			otherObj.SendMessage("OnCollisionWithProjectile", SendMessageOptions.DontRequireReceiver);
			autoDestruction.DestroySelf();
		}
	}
}

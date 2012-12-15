using UnityEngine;
using System.Collections;

public class AutoDestruction : MonoBehaviour {
	
	public float livingTime;
	public ObjectPool.PooledObjectType poolObjId;
	
	private float startTime;
	private ObjectPool objPool;

	void OnEnable() 
	{
		if (! objPool)
		{
			objPool = ObjectPool.GetInstance();
		}
		//particleSystem.Play();
		startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (gameObject.active && 
			(Time.time - startTime > livingTime))
		{
			//particleSystem.Stop();
			if (! objPool.PoolObject(poolObjId, gameObject))
			{
				GameObject.Destroy(gameObject);
			}
		}
	
	}
}

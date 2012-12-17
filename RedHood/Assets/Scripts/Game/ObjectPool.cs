using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	private static ObjectPool instance;
    /// <summary>
    /// The object prefabs which the pool can handle.
    /// </summary>
    public GameObject[] objectPrefabs;
    /// <summary>
    /// The pooled objects currently available.
    /// </summary>
    public List<GameObject>[] pooledObjects;
    /// <summary>
    /// The amount of objects of each type to buffer.
    /// </summary>
	
	public int[] amountToBuffer;
	
	public int defaultBufferAmount = 3;
	
	/// <summary>
	/// The container object that we will keep unused pooled objects
	/// so we dont clog up the editor with objects.
	/// </summary>
	protected GameObject containerObject;
	
	
	public enum PooledObjectType
	{
    	None = 0,
		RedHood,
		Blood,
		Projectile,
    	Granny,
    	Hunter,
    	Rabbit
	}
	
	public static ObjectPool GetInstance() 
	{
		if (! instance) 
		{
			instance = FindObjectOfType(typeof(ObjectPool)) as ObjectPool;
			if (! instance)
				Debug.LogError("There needs to be one active ObjectPool script on a GameObject in your scene.");
		}
		return instance;
	}

	// Use this for initialization
	void Start ()
	{
  		containerObject = new GameObject("ObjectPool");
  		//Loop through the object prefabs and make a new list for each one.
  		//We do this because the pool can only support prefabs set to it in the editor,
  		//so we can assume the lists of pooled objects are in the same
  		//order as object prefabs in the array
  		pooledObjects = new List<GameObject>[objectPrefabs.Length];
  		int i = 0;
  		foreach ( GameObject objectPrefab in objectPrefabs )
  		{
    		pooledObjects[i] = new List<GameObject>();
    		
			int bufferAmount = (i < amountToBuffer.Length) ? amountToBuffer[i] : defaultBufferAmount;
    		
    		for (int n = 0; n < bufferAmount; n++)
    		{
				GameObject newObj = Instantiate(objectPrefab) as GameObject;
				newObj.transform.position = Vector3.one * -1000f;
           		newObj.name = objectPrefab.name;
           		PoolObject(idxToPooledType(i), newObj);
			}
			i++;
		}
	}
	
	private int pooledTypeToIdx(PooledObjectType objectType)
	{
		int ret = -1;
		switch (objectType)
		{
			case PooledObjectType.RedHood:
				ret = 0;
				break;
			case PooledObjectType.Blood:
				ret = 1;
				break;
			case PooledObjectType.Granny:
				ret = 2;
				break;
			case PooledObjectType.Hunter:
				ret = 3;
				break;
			case PooledObjectType.Rabbit:
				ret = 4;
				break;
			default:
				break;
		}
		return ret;
	}
	
	private PooledObjectType idxToPooledType(int index)
	{
		return (PooledObjectType)(index + 1);
	}
	
	/// <summary>
    /// Gets a new object for the name type provided.  If no object type
    /// exists or if onlypooled is true and there is no objects of that
    /// type in the pool
    /// then null will be returned.
    /// </summary>
    /// <returns>
    /// The object for type.
    /// </returns>
    /// <param name='objectType'>
    /// Object type.
    /// </param>
    /// <param name='onlyPooled'>
    /// If true, it will only return an object if there is one currently
    /// pooled.
    /// </param>
    public GameObject GetObjectForType(PooledObjectType objectType, bool onlyPooled)
    {	
        int idx = pooledTypeToIdx(objectType);
		if (idx < 0 
		||  idx >= objectPrefabs.Length)
		{
			return null;
		}
		if (pooledObjects[idx].Count > 0)
        {
            GameObject pooledObject = pooledObjects[idx][0];
            pooledObjects[idx].RemoveAt(0);
            pooledObject.transform.parent = null;
            pooledObject.SetActiveRecursively(true);
            return pooledObject;
        }
		else if (! onlyPooled) 
		{
			GameObject prefab    = objectPrefabs[idx];
			GameObject newObject = Instantiate(prefab) as GameObject;
			newObject.name       = prefab.name;
			return newObject;
		}			
		
		//If we have gotten here either there was no object of the specified
		//type or none were left in the pool with onlyPooled set to true
		return null;
	}
	
	/// <summary>
	/// Pools the object specified.  Will not be pooled if there are no prefab of that type.
	/// </summary>
	/// <param name='obj'>
	/// Object to be pooled.
	/// </param>
	public bool PoolObject (PooledObjectType objectType, GameObject obj)
	{
 		int idx = pooledTypeToIdx(objectType);
		if (idx < 0 
		||  idx >= objectPrefabs.Length)
		{
			return false;
		}
		
		obj.SetActiveRecursively(false);
      	obj.transform.parent = containerObject.transform;
      	pooledObjects[idx].Add(obj);
      	return true;
	}
}

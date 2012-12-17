using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public GameObject player;
	public Transform huntersRoot;
	public Transform hoodsRoot;
	public Transform grannysRoot;
	
	public float girlsMinDeltaSpawnTime = 2f;
	public float grannysSpawningTime = 7f;
	public float huntersSpawningTime = 9f;
	
	private bool countdownFinished;
	private GameObject[] spawnPoints;
	private float girlsSpawningTimer;
	private float grannysSpawningTimer;
	private float hunterSpawningTimer;
	private ObjectPool pool;
	
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
		pool = ObjectPool.GetInstance();
	}
	
	void SpawnInitialPopulation() {
		foreach (GameObject spawnPointObj in spawnPoints) {
			GameObject hood = pool.GetObjectForType(ObjectPool.PooledObjectType.RedHood, true);
			hood.transform.parent = hoodsRoot;
			hood.transform.position = spawnPointObj.transform.position;
		}
	}
	
	void Update () {
		if (!countdownFinished) 
			return;
		
		if (!player)
			return;
		
		girlsSpawningTimer += Time.deltaTime;
		grannysSpawningTimer += Time.deltaTime;
		hunterSpawningTimer += Time.deltaTime;
		
		if (girlsSpawningTimer > girlsMinDeltaSpawnTime) {
			girlsSpawningTimer = 0f;
			Vector3 spawnPosition = getTheMostFarSpawnPosition();
			GameObject hood = pool.GetObjectForType(ObjectPool.PooledObjectType.RedHood, true);
			if (hood) {
				hood.transform.parent = hoodsRoot;
				hood.transform.position = spawnPosition;
			}
		}
		
		if (grannysSpawningTimer > grannysSpawningTime) {
			grannysSpawningTimer = 0f;
			Vector3 spawnPosition = getTheMostFarSpawnPosition();
			GameObject granny = pool.GetObjectForType(ObjectPool.PooledObjectType.Granny, true);
			if (granny) {
				granny.transform.parent = grannysRoot;
				granny.transform.position = spawnPosition;
			}
		}
		
		if (hunterSpawningTimer > huntersSpawningTime) {			
			hunterSpawningTimer = 0f;
			Vector3 spawnPosition = getTheMostFarSpawnPosition();
			GameObject hunter = pool.GetObjectForType(ObjectPool.PooledObjectType.Hunter, true);
			if (hunter) {
				hunter.transform.parent = huntersRoot;
				hunter.transform.position = spawnPosition;
			}
		}
		
	}
	
	Vector3 getTheMostFarSpawnPosition() {
		float theMostFarDistance = float.MinValue;
		Vector3 theMostFarSpawnPosition = Vector3.zero;
		foreach (GameObject spawnPointObj in spawnPoints) {
			Vector3 vectorFromPlayerToPoint = player.transform.position - spawnPointObj.transform.position;
			float vectorLength = vectorFromPlayerToPoint.magnitude;
			if (vectorLength > theMostFarDistance) {
				theMostFarDistance = vectorLength;
				theMostFarSpawnPosition = spawnPointObj.transform.position;
			}
		}
		return theMostFarSpawnPosition;
	}
	
	void OnCountdownFinished() {
		countdownFinished = true;
		SpawnInitialPopulation();
	}
}

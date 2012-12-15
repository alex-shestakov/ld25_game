using UnityEngine;
using System.Collections;

public class EnemyDeathVisualization : MonoBehaviour {
	
	public ParticleSystem particlesPrefab;
	
	public void OnKilled() {
		ObjectPool pool = ObjectPool.GetInstance();
		ParticleSystem particle = pool.GetObjectForType(ObjectPool.PooledObjectType.Blood, false).GetComponent<ParticleSystem>();
		particle.transform.position = transform.position + new Vector3(0f, 7f, 0f);
		particle.Play();
		pool.PoolObject(ObjectPool.PooledObjectType.RedHood, gameObject);
	}
}

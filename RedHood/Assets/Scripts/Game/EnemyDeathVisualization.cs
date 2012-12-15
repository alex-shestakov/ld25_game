using UnityEngine;
using System.Collections;

public class EnemyDeathVisualization : MonoBehaviour {
	
	public ParticleSystem particlesPrefab;
	
	public void OnKilled() {
		GameObject.Instantiate(particlesPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

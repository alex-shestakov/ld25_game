using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour {
	
	public ParticleSystem particlesPrefab;
	
	public UITKText text;
	public PitchDownOnPlayerDeath cameraScript;
	
	public void OnKilled() {
		cameraScript.Explode();
		
		
		ObjectPool pool = ObjectPool.GetInstance();
		ParticleSystem particle = pool.GetObjectForType(ObjectPool.PooledObjectType.Blood, false).GetComponent<ParticleSystem>();
		particle.transform.position = transform.position + new Vector3(0f, 7f, 0f);
		//particle.startColor = Color.blue;
		particle.Play();
		
		Destroy(gameObject);
		
		
		text.setText("Oops... Try again!");
	}
}

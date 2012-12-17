using UnityEngine;
using System.Collections;

public class RidingHoodPanicBehaviour : MonoBehaviour {
	
	public float fleeForce = 10f;
	
	private Transform targetTransform;
	private Transform parentTransform;
	private Rigidbody parentBody;
	
	public AudioClip[]  screamClips;
	public AudioSource  screamSound;
	private int soundsNum;
	
	void Start () {
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
		parentTransform = transform.parent.transform;
		parentBody = transform.parent.rigidbody;
		
		if (screamClips != null)
		{
			soundsNum = screamClips.Length;
		}
	}
	
	void Update () {
		if (targetTransform == null)
			return;
		
		Vector3 fleeDirection = parentTransform.position - targetTransform.position;
		fleeDirection.Normalize();
		parentBody.AddForce(fleeDirection * fleeForce);
	}
	
	void OnEnable() 
	{
        if (soundsNum > 0)
		{ 
			int idx = (soundsNum > 1) ? Random.Range(0, soundsNum) : 0;
			screamSound.clip = screamClips[idx];
			screamSound.Play();
			//print ("scream sound " + screamSounds[idx].clip.name);
		}
    }
}

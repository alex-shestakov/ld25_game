using UnityEngine;
using System.Collections;

public class PitchDownOnPlayerDeath : MonoBehaviour {
	
	private GameObject player;
	private AudioSource music;
	private bool startedPitchDown;
	
	public AudioSource explosionSound;
	
	private float startPitchDownAfterPauseTimer;
	public float startPitchDownAfterPauseTime = 1f;
	

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		Component[] audioSources = GetComponents<AudioSource>();
		for (int i = 0; i < audioSources.Length; i++) {
			AudioSource audioSource = audioSources[i] as AudioSource;
			if (audioSource.clip.name == "IdrM - ludum darum - wolf")
				music = audioSource;
		}
	}
	
	void Update () {
		if (player)
			return;
	
		startPitchDownAfterPauseTimer += Time.deltaTime;
		if (startPitchDownAfterPauseTimer > startPitchDownAfterPauseTime)
			music.pitch *= 0.94f;
	}
	
	public void Explode()
	{
		explosionSound.Play();
	}
}

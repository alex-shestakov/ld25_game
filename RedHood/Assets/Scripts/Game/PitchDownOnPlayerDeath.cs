using UnityEngine;
using System.Collections;

public class PitchDownOnPlayerDeath : MonoBehaviour {
	
	private GameObject player;
	private AudioSource audio;
	private bool startedPitchDown;
	

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		audio = GetComponent<AudioSource>();
	}
	
	void Update () {
		if (player)
			return;

		audio.pitch *= 0.95f;
	}
}
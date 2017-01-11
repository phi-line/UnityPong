using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static SoundManager instance = null;

	public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    AudioSource audioS;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		audioS = this.GetComponent<AudioSource>();
	}

	public void PlayRand(AudioClip clip) {
		float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);
		audioS.pitch = randomPitch;
		audioS.clip = clip;
		audioS.Play();
	}
}

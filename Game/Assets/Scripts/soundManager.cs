using UnityEngine;
using System.Collections;

public class soundManager : MonoBehaviour {
    private AudioClip[] SceneSounds;
	// Use this for initialization
	void Start () {
        SceneSounds = new AudioClip[3];
        SceneSounds[0] = Resources.Load("foliage_crackle") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void playSound(int line)
    {
        audio.clip = SceneSounds[line - 1];

        audio.Play();
    }

    public bool PlayingSound()
    {
        bool isplayingAudio = audio.isPlaying;
        return isplayingAudio;
    }
}

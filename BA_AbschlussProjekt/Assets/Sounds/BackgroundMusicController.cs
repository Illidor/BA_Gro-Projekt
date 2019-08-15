using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    public static BackgroundMusicController instance = null;

    public List<AudioSource> backgroundClips = new List<AudioSource>();

    private int currentlyPlaying = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        
    }

    public void ChangeMusic(int indexToPlay)
    {
        backgroundClips[currentlyPlaying].Stop();

        backgroundClips[indexToPlay].Play();

        currentlyPlaying = indexToPlay;
    }
}

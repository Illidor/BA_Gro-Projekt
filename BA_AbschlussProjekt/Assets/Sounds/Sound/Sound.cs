using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Sound : MonoBehaviour
{
    //public string name;

    public List<AudioClip> clips;

    [Range(0f, 1f)]
    public float volume = .75f;
    [Range(0f, 1f)]
    public float volumeVariance = .1f;

    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = .1f;

    public bool loop = false;
    public bool playOnAwake = false;

    public AudioMixerGroup mixerGroup;

    
    private AudioSource source;
    private AudioSource sourceTwo;

    /// <summary>
    /// Playing the Sound File on the index. Put it to the Place where the Sound should be triggered
    /// </summary>
    public void PlaySound(int index)
    {
        if(source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        if(index < clips.Count)
        {
            source.clip = clips[index];
            source.outputAudioMixerGroup = mixerGroup;
            source.Play();
        }

        if(source != null)
        {
            StartCoroutine(DestroySoundComponent(source));
        }
    }

    public void PlaySound(int index, int audioSourceIndex) {

        if(audioSourceIndex == 1) {
            if (source == null) {
                source = gameObject.AddComponent<AudioSource>();
            }

            if (index < clips.Count) {
                source.clip = clips[index];
                source.outputAudioMixerGroup = mixerGroup;
                source.Play();
            }

            if (source != null) {
                StartCoroutine(DestroySoundComponent(source));
            }
        }
        else if(audioSourceIndex == 2) {
            if (sourceTwo == null) {
                sourceTwo = gameObject.AddComponent<AudioSource>();
            }

            if (index < clips.Count) {
                sourceTwo.clip = clips[index];
                sourceTwo.outputAudioMixerGroup = mixerGroup;
                sourceTwo.Play();
            }

            if (sourceTwo != null) {
                StartCoroutine(DestroySoundComponent(sourceTwo));
            }
        }
    }

    private IEnumerator DestroySoundComponent(AudioSource source)
    {
        //Debug.Log("Playing Sound");
        yield return new WaitUntil(() => source.isPlaying == false);
        Debug.Log("DestroySFX");
        Destroy(source);
        source = null;
    }

}
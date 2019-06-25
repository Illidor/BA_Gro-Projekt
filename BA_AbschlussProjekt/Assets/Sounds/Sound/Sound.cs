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

    [HideInInspector]
    public AudioSource source;

    /// <summary>
    /// Playing the Sound File on the index. Put it to the Place where the Sound should be triggered
    /// </summary>
    public void playSound(int index)
    {
        if(source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }
        if(index < clips.Count)
        {
            source.clip = clips[index];
            source.Play();
        }

        if(source != null)
        {
            StartCoroutine(destroySoundComponent());
        }
    }

    private IEnumerator destroySoundComponent()
    {
        Debug.Log("Playing Sound");
        yield return new WaitUntil(() => source.isPlaying == false);
        Destroy(source);
        source = null;
    }

}
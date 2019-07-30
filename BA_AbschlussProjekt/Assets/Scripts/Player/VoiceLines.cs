using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    public static VoiceLines instance;

    public List<AudioSource> voiceLines = new List<AudioSource>();
    public List<AudioSource> dillenVoiceLines = new List<AudioSource>();

    private bool voiceLinePlayed = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(DelayWakeUpVoiceLine());
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= 180 && !voiceLinePlayed)
        {
            voiceLinePlayed = true;
            if(Random.Range(0,2) >= 1)
            {
                PlayVoiceLine(7, 0f);
            }
            else
            {
                PlayVoiceLine(8, 0f);
            }
        }
    }

    public void PlayVoiceLine(int index, float delay)
    {
        StartCoroutine(DelayVoiceLine(index, delay));
    }

    public void PlayDillenVoiceLine(int index, float delay)
    {
        StartCoroutine(DelayDillenVoiceLine(index, delay));
    }

    private IEnumerator DelayVoiceLine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        voiceLines[index].Play();
    }

    private IEnumerator DelayDillenVoiceLine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        dillenVoiceLines[index].Play();
    }

    private IEnumerator DelayWakeUpVoiceLine()
    {
        yield return new WaitForSeconds(4f);

        voiceLines[0].Play();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines : MonoBehaviour
{
    public static VoiceLines instance;

    public List<AudioSource> voiceLines = new List<AudioSource>();
    public List<AudioSource> dillenVoiceLines = new List<AudioSource>();

    private bool voiceLinePlayed = false;
    private float? deltaTime = null;
    public bool solved = false;

    private InteractionScript player;

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

        player = FindObjectOfType<InteractionScript>();
    }

    void Start()
    {
       StartCoroutine(DelayWakeUpVoiceLine());
    }

    void Update()
    {
        if((Time.time >= deltaTime + 180) && !voiceLinePlayed && !solved)
        {
            Debug.Log("start voiceline after 180");
            voiceLinePlayed = true;
            PlayVoiceLine(Random.Range(7, 9), 0f);
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

        StartVoicelinePlaying(index);
    }

    private void StartVoicelinePlaying(int index)
    {
        voiceLines[ index ].Play();
        player.FreezePlayer(voiceLines[ index ].clip.length);
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

    public void SetDeltaTime()
    {
        deltaTime = Time.time;
        Debug.Log(deltaTime);
    }
}

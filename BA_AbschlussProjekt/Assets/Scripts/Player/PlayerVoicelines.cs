using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoicelines : MonoBehaviour
{
    public static PlayerVoicelines instance;

    public List<AudioSource> voiceLines = new List<AudioSource>();

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
        
    }

    public void PlayVoiceLine(int index, float delay)
    {
        StartCoroutine(DelayVoiceLine(index, delay));
    }

    private IEnumerator DelayVoiceLine(int index, float delay)
    {
        yield return new WaitForSeconds(delay);

        voiceLines[index].Play();
    }

    private IEnumerator DelayWakeUpVoiceLine()
    {
        yield return new WaitForSeconds(4f);

        voiceLines[0].Play();

    }
}

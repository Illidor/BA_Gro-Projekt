using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLineTrigger : MonoBehaviour
{
    public int index;

    public float delay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerVoicelines.instance.PlayVoiceLine(index, delay);
            Destroy(gameObject);
        }
    }
}

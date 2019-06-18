using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matress : GrabInteractable
{
    protected new enum SoundTypes
    {
        pickup = 0,
        drop = 1,
        pull = 2
    }
    // Start is called before the first frame update
    void Start()
    {
        audioManager.PlaySound(soundNames[(int)SoundTypes.pull], this);
        GetComponent<AudioSource>().mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Mathf.Sqrt(Mathf.Pow(rigid.velocity.x, 2) + Mathf.Pow(rigid.velocity.z, 2));
        if (speed < -1 || speed > 1)
        {
            if (soundSources[0].mute == true)
            {
                soundSources[0].mute = false;
            }
        }
        else
        {
            if (soundSources[0].mute == false)
            {
                soundSources[0].mute = true;
            }
        }
    }
}

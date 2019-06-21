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
        AudioManager.PlaySound(SoundNames[(int)SoundTypes.pull], this);
        GetComponent<AudioSource>().mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Mathf.Sqrt(Mathf.Pow(rigid.velocity.x, 2) + Mathf.Pow(rigid.velocity.z, 2));
        if (speed < -1 || speed > 1)
        {
            if (SoundSources[0].mute == true)
            {
                SoundSources[0].mute = false;
            }
        }
        else
        {
            if (SoundSources.Length > 0 && SoundSources[0].mute == false)
            {
                SoundSources[0].mute = true;
            }
        }
    }
}

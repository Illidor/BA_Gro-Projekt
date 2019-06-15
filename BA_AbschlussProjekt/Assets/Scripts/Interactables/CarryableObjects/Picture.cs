using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : GrabInteractable
{
    [SerializeField]
    private bool broken = false;

    public List<GameObject> pictureParts = new List<GameObject>();
    private MeshRenderer pictureUnbroken;
    private BoxCollider interactionCollider;
    protected new enum SoundTypes
    {
        pickup = 0,
        drop = 1,
        destroy = 2
    }

    void Start()
    {
        pictureUnbroken = GetComponent<MeshRenderer>();
        interactionCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // Check for Physics Material            no idea what this comment means, maybe a todo? The code had nothing to do with physics materials... I'll leave it in just in case
        if(IsBeeingCarried == false)
        {
            if (other.collider.material.bounciness < 0.6 && !broken && rigid.velocity.y < -10)
            {
                Break();
            }
            else
            {
                PlaySound(soundNames[(int)SoundTypes.drop]);
            }
        }
    }

    private void Break()
    {
        if (GetComponent<AudioSource>() != null)
        {
            PlaySound(soundNames[(int)SoundTypes.destroy]);
        }
        broken = true;
        foreach (GameObject part in pictureParts)
        {
            pictureUnbroken.enabled = false;
            interactionCollider.enabled = false;
            part.SetActive(true);
            part.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f)));
        }
    }
}

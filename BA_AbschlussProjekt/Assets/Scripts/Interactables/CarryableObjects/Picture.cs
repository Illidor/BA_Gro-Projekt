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

        textToDisplayOnHover = "Click to pick up " + DisplayName;
    }

    private new void OnCollisionEnter(Collision other)
    {
        // Check for Physics Material            no idea what this comment means, maybe a todo? The code had nothing to do with physics materials... I'll leave it in just in case
        if(IsBeeingCarried == false)
        {
            Debug.Log("Not carried drop");
            if (other.collider.material.bounciness < 0.6 && !broken && velocity < -10)
            {
                Debug.Log("Break");
                Break();
            }
            else if (velocity < -2)
            {
                audioManager.PlaySound(soundNames[(int)SoundTypes.drop], this);
            }
        }
    }

    private void Break()
    {
        try
        {
            audioManager.PlaySound(soundNames[(int)SoundTypes.destroy], this);
        }
        catch { }


        broken = true;
        foreach (GameObject part in pictureParts)
        {
            pictureUnbroken.enabled = false;
            interactionCollider.enabled = false;
            part.SetActive(true);
            part.GetComponent<Rigidbody>().AddForce(new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f)));
        }

        this.enabled = false;
    }
}

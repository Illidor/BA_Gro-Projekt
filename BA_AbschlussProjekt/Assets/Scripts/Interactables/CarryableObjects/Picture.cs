using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Picture : GrabInteractable
{
    [SerializeField]
    private List<GameObject> pictureParts = new List<GameObject>();
    [SerializeField]
    private GameObject pictureUnbroken;

    private BoxCollider interactionCollider;
    private bool broken = false;

    void Start()
    {
        interactionCollider = GetComponent<BoxCollider>();

        textToDisplayOnHover = "Click to pick up " + DisplayName;
    }

    private void OnCollisionEnter(Collision other)
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
                //Todo: Play Sound
                try
                {
                    GetComponent<Sound>().PlaySound(0);
                }
                catch (System.Exception) { }
            }
        }
    }

    private void Break()
    {
        broken = true;
        interactionCollider.enabled = false;
        foreach (GameObject part in pictureParts)
        {
            part.SetActive(true);
            part.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
        }

        enabled = false;
    }
}

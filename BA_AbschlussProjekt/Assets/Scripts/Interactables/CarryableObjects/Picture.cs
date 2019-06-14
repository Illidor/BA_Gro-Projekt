using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picture : GrabInteractable
{
    public List<GameObject> pictureParts = new List<GameObject>();
    private MeshRenderer pictureUnbroken;
    private BoxCollider interactionCollider;

    void Start()
    {
        pictureUnbroken = GetComponent<MeshRenderer>();
        interactionCollider = GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for Physics Material            no idea what this comment means, maybe a todo? The code had nothing to do with physics materials... I'll leave it in just in case
        if(isBeeingCarried == false)
        {
            foreach (GameObject part in pictureParts)
            {
                pictureUnbroken.enabled = false;
                interactionCollider.enabled = false;
                part.SetActive(true);
                part.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
            }
        }
    }
}

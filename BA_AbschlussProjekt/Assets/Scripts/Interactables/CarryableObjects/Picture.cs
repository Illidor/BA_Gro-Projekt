using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class Picture : GrabInteractable
{
    public static event UnityAction PlayerFailed;
    public static void InvolePlayerFailed() { PlayerFailed?.Invoke(); }

    [SerializeField]
    private bool willTriggerDeathOnDestroy = false;
    [SerializeField]
    private float materialBouncinessThreshholdToNotBreak = 0.6f;
    [SerializeField]
    private float velocityThreshholdToNotBreak = -10;
    [SerializeField]
    private List<GameObject> pictureParts = new List<GameObject>();
    [SerializeField]
    private GameObject pictureUnbroken;
    [SerializeField]
    private Sound breakSound;

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

        float velocity = rigidbody.velocity.y;

        if (IsBeeingCarried == false)
        {
            Debug.Log("Not carried drop");
            if (other.collider.material.bounciness < materialBouncinessThreshholdToNotBreak && 
                !broken && 
                velocity < velocityThreshholdToNotBreak)
            {
                Debug.Log("Break");
                Break();
            }
            else if (velocity < -2f)
            {
                //Todo: Play Sound
                breakSound?.PlaySound(0);
            }
        }
    }

    public void Break()
    {
        broken = true;
        interactionCollider.enabled = false;
        foreach (GameObject part in pictureParts)
        {
            part.SetActive(true);
            part.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));
        }

        enabled = false;

        StartCoroutine(DisableCollider());

        if (willTriggerDeathOnDestroy)
            PlayerFailed?.Invoke();
    }

    private IEnumerator DisableCollider()
    {
        yield return new WaitForSeconds(10f);

        foreach (GameObject part in pictureParts)
        {
            part.GetComponent<Rigidbody>().isKinematic = true;
            part.GetComponent<Rigidbody>().useGravity = false;
            part.GetComponent<Collider>().enabled = false;
        }
    }
}

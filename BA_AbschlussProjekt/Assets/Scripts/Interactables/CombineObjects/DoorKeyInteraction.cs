using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : InteractionFoundation, ICombinable        // I implemented ICombinable because there was a combine method ~Seb
{
    [SerializeField]
    private float timeToWaitUntilDoorOpens = 1.65f;
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string objectToInteractWith;

    [SerializeField]
    protected Sound interactSound;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteractWith)
        {
            keyInLock.SetActive(true);
            ((GrabInteractable)interactingComponent).PutDown(player);
            Destroy(interactingComponent.gameObject);

            interactSound?.PlaySound(0);

            StartCoroutine(OpenDoor(player, interactingComponent));
            
            return true;
        }
        return false;
    }

    private IEnumerator OpenDoor(InteractionScript player, BaseInteractable interactingComponent)
    {
        yield return new WaitForSeconds(timeToWaitUntilDoorOpens);

        GetComponent<Animator>().SetTrigger("open");
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

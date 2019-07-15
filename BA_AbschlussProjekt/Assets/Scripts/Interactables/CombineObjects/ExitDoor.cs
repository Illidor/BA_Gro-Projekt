using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ExitDoor : BaseInteractable, ICombinable   
{
    [SerializeField]
    private float timeToWaitUntilDoorOpens = 1.65f;
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string nameOfKeysGameobject = "Key";

    [SerializeField]
    protected Sound interactSound;
    [SerializeField]
    protected Sound doorOpeningSound;

    private bool isOpen = false;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crowbar crowbar)
            crowbar.Break(player);


        if (interactingComponent.name == nameOfKeysGameobject)
        {
            keyInLock.SetActive(true);

            ((GrabInteractable)interactingComponent).PutDown(player);
            Destroy(interactingComponent.gameObject);

            interactSound?.PlaySound(0);

            StartCoroutine(OpenDoor(player, interactingComponent));

            isOpen = true;

            return true;
        }
        return false;
    }

    private IEnumerator OpenDoor(InteractionScript player, BaseInteractable interactingComponent)
    {
        yield return new WaitForSeconds(timeToWaitUntilDoorOpens);

        GetComponent<PlayableDirector>().Play();
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

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (isOpen)
            return false;

        interactSound?.PlaySound(0);
        return true;
    }
}

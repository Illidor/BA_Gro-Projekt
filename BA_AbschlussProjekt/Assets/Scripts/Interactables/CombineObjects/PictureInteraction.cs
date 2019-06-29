using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInteraction : InteractionFoundation, ICombinable
{
    public GameObject objectToInteract;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteract.name)
        {
            ((GrabInteractable)interactingComponent).PutDown(player);
            interactingComponent.GetComponent<Rigidbody>().isKinematic = true;
            interactingComponent.GetComponent<Collider>().enabled = false;
            interactingComponent.transform.SetParent(transform);
            interactingComponent.transform.localPosition = Vector3.zero;
            interactingComponent.transform.localEulerAngles = Vector3.zero;

            GetComponent<Collider>().enabled = false;
            return true;
        }
        return false;
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

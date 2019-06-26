using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : InteractionFoundation, ICombinable        // I implemented ICombinable because there was a combine method ~Seb
{
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string objectToInteractWith;

    [SerializeField]
    protected string interactSound;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteractWith)
        {
            keyInLock.SetActive(true);
            
            GetComponent<Animator>().SetTrigger("open");
            ((GrabInteractable)interactingComponent).PutDown(player);
            Destroy(interactingComponent.gameObject);
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

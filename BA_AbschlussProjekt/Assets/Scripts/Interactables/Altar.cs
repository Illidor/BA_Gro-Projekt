using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : BaseInteractable
{
    public override bool Interact(InteractionScript interactionScript)
    {
        //TODO: implement praying 
        Debug.Log("Prayed at altar");
        return true;
    }

    public bool SecondInteract(InteractionScript interactionScript)
    {
        //TODO: implement destroying
        Debug.Log("Destroyed altar");
        return true;
    }

    public override void HandleInteraction(InteractionScript player)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to Pray at " + DisplayName;
        player.GUIInteractionFeedbackHandler.SecondActionDescription.text = "Press E to Destroy " + DisplayName;

        if (CTRLHub.InteractDown)
            Interact(player);
        else if (CTRLHub.SecondInteractDown)
            SecondInteract(player);
    }
}

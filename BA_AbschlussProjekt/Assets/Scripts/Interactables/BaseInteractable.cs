using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Base class for all interactable objects.
/// </summary>
public abstract class BaseInteractable : InteractionFoundation
{
    protected string textToDisplayOnHover = "";

    protected void Awake()
    {
        if (textToDisplayOnHover.Equals(""))
            textToDisplayOnHover =  "Click to interact with " + DisplayName;

        base.Awake();
    }

    /// <summary>
    /// To be fired when an interaction starts. Return whether the Interaction was successfull (true) or not (false). Usually fired from "HandleInteraction"
    /// </summary>
    /// <param name="interactionScript">The script calling the function (just type "this"), in other words the player</param>
    /// <returns>Whether the Interaction was successfull (true) or not (false)</returns>
    public abstract bool Interact(InteractionScript interactionScript);

    /// <summary>
    /// Call to make the players UI show the possible interaction via text and crosshair change. Should fire "Interact" when necessary 
    /// </summary>
    /// <param name="guiInteractionFeedbackHandler">Collection of references of GUI elements used for interaction feedback inside the gui. Should be present in the current "InteractionScript"</param>
    public virtual void HandleInteraction(InteractionScript player)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = textToDisplayOnHover;

        if (CTRLHub.InteractDown)
            Interact(player);
    }
}
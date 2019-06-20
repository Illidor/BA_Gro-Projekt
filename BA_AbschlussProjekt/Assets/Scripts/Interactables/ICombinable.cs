using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to make an object combinable with a BaseInteractable carried by the player
/// </summary>
public interface ICombinable
{
    /// <summary>
    /// Used to combine given object with BaseInteractables. Usually fired from "HandleCombine"
    /// </summary>
    /// <param name="interactingComponent">The object the player is carrying</param>
    /// <returns>Returns whether the combination was successfull (true) or not (false)</returns>
    bool Combine(InteractionScript player, BaseInteractable interactingComponent);

    /// <summary>
    /// Call to make the players UI show the possible combine via text and crosshair change. Should fire "Combine" when necessary. Possible implementation in the ICombinable file
    /// </summary>
    /// <param name="guiInteractionFeedbackHandler">Collection of references of GUI elements used for interaction feedback inside the gui. Should be present in the current "InteractionScript"</param>
    /// <returns>Returns whether the combination was successfull (true) or not (false)</returns>
    bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding);
    /* Possible implementation:   
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + DisplayName + " with " + currentlyHolding.DisplayName; //Inherit from NamedObject to have access to DisplayName

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    } */
}

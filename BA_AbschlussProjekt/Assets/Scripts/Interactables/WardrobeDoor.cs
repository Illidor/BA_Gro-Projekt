using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeDoor : HingedInteraction, ICombinable
{
    [SerializeField]
    private string wardrobeKeyName = "WardrobeKey";
    [SerializeField]
    private WardrobeDoor otherDoor;
    [SerializeField]
    private Sound doorBreakOpenSound;
    [SerializeField]
    private Sound unlockWithKeySound;

    public bool IsLocked { get; set; }

    private new void Awake()
    {
        IsLocked = true;

        base.Awake();
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

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crowbar)
        {
            doorBreakOpenSound?.PlaySound(0);
        }
        else if (interactingComponent.DisplayName.Equals(wardrobeKeyName))
        {
            unlockWithKeySound?.PlaySound(0);
        }
        else    // if wrong combine object, don't execute code below
        {
            return false; 
        }

        IsLocked = false;
        otherDoor.IsLocked = false;

        return true;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (IsLocked)
            return false;

        return base.CarryOutInteraction(player);
    }




}

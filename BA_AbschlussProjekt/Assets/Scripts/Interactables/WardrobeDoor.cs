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
    private Rigidbody handcuffRB;
    [SerializeField]
    private Sound unlockWithKeySound;
    [SerializeField]
    private Animation animationToPlay;



    public bool IsLocked { get; set; }

    private new void Awake()
    {
        if(handcuffRB != null)
        {
            IsLocked = true;
        }

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
        else if (interactingComponent.name.Equals(wardrobeKeyName))
        {
            unlockWithKeySound?.PlaySound(0);
        }
        else    // if wrong combine object, don't execute code below
        {
            print("Locked");

            return false; 
        }

        IsLocked = false;
        otherDoor.IsLocked = false;

        if (handcuffRB != null)
        {
            Destroy(handcuffRB.gameObject.GetComponent<HingeJoint>());
            Destroy(otherDoor.GetComponent<WardrobeDoor>().handcuffRB.GetComponent<HingeJoint>());

            handcuffRB.useGravity = !IsLocked;
            handcuffRB.isKinematic = IsLocked;
        }

        return true;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (IsLocked)
        {
            if (gameObject.name == "mdl_wardrobe_door_right")
            {
                animationToPlay.Play();
            }
            else
            {
                animationToPlay.Play();
            }
            return false;

        }

        return base.CarryOutInteraction(player);
    }
}

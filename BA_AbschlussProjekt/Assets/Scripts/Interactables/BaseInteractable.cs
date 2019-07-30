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

    [SerializeField]
    protected Conditions conditionsTypeNeededToInteract = Conditions.UpperBodyCondition;
    [SerializeField]
    protected bool isBothHanded;

    protected new void Awake()
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
    public abstract bool CarryOutInteraction(InteractionScript player);

    /// <summary>
    /// Call to make the players UI show the possible interaction via text and crosshair change. Should fire "Interact" when necessary 
    /// </summary>
    /// <param name="guiInteractionFeedbackHandler">Collection of references of GUI elements used for interaction feedback inside the gui. Should be present in the current "InteractionScript"</param>
    public virtual void HandleInteraction(InteractionScript player)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        //player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        //player.GUIInteractionFeedbackHandler.ActionDescription.text = textToDisplayOnHover;
        player.GUIInteractionFeedbackHandler.InteractionSymbolHand.SetActive(true);

        if (CTRLHub.InteractDown)
        {
            CarryOutInteraction(player);

            //Disabled IK for non-carryables
            // if (!player.cR_isRunning)
            // {
            //     player.cR_isRunning = true;
            //    
            //     StartCoroutine(player.IKToObject(this, isBothHanded));
            // }
        }
    }


    public virtual Transform GetIKPoint( bool leftHand)
    {
        List<Transform> transforms = new List<Transform>();     

        foreach (Transform item in GetComponentsInChildren<Transform>())
        {
            if(item.tag == "IK")
            {
                transforms.Add(item);
            }
        }

        if(transforms.Count == 0)
        {
            return null;
        }
        else if(transforms.Count == 1)
        {
            return transforms[0];
        }
        else
        {
            if (!leftHand)
            {
                return transforms[0];
            }

            if (leftHand)
            {
                return transforms[1];
            }
            return null;
        }
    }
}
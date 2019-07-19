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
            if (!player.cR_isRunning)
            {
                player.cR_isRunning = true;
                StartCoroutine(player.IKToObject(this, isBothHanded));
            }
        }
    }


    public virtual Transform GetIKPoint(Transform playerGrabPoint, bool leftHand)
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
            float distance = -1;
            Transform returnTransform = transforms[0];
            foreach (Transform objGrabPoint in transforms)
            {
                if (distance == -1)
                {
                    returnTransform = objGrabPoint;
                    distance = (objGrabPoint.position - playerGrabPoint.position).sqrMagnitude;
                    Debug.Log("Distance: " + distance);
                }
                else if ((objGrabPoint.position - playerGrabPoint.position).sqrMagnitude <= distance)
                {
                    distance = (objGrabPoint.position - playerGrabPoint.position).sqrMagnitude;
                    returnTransform = objGrabPoint;
                    Debug.Log("Distance: " + distance);
                }
            }

            if (leftHand)
            {
                Transform leftReturnTransform = transforms[0];
                foreach (Transform objGrabPoint in transforms)
                {
                    if (distance == -1)
                    {
                        leftReturnTransform = objGrabPoint;
                        distance = (objGrabPoint.position - playerGrabPoint.position).sqrMagnitude;
                        Debug.Log("Distance: " + distance);
                    }
                    else if (objGrabPoint != returnTransform)
                    {
                        return objGrabPoint;
                    }
                }
            }
            return returnTransform;
        }
    }
}
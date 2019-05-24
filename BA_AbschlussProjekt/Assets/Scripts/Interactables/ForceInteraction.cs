using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Forces an interaction on given Interactable when receiving interation itself
/// </summary>
public class ForceInteraction : BaseInteractable
{
    [SerializeField]
    private BaseInteractable toForceInteractionOn;
    [SerializeField] [Tooltip("When true, will only force interaction once.")]
    private bool oneTimeOnly = true;

    private bool alreadyForcedInteraction = false;

    public override bool Interact(InteractionScript interactionScript)
    {
        if (oneTimeOnly && alreadyForcedInteraction)
            return false;
        alreadyForcedInteraction = true;

        return toForceInteractionOn.Interact(interactionScript);
    }
}

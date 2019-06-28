using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionedInteraction : BaseInteractable
{
    [SerializeField]
    protected float minCondition;

    public override void HandleInteraction(InteractionScript player)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = textToDisplayOnHover;

        if (CTRLHub.InteractDown && player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) >= minCondition)
        {
            CarryOutInteraction(player);
        }
    }

}

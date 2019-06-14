using System.Collections;
using System.Collections.Generic;
using CustomEnums;
using UnityEngine;

/// <summary>
/// Used if interaction on object is bound by certain player health conditions
/// </summary>
public abstract class ConditionedInteractable : BaseInteractable
{
    /// <summary>
    /// Able to interact if player condition matches any of the selected
    /// </summary>
    [EnumFlag]
    public InteractionConditions possibleConditionsToInteract = InteractionConditions.All;

    public override bool Interact(InteractionScript player)
    {
        if (CheckConditions(player))     
            return CarryOutInteraction(player);

        return false;
    }

    protected virtual bool CheckConditions(InteractionScript player)
    {
        InteractionConditions playersConditions = player.PlayerHealth.GetInjuriesAsInteractionConditions();
        return possibleConditionsToInteract.HasFlag(playersConditions);
    }

    /// <summary>
    /// Will get excecuted if interact is triggered and the conditions are met
    /// </summary>
    /// <returns>Returns whether the Interaction was successfull (true) or not (false)</returns>
    public abstract bool CarryOutInteraction(InteractionScript player);
    
}

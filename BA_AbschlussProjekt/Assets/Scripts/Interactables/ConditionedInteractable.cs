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

    public override bool Interact(InteractionScript player, Conditions condition, float minCondition)
    {
        if (CheckConditions(player, condition) > minCondition)     
            return CarryOutInteraction(player);

        return false;
    }

    protected virtual float CheckConditions(InteractionScript player, Conditions condition)
    {
        float playersConditions = player.PlayerHealth.getCondition(condition);
        return playersConditions;
    }

    /// <summary>
    /// Will get excecuted if interact is triggered and the conditions are met
    /// </summary>
    /// <returns>Returns whether the Interaction was successfull (true) or not (false)</returns>
    public abstract bool CarryOutInteraction(InteractionScript player);
    
}

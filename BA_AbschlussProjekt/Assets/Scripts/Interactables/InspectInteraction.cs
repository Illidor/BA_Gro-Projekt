using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectInteraction : BaseInteractable
{

    public Carryable secondInteraction;

    public override bool Combine(GameObject gameObject)
    {
        return true;
    }

    public override bool Interact(InteractionScript interactionScript)
    {
        return false;
    }

    public override bool Use()
    {
        return false;
    }

}

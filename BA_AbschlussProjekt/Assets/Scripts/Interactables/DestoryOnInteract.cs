using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnInteract : BaseInteractable
{
    public override bool CarryOutInteraction(InteractionScript player)
    {
        Destroy(gameObject);
        return true;
    }
}

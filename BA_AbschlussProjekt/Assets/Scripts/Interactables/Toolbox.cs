using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : BaseInteractable
{
    public override bool CarryOutInteraction(InteractionScript player)
    {
        GetComponent<Animation>().Play();
        return true;
    }
}

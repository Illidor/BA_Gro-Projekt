using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : BaseInteractable
{
    private bool used = false;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (!used)
        {
            GetComponent<Animation>().Play();
            return true;
        }
        return false;
    }
}

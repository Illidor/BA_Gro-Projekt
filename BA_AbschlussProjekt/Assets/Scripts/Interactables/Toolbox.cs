using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : BaseInteractable
{
    private bool used = false;

    [SerializeField]
    private AnimationClip close;
    [SerializeField]
    private AnimationClip open;


    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (!used)
        {
            used = true;
            GetComponent<Animation>().clip = open;
            GetComponent<Animation>().Play();
            return true;
        }
        else
        {
            used = false;
            GetComponent<Animation>().clip = close;
            GetComponent<Animation>().Play();
        }

        return false;
    }
}

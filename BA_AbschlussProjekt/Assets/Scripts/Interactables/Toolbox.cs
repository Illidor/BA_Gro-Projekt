using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbox : BaseInteractable
{
    private bool used = false;
    private GameObject parent;

    [SerializeField]
    private AnimationClip close;
    [SerializeField]
    private AnimationClip open;

    private void Awake()
    {
        parent = gameObject.transform.parent.gameObject;

        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {

        if (!used)
        {
            used = true;
            parent.gameObject.GetComponent<Animation>().clip = open;
            parent.gameObject.GetComponent<Animation>().Play();
            return true;
        }
        else
        {
            used = false;
            parent.gameObject.GetComponent<Animation>().clip = close;
            parent.gameObject.GetComponent<Animation>().Play();
        }

        return false;
    }
}

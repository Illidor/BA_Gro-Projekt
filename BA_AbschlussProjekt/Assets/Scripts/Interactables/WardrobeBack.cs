using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WardrobeBack : HingedInteraction, ICombinable
{
    public static event UnityAction ShockPlayer;

    [SerializeField]
    private float smashStrength = 1;
    [SerializeField]
    private Sound wardrobeBackBreakSound;

    private bool isBrokenOut = false;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crowbar)
        {
            //doorBreakOpenSound?.PlaySound(0);
            ShockPlayer?.Invoke();
            return true;
        }
        return true;
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        throw new System.NotImplementedException();
    }

    //public override bool CarryOutInteraction(InteractionScript player)
    //{
    //    if (isBrokenOut)
    //        return base.CarryOutInteraction(player);

    //    Rigidbody rigidbody = GetComponent<Rigidbody>();
    //    rigidbody.isKinematic = false;
    //    rigidbody.AddForce( - (transform.position - player.transform.position).normalized * smashStrength, ForceMode.Impulse);

    //    wardrobeBackBreakSound?.PlaySound(0);

    //    isBrokenOut = true;

    //    return true;
    //}
}

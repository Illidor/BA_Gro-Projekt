using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBack : GrabInteractable
{
    [SerializeField]
    private float smashStrength = 1;
    [SerializeField]
    private Sound wardrobeBackBreakSound;

    private bool isBrokenOut = false;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (isBrokenOut)
            return base.CarryOutInteraction(player);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce( - (transform.position - player.transform.position).normalized * smashStrength, ForceMode.Impulse);

        wardrobeBackBreakSound?.PlaySound(0);

        isBrokenOut = true;

        return true;
    }
}

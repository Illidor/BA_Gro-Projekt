using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardrobeBack : BaseInteractable
{
    [SerializeField]
    private float smashStrength = 1;
    [SerializeField]
    private Sound wardrobeBackBreakSound;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce((transform.position - player.transform.position).normalized * smashStrength, ForceMode.Impulse);

        wardrobeBackBreakSound?.PlaySound(0);

        return true;
    }
}

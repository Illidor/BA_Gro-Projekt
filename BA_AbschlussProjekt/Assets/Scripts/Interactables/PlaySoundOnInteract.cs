using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Sound))]
public class PlaySoundOnInteract : BaseInteractable
{
    private Sound soundToPlayOnInteract;

    private new void Awake()
    {
        soundToPlayOnInteract = GetComponent<Sound>();

        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        soundToPlayOnInteract?.PlaySound(0);
        return true;
    }
}

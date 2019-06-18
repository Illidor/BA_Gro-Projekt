using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crutch : GrabInteractable
{
    [SerializeField]
    private float reachIncreaseOnCarry = 1;

    protected override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        player.IncreaseReach(reachIncreaseOnCarry);
        PlaySound(soundNames[(int)SoundTypes.pickup]);

        return base.CarryOutInteraction_Carry(player);
    }

    public override void PutDown(InteractionScript player)
    {
        player.ResetReachToDefault();

        base.PutDown(player);
    }
}

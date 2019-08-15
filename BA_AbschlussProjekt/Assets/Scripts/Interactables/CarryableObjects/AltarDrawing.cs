using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarDrawing : GrabInteractable
{
    public override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        VoiceLines.instance.SetDeltaTime();
        base.CarryOutInteraction_Carry(player);
        return true;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinPuzzleDoor : BaseInteractable
{
    public override bool Interact(InteractionScript interactionScript)
    {
        return false; //TODO play locked door sound
    }
}

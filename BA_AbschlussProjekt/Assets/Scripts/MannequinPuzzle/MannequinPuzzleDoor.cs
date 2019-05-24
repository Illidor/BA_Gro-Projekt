using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinPuzzleDoor : BaseInteractable
{
    private bool isOpen = false;

    public override bool Interact(InteractionScript interactionScript)
    {
        return false; //TODO play locked door sound
    }

    public void UnlockAndOpen()
    {
        if (!GetComponent<Animation>().isPlaying && !isOpen)
        {
            GetComponent<Animation>().Play();
            isOpen = true;
        }
    }
}


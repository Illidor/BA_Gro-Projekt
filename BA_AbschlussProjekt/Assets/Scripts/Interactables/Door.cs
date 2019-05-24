using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseInteractable
{
    public Animation openDoor;

    private bool isOpen = false;

    public override bool Interact(InteractionScript interactionScript)
    {
        return OpenDoor();
    }

    /// <summary>
    /// Opens the door. Returns whether the door got successfully opend or not.
    /// </summary>
    /// <returns>Whether the door got opend or not</returns>
    private bool OpenDoor()
    {
        if (!openDoor.isPlaying && !isOpen)
        {
            openDoor.Play();
            isOpen = true;
            return true;
        }
        return false;
    }
}

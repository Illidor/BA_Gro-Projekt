using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseInteractable
{
    public override bool Interact()
    {
        return OpenDoor();
    }

    /// <summary>
    /// Opens the door. Returns whether the door got successfully opend or not.
    /// </summary>
    /// <returns>Whether the door got opend or not</returns>
    private bool OpenDoor()
    {
        throw new NotImplementedException(); //TODO inplement door logic
    }
}

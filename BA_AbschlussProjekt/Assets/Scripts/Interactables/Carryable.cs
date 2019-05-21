using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic of objects the player is able to pickup and carry.
/// </summary>
public class Carryable : BaseInteractable
{
    public override bool Interact()
    {
        return GrabObject();
    }

    private bool GrabObject()
    {
        throw new NotImplementedException();
    }
}

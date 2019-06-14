using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to make an object combinable with a BaseInteractable carried by the player
/// </summary>
public interface ICombinable
{
    /// <summary>
    /// Used to combine given object with BaseInteractables.
    /// </summary>
    /// <param name="interactingComponent">The object the player is carrying</param>
    /// <returns>Returns whether the combination was successfull (true) or not (false)</returns>
    bool Combine(InteractionScript player, BaseInteractable interactingComponent);
}

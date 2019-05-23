using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all interactable objects.
/// </summary>
public abstract class BaseInteractable : MonoBehaviour
{
    /// <summary>
    /// To be fired when an interaction starts. Return whether the Interaction was successfull (true) or not (false)
    /// </summary>
    /// <returns>Whether the Interaction was successfull (true) or not (false)</returns>
    public abstract bool Interact(InteractionScript interactionScript);
}

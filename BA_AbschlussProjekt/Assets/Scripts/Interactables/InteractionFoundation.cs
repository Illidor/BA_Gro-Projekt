using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A collection of essential components all sort of interactiony objects need.
/// Any sort of interaction should have this somewhere in their inheritance hirarchy, be it IUseable, ICombinable or derived from BaseInteraction.
/// </summary>
public class InteractionFoundation : MonoBehaviour
{
    /// <summary>
    /// The name to use for displaying this object to the player
    /// </summary>
    [field: LabelOverride("Display Name"), SerializeField, Tooltip("The name to use for displaying this object to the player")]
    public string DisplayName { get; protected set; }

    protected void Awake()
    {
        if (DisplayName.Equals(""))
            DisplayName = name;
    }
}

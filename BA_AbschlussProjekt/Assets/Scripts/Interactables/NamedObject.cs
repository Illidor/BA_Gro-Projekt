using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedObject : MonoBehaviour
{
    /// <summary>
    /// The name to use for displaying this object to the player
    /// </summary>
    [field: LabelOverride("Display Name"), SerializeField, Tooltip("The name to use for displaying this object to the player")]
    public string DisplayName { get; protected set; }
}

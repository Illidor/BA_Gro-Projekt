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

    [SerializeField]
    protected AudioSource[] soundSources;
    //TODO: add summary
    public AudioSource[] SoundSources
    {
        get => soundSources;
        set => soundSources = value;
    }

    [SerializeField]
    protected string[] soundNames;
    //TODO: add summary
    public string[] SoundNames
    {
        get => soundNames;
        protected set => soundNames = value;
    }

    protected void Awake()
    {
        if (DisplayName.Equals(""))
            DisplayName = name;
    }

    protected enum SoundTypes
    {
        pickup = 0,
        drop = 1 //TODO: override
    }
}

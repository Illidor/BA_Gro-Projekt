using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mannequin : BaseInteractable
{
    [SerializeField]
    private MannequinColors mannequinColor;

    public MannequinColors MannequinColor { get { return mannequinColor; } }

    public override bool Interact(InteractionScript interactionScript)
    {
        return false;
    }
}

public enum MannequinColors
{
    none,
    Red,
    Blue,
    Green,
    Yellow
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinObject : Carryable
{
    [SerializeField]
    private MannequinColors mannequinColor;

    public MannequinColors MannequinColor { get { return mannequinColor; } }

    public override bool Combine(InteractionScript interactionScript, BaseInteractable combinationComponent)
    {
        if ((combinationComponent as Mannequin)?.MannequinColor == MannequinColor)
        {
            DetachFromPlayer(interactionScript);
            rigidbody.isKinematic = true;
            transform.position = combinationComponent.transform.position + new Vector3(0, 1, 0); //TODO implement meaningfull mannequin combine action
            collider.enabled = false;

            MannequinPuzzle.mannequinSatisfied(mannequinColor);

            return true;
        }
        return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic of objects the player is able to pickup and carry.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Carryable : BaseInteractable
{
    private const string noPlayerCollisionLayerName = "NoPlayerCollision";

    protected new Rigidbody rigidbody;
    protected new Collider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider  = GetComponent<Collider>();
    }

    public override bool Interact(InteractionScript interactionScript)
    {
        Grab(interactionScript);
        return true;
    }

    /// <summary>
    /// To be fired when an interaction between a carried item and another interactable starts. Return whether the combining was successfull (true) or not (false)
    /// </summary>
    /// <returns>Return whether the combining was successfull (true) or not (false)</returns>
    public virtual bool Combine(InteractionScript interactionScript, BaseInteractable combinationComponent)
    {
        return false;
    }

    private void Grab(InteractionScript interactionScript)
    {
        AttachToPlayer(interactionScript);
        transform.localPosition = Vector3.zero;

        gameObject.layer = LayerMask.NameToLayer(noPlayerCollisionLayerName);
    }

    /// <summary>
    /// Attaches itself to the player. Changes its parent to the hand, sets itself to kinematic and sets itself as carried object
    /// </summary>
    protected void AttachToPlayer(InteractionScript interactionScript)
    {
        transform.parent = interactionScript.GrabingPoint.transform;
        rigidbody.isKinematic = true;
        interactionScript.CarriedObject = this;
    }

    public void Throw(InteractionScript interactionScript, float throwingStrength)
    {
        DetachFromPlayer(interactionScript);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rigidbody.AddForce(ray.direction.normalized * throwingStrength, ForceMode.Impulse);

        Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check?)
    }

    /// <summary>
    /// Detaches itself from the player. Changes its parent to global, sets itself to non-kinematic and sets the carried object to null
    /// </summary>
    protected void DetachFromPlayer(InteractionScript interactionScript)
    {
        transform.parent = InstancePool.transform;
        rigidbody.isKinematic = false;
        interactionScript.CarriedObject = null;
    }

    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}

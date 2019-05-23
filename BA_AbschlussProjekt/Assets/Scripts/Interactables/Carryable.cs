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

    private new Rigidbody rigidbody;
    private new Collider collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider  = GetComponent<Collider>();
    }

    public override bool Interact(InteractionScript interactionScript)
    {
        if (interactionScript.IsCarrying)   // if player is carrying this
        {
            Drop(interactionScript);
            return false;
        }

        Grab(interactionScript);
        return true;
    }

    private void Grab(InteractionScript interactionScript)
    {
        transform.parent = interactionScript.GrabingPoint.transform;
        rigidbody.isKinematic = true;
        interactionScript.CarriedObject = this;
        transform.localPosition = Vector3.zero;

        gameObject.layer = LayerMask.NameToLayer(noPlayerCollisionLayerName);
    }

    private void Drop(InteractionScript interactionScript)
    {
        transform.parent = InstancePool.transform;
        rigidbody.isKinematic = false;
        interactionScript.CarriedObject = null;

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public void Throw(InteractionScript interactionScript, float throwingStrength)
    {
        transform.parent = InstancePool.transform;
        rigidbody.isKinematic = false;
        interactionScript.CarriedObject = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        rigidbody.AddForce(ray.direction.normalized * throwingStrength, ForceMode.Impulse);

        Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check?)
    }

    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}

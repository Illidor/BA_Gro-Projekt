using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic of objects the player is able to pickup and carry.
/// </summary>
[Obsolete]
public class Carryable : ObjectInteraction
{
    private const string noPlayerCollisionLayerName = "NoPlayerCollision";
    
    [HideInInspector] public bool isInUse = false;

    

    public override bool Interact(InteractionScript player, Conditions condition, float minCondition)
    {
        Grab(player);
        return true;
    }

    /// <summary>
    /// To be fired when an interaction between a carried item and another interactable starts. Return whether the combining was successfull (true) or not (false)
    /// </summary>
    /// <returns>Return whether the combining was successfull (true) or not (false)</returns>
    

    private void Grab(InteractionScript interactionScript)
    {
        PlaySound(SoundNames[Convert.ToInt16(SoundTypes.pickup)]);
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
        //interactionScript.UsedObject = this; 
    }

    public void Throw(InteractionScript interactionScript, float throwingStrength)
    {
        if(isInUse == false)
        {
            DetachFromPlayer(interactionScript);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            rigidbody.AddForce(ray.direction.normalized * throwingStrength, ForceMode.Impulse);

            Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check?)
        }
    }

    ///// <summary>
    /////use currently Equipped Object
    ///// </summary>
    //public virtual void Use()
    //{
    //    Debug.Log("used");
    //}

    
    private void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    

    public bool Use()
    {
        if (!isInUse)
            isInUse = true;

        return true;
    }
}

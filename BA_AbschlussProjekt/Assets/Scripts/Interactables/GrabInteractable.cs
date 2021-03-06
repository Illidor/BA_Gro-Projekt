﻿using System;
using CustomEnums;
using UnityEngine;

/// <summary>
/// Used for objects that can be carried or pushed
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class GrabInteractable : BaseInteractable
{
    private readonly Vector3 putDownOffset = new Vector3(0, 0.25f, 0);
    /// <summary>
    /// If the player puts down on object and the surface looking at is tilted less than that much degrees, the object will be put on top of said surface (eg. a table).
    /// Else, the object will be put down in between the player and the point he is looking at (eg. a wall)
    /// </summary>
    private readonly float putDownOnTopDefinitionOffsetInDegrees = 45f; 


    //Sound     //please add a <summary> to serialized fields
    [SerializeField]
    protected float velocity;

    [SerializeField]
    protected float minConditionToCarry = 0.5f;
    [SerializeField] [Tooltip("If over 2, pushing is not possible")]
    protected float minConditionToPush = 3;
    [SerializeField][Tooltip("Will search on this gameobject if not provided.")]
    protected Sound soundToPlayOnInteract;

    protected new Rigidbody rigidbody;
    protected Rigidbody rigidbodyPulling;

    protected new Collider collider;

    public bool IsBeeingCarried { get; protected set; }

    public bool IsBeeingPulled { get; protected set; }


    protected new void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        if (soundToPlayOnInteract == null)
            soundToPlayOnInteract = GetComponent<Sound>();

        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) >= minConditionToCarry)
        {
            return CarryOutInteraction_Carry(player);
        }
        else if (player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) >= minConditionToPush)
        {
            return CarryOutInteraction_Push(player);
        }

        return false;
    }

    /// <summary>
    /// Carry out interaction carry
    /// </summary>
    protected virtual bool CarryOutInteraction_Carry(InteractionScript player)
    {
        Debug.Log("carrying");
        gameObject.layer = LayerMask.NameToLayer("NoPlayerCollision");
        transform.parent = player.GrabingPoint.transform;
        transform.localPosition = Vector3.zero;
        rigidbody.isKinematic = true;
        player.SetCarriedObject(this);
        IsBeeingCarried = true;

        collider.enabled = false;

        soundToPlayOnInteract?.PlaySound(0);

        return true;
    }

    public virtual void PutDown(InteractionScript player)  //TODO: better putDown implementation instead of simply droping the object
    {
        Debug.Log("put down");

        if (IsBeeingCarried)
        {
            transform.SetParent(InstancePool.transform, true);

            Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(screenCenterRay, out RaycastHit raycastHit, player.GrabingReach))
            {
                // if the player is looking at a surface in his reach, ...
                if (Vector3.Angle(raycastHit.normal, Vector3.up) < putDownOnTopDefinitionOffsetInDegrees)
                {
                    // ... and it's pointing upwards (eg a table), then put the object down obtop of it.
                    transform.position = raycastHit.point + putDownOffset;
                }
                else
                {
                    // ... and it's not pointing upwards (eg a wall), then put the object down inbetween it and the player
                    transform.position = Camera.main.transform.position + screenCenterRay.direction * ((Camera.main.transform.position - raycastHit.point).magnitude * 0.5f );
                }
            }
            else
            {
                // if the player is not looking at a surface, drop the object from the players max reach infront of him
                transform.position = Camera.main.transform.position + screenCenterRay.direction * player.GrabingReach;

                // Alternative: if the player is not looking at a surface, drop the object where the player stands like he just released the grip without caring where it lands.
                // To implement, delete this else block
            }
        }
        else
        {
            transform.parent = InstancePool.transform;
        }

        rigidbody.isKinematic = false;
        player.StopUsingObject();

        IsBeeingPulled = false;
        IsBeeingCarried = false;
        rigidbodyPulling = null;

        collider.enabled = true;

        //Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check)
    }

    protected virtual bool CarryOutInteraction_Push(InteractionScript player)
    {
        player.SetPushedObject(this);

        IsBeeingPulled = true;
        rigidbodyPulling = player.GetComponent<Rigidbody>();

        return true;
    }

    protected void FixedUpdate()
    {
        velocity = rigidbody.velocity.y;
        //TODO: better implementation of pulling. Maybe considering objects weight and players conditions
        //if (IsBeeingPulled)
        //    rigid.velocity = rigidbodyPulling.velocity;

        //new:
        if (IsBeeingPulled)
        {
            Ray r = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Vector3 pointToPull = r.GetPoint(rigidbodyPulling.GetComponent<InteractionScript>().GrabingReach);
            pointToPull = new Vector3(pointToPull.x, transform.position.y, pointToPull.z);
            transform.position = Vector3.Lerp(transform.position,pointToPull, 0.3f);
        }
    }

    protected void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}

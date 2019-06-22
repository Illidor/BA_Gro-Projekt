using System;
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

    /// <summary>
    /// Able to carry object if player condition matches any of the selected
    /// </summary>
    [EnumFlag]
    public InteractionConditions possibleConditionsToCarry = InteractionConditions.All;
    
    /// <summary>
    /// Able to carry object if player condition matches any of the selected
    /// </summary>
    [EnumFlag]
    public InteractionConditions possibleConditionsToPush = InteractionConditions.Unable;

    //Sound     //please add a <summary> to serialized fields
    [SerializeField]
    protected float velocity;


    protected Rigidbody rigid;
    protected Rigidbody rigidbodyPulling;

    protected new Collider collider;

    public bool IsBeeingCarried { get; protected set; }

    public bool IsBeeingPulled { get; protected set; }


    protected new void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        base.Awake();
    }

    public override bool Interact(InteractionScript player)
    {
        InteractionConditions playersConditions = player.PlayerHealth.GetInjuriesAsInteractionConditions();

        if (possibleConditionsToCarry.HasFlag(playersConditions))
        {
            if (GetComponent<AudioSource>() != null)
            {
                AudioManager.PlaySound(SoundNames[(int)SoundTypes.pickup], this);
            }
            return CarryOutInteraction_Carry(player);
        }

        if (possibleConditionsToPush.HasFlag(playersConditions))
        {
            return CarryOutInteraction_Push(player);
        }

        return false;
    }

    protected virtual bool CarryOutInteraction_Carry(InteractionScript player)
    {
        gameObject.layer = LayerMask.NameToLayer("NoPlayerCollision");
        transform.parent = player.GrabingPoint.transform;
        transform.localPosition = Vector3.zero;
        rigid.isKinematic = true;
        player.SetCarriedObject(this);
        IsBeeingCarried = true;

        return true;
    }

    public virtual void PutDown(InteractionScript player)  //TODO: better putDown implementation instead of simply droping the object
    {
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
                    transform.position = raycastHit.point - screenCenterRay.direction;
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
        rigid.isKinematic = false;
        player.StopUsingObject();

        IsBeeingPulled = false;
        IsBeeingCarried = false;
        rigidbodyPulling = null;

        Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check)
    }

    protected virtual bool CarryOutInteraction_Push(InteractionScript player)
    {
        player.SetPushedObject(this);

        IsBeeingPulled = true;
        rigidbodyPulling = player.GetComponent<Rigidbody>();

        return true;
    }

    private void FixedUpdate()
    {
        velocity = rigid.velocity.y;
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

    // Wait, why is this gone? And if that's okey, why is it still here?
    ///// <summary>
    ///// Play a sound of a type eg. pickup, drop
    ///// </summary>
    ///// <param name="soundType">type eg. pickup, drop</param>
    //public void PlaySound(string soundType)
    //{
    //    if (GetComponent<AudioSource>() != null)
    //    {
    //        sounds = GetComponents<AudioSource>();
    //        foreach (AudioSource sound in sounds)
    //        {
    //            if (sound.clip.name == soundType)
    //            {
    //                sound.Play();
    //            }
    //            else
    //            {
    //                AudioManager.AddSound(soundType, this.gameObject);
    //                sounds = GetComponents<AudioSource>();
    //                sounds.First(audios => audios.name == soundType).Play();
    //            }
    //        }
    //    }
    //    else
    //    {
    //        AudioManager.AddSound(soundType, this.gameObject);
    //        sounds = GetComponents<AudioSource>();
    //        sounds[0].Play();
    //    }
    //}

    /// <summary>
    /// play dropsound when falling from high
    /// </summary>
    protected void OnCollisionEnter(Collision other)
    {
        if (velocity < -2)      // Why having a variable "velocity" instead of using "rigid.velocity" directly? 
        {
            if(GetComponent<AudioSource>() != null)
            {
                AudioManager.PlaySound(SoundNames[(int)SoundTypes.drop], this);
            }
        }
    }

}

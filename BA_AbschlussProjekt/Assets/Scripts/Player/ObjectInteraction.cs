using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[Obsolete]
public class ObjectInteraction : BaseInteractable
{
    //[SerializeField][Tooltip("What Interactiontype")]
    //private Enums.ObjectParameters objectParameters;
    [SerializeField][Tooltip("The Rigidbody of the Player")]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private bool pulledOn = false;

    private Transform objectAttachpoint;
    private Transform objectAttachpointAlternative;
    private const string noPlayerCollisionLayerName = "NoPlayerCollision";


    protected new Rigidbody rigidbody;
    protected new Collider collider;

    [SerializeField]
    private float velocity;

    protected new void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        //playerRigidbody = InteractionScript.Get().transform.GetComponent<Rigidbody>();

        base.Awake();
    }

    #region InteractCarryPull
    /// <summary>
    /// Detect Interactionmode
    /// </summary>
    /// <param name="interactionScript"></param>
    /// <returns>Returns if Successful</returns>
    public override bool Interact(InteractionScript player, Conditions condition, float minCondition)
    {
        //switch (objectParameters)
        //{
        //    case Enums.ObjectParameters.CarryOneHand:
        //        CarryOneHand(interactionScript);
        //        break;
        //    case Enums.ObjectParameters.PullOneHand:
        //        PullOneHand(interactionScript);
        //        break;
        //    case Enums.ObjectParameters.CarryTwoHands:
        //        CarryTwoHands(interactionScript);
        //        break;
        //    case Enums.ObjectParameters.PullTwoHands:
        //        PullTwoHands(interactionScript);
        //        break;
        //    default:
        //        return false;
        //}
        return true;
    }

    private void CarryOneHand(InteractionScript interactionScript)
    {
        PlaySound(SoundNames[Convert.ToInt16(SoundTypes.pickup)]);
        //SetIKPoint(interactionScript, 1);
        ConnectToIK(interactionScript, 1);
    }

    private void PullOneHand(InteractionScript interactionScript)
    {
        pulledOn = true;
        //SetIKPoint(interactionScript, 1);
        ConnectToIK(interactionScript, 1);
    }

    private void CarryTwoHands(InteractionScript interactionScript)
    {
        PlaySound(SoundNames[Convert.ToInt16(SoundTypes.pickup)]);
        //SetIKPoint(interactionScript, 2);
        ConnectToIK(interactionScript, 2);
    }

    private void PullTwoHands(InteractionScript interactionScript)
    {
        pulledOn = true;
        //SetIKPoint(interactionScript, 2);
        ConnectToIK(interactionScript, 2);
    }

    #endregion
    #region InverseKinematic

    private void ConnectToIK(InteractionScript interactionScript, int iKPoints)
    {
        //TODO: IK Funktion
        //transform.parent = interactionScript.GrabingPoint.transform;
        //rigidbody.isKinematic = true;
        //interactionScript.UsedObject = (ObjectInteraction)(BaseInteractable)this;
        //interactionScript.IsPulling = true;
        gameObject.layer = LayerMask.NameToLayer(noPlayerCollisionLayerName);
    }

    private void SetIKPoint(InteractionScript interactionScript, int count) //TODO: Change Searchmethod to Object Structure
    {
        if (count == 1)
        {
            objectAttachpoint = this.transform.GetChild(0);
        }
        if (count == 2)
        {
            objectAttachpointAlternative = this.transform.GetChild(1);
        }
    }

    #endregion
    
    public virtual bool Combine(InteractionScript interactionScript, BaseInteractable combinationComponent)
    {
        return false;
    }
    
    protected void PlaySound(string soundType)
    {
        if (GetComponent<AudioSource>() != null)
        {
            SoundSources = GetComponents<AudioSource>();
            foreach (AudioSource sound in SoundSources)
            {
                if (sound.clip.name == soundType)
                {
                    sound.Play();
                }
                else
                {
                    SoundSources = GetComponents<AudioSource>();
                    SoundSources.First(audios => audios.name == soundType).Play();
                }
            }
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }

    public virtual void PutDown(InteractionScript interactionScript)
    {
        DetachFromPlayer(interactionScript);
        pulledOn = false;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check?)
    }

    protected void DetachFromPlayer(InteractionScript interactionScript)
    {
        //TODO: Detach IK
        transform.parent = InstancePool.transform;
        rigidbody.isKinematic = false;
        //interactionScript.UsedObject = null;
    }
    
    protected void ResetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    protected void PushPull()
    {
        if (pulledOn)
        {
            this.rigidbody.velocity = playerRigidbody.velocity;
        }
    }
    protected void FixedUpdate()
    {
        PushPull();
        velocity = rigidbody.velocity.y;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (velocity < -3)
        {
            PlaySound(SoundNames[Convert.ToInt16(SoundTypes.drop)]);
        }
    }
}

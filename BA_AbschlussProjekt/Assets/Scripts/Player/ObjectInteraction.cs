using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ObjectInteraction : BaseInteractable
{
    [SerializeField][Tooltip("What Interactiontype")]
    private Enums.ObjectParameters objectParameters;


    private Transform objectAttachpoint;
    private Transform objectAttachpointAlternative;
    private const string noPlayerCollisionLayerName = "NoPlayerCollision";


    protected new Rigidbody rigidbody;
    protected new Collider collider;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    #region InteractCarryPull
    /// <summary>
    /// Detect Interactionmode
    /// </summary>
    /// <param name="interactionScript"></param>
    /// <returns>Returns if Successful</returns>
    public override bool Interact(InteractionScript interactionScript)
    {
        switch (objectParameters)
        {
            case Enums.ObjectParameters.CarryOneHand:
                CarryOneHand(interactionScript);
                break;
            case Enums.ObjectParameters.PullOneHand:
                PullOneHand(interactionScript);
                break;
            case Enums.ObjectParameters.CarryTwoHands:
                CarryTwoHands(interactionScript);
                break;
            case Enums.ObjectParameters.PullTwoHands:
                PullTwoHands(interactionScript);
                break;
            default:
                return false;
        }
        return true;
    }
    
    private void CarryOneHand(InteractionScript interactionScript)
    {
        //SetIKPoint(interactionScript, 1);
        ConnectToIK(interactionScript, 1);
    }

    private void PullOneHand(InteractionScript interactionScript)
    {
        //SetIKPoint(interactionScript, 1);
        ConnectToIK(interactionScript, 1);
    }

    private void CarryTwoHands(InteractionScript interactionScript)
    {
        //SetIKPoint(interactionScript, 2);
        ConnectToIK(interactionScript, 2);
    }

    private void PullTwoHands(InteractionScript interactionScript)
    {
        //SetIKPoint(interactionScript, 2);
        ConnectToIK(interactionScript, 2);
    }

    #endregion
    #region InverseKinematic

    private void ConnectToIK(InteractionScript interactionScript, int iKPoints)
    {
        //TODO: IK Funktion
        transform.parent = interactionScript.GrabingPoint.transform;
        rigidbody.isKinematic = true;
        interactionScript.UsedObject = this;
        interactionScript.IsPulling = true;
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



    public virtual void PutDown(InteractionScript interactionScript)
    {
        DetachFromPlayer(interactionScript);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Invoke("ResetLayer", 2f); //TODO: Switch to better implementation of invoking ResetLayer. (Maybe with trigger or distance check?)
    }

    protected void DetachFromPlayer(InteractionScript interactionScript)
    {
        //TODO: Detach IK
        transform.parent = InstancePool.transform;
        rigidbody.isKinematic = false;
        interactionScript.UsedObject = null;
    }

    public override bool Combine(GameObject gameObject)
    {
        throw new NotImplementedException();
    }
}

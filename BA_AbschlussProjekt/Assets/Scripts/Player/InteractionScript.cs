//﻿using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Linq;

//public class InteractionScript : MonoBehaviour
//{
//    [SerializeField]
//    [Tooltip("Max distance to objects the player is able to grab")]
//    private float grabingReach = 1.5f;


//    private ObjectInteraction usedObject;
//    public ObjectInteraction UsedObject
//    {
//        get { return usedObject; }
//        set
//        {
//            usedObject = value;
//            IsInteracting = (value == null ? false : true);
//        }
//    }

//    public bool IsInteracting { get; set; }

//    private void Update()
//    {
//        if (CTRLHub.InteractDown)
//            CheckInteraction();

//        if (IsInteracting)
//            HandledDrop();
//    }

//    private void CheckInteraction()
//    {
//        Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

//        if (Physics.Raycast(screenCenterRay, out RaycastHit hit, grabingReach))
//        {
//            BaseInteractable interactableToInteractWith = hit.collider.GetComponent<BaseInteractable>();

//            if (interactableToInteractWith != null)
//            {
//                if (IsCarrying)
//                {
//                    Debug.Log("combine " + CarriedObject.name + " with " + interactableToInteractWith.name);
//                    //CarriedObject.Combine(this, interactableToInteractWith);

//                    interactableToInteractWith.gameObject.GetComponent<BaseInteractable>().Combine(CarriedObject.gameObject);
//                }
//                else if (interactableToInteractWith.gameObject.GetComponent<FlashbackInteraction>() == null)
//                {
//                    interactableToInteractWith.Interact(this);
//                }
//                else
//                {
//                    interactableToInteractWith.gameObject.GetComponent<FlashbackInteraction>().Interact(this);
//                }
//            }
//        }
//        else
//        {
//            if (IsCarrying)
//            {
//                CarriedObject.gameObject.GetComponent<BaseInteractable>().Use();
//            }
//        }
//    }

//    private void HandledDrop()
//    {
//        if (CTRLHub.ThrowUp)
//        {
//            UsedObject.PutDown(this);
//        }
//    }
//}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] [Tooltip("Max distance to objects the player is able to grab empty handed")]
    private float emptyHandedGrabingReach = 1.5f;
    [SerializeField] [Tooltip("Hand the carried object is parented to")]
    private Transform grabingPoint;
    [SerializeField] [Tooltip("Handler of the players injuries")]
    private PlayerHealth playerHealth;

    public PlayerHealth PlayerHealth { get { return playerHealth; } }

    private GrabInteractable UsedObject { get; set; }

    public bool IsCarrying { get; private set; }
    public bool IsPushing  { get; private set; }

    public Transform GrabingPoint { get { return grabingPoint; } }

    private float grabingReach;

    private void Awake()
    {
        grabingReach = emptyHandedGrabingReach;
    }

    private void Update()
    {
        if (CTRLHub.InteractDown)
            CheckInteraction();

        if (IsCarrying || IsPushing)
        {
            if (CTRLHub.DropUp)
                UsedObject.PutDown(this);
        }
    }

    private void CheckInteraction()
    {
        Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        bool didRaycastHit = Physics.Raycast(screenCenterRay, out raycastHit, grabingReach);

        if (IsCarrying == false)
        {
            raycastHit.collider?.GetComponent<BaseInteractable>()?.Interact(this);
        }
        else
        {
            if (didRaycastHit)
            {
                ICombinable objectToCombineWith = raycastHit.collider.GetComponent<ICombinable>();
                if (objectToCombineWith != null &&
                    objectToCombineWith.Combine(this, UsedObject))
                    return;
            }

            if(UsedObject != null)
            {
                UsedObject.GetComponent<IUseable>()?.Use(this);
            }
            else
            {
                //UsedObject
            }
        }
    }

    public void SetCarriedObject(GrabInteractable objectToCarry)
    {
        UsedObject = objectToCarry;
        IsCarrying = true;
        IsPushing = false;
    }

    public void SetPushedObject(GrabInteractable objectToPush)
    {
        UsedObject = objectToPush;
        IsPushing = true;
        IsCarrying = false;
    }

    public void StopUsingObject()
    {
        UsedObject = null;
        IsCarrying = false;
        IsPushing = false;
    }

    public void IncreaseReach(float reachToAdd)
    {
        grabingReach += reachToAdd;
    }

    public void ResetReachToDefault()
    {
        grabingReach = emptyHandedGrabingReach;
    }

    public float GetReach()
    {
        return grabingReach;
    }
}



/* old CheckInteraction
private void CheckInteraction()
{
    Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(screenCenterRay, out RaycastHit hit, grabingReach))
    {
        BaseInteractable interactableToInteractWith = hit.collider.GetComponent<BaseInteractable>();

        if (interactableToInteractWith != null)
        {
            if (IsInteracting)
            {
                Debug.Log("combine " + UsedObject.name + " with " + interactableToInteractWith.name);
                UsedObject.Combine(this, interactableToInteractWith);

                //interactableToInteractWith.gameObject.GetComponent<BaseInteractable>().Combine(UsedObject.gameObject);
            }
            else
            {
                interactableToInteractWith.Interact(this);
            }
        }
        else if (UsedObject != null)
        {
            //UsedObject.Use();
        }
    }
    else if (UsedObject != null)
    {
        //UsedObject.Use();
    }
}
*/

/*

 BaseInteractable interactable = hit.collider.GetComponent<BaseInteractable>();
 if (interactable != null)
    interactable.Interact(this);

    == 
 
hit.collider.GetComponent<BaseInteractable>()?.Interact(this);

 */
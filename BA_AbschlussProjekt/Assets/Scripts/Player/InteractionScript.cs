using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] [Tooltip("Max distance to objects the player is able to grab")]
    private float grabingReach = 1.5f;
    [SerializeField] [Tooltip("Multiplicator of final throwing strength")]
    private float throwingStrength = 1f;
    [SerializeField] [Tooltip("Multiplicator of overtime strength when holding down the throw button")]
    private float throwingChargeModifier = 1f;
    [SerializeField]
    [Tooltip("Maximum throwing strength. Needed to not overpower charge")]
    private float maxThrowingStrength = 30f;
    [Space]
    [SerializeField] [Tooltip("Hand the carried object is parented to")]
    private Transform grabingPoint;


    public Carryable carriedObject;
    public Carryable CarriedObject
    {
        get { return carriedObject; }
        set
        {
            carriedObject = value;
            IsCarrying = (value == null ? false : true);
        }
    }

    public bool IsCarrying { get; set; }

    public Transform GrabingPoint { get { return grabingPoint; } }


    private float throwChargeTimer;


    private void Update()
    {
        if (CTRLHub.InteractDown)
            CheckInteraction();

        if (IsCarrying)
        {
            if (CTRLHub.ThrowUp)
            {
                carriedObject.Throw(this, 0);
            }
            //if is carrying stuff handle use of those
            HandleUseObject();
        }
    }

    private void CheckInteraction()
    {
        Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenCenterRay, out RaycastHit hit, grabingReach))
        {
            BaseInteractable interactableToInteractWith = hit.collider.GetComponent<BaseInteractable>();

            if (interactableToInteractWith != null)
            {
                if (IsCarrying)
                {
                    Debug.Log("combine " + CarriedObject.name  +" with " + interactableToInteractWith.name);
                    CarriedObject.Combine(this, interactableToInteractWith);

                    interactableToInteractWith.gameObject.GetComponent<BaseInteractable>().Combine(CarriedObject.gameObject);
                }
                else
                {
                    interactableToInteractWith.Interact(this);
                }

            }
            else if (CarriedObject != null)
            {
                CarriedObject.Use();
            }
        }
        else if(CarriedObject != null)
        {
            CarriedObject.Use();
        }
    }

    private void HandledDrop()
    {
        if (CTRLHub.ThrowUp)
        {
            carriedObject.Throw(this,0);
        }
    }

    private void HandleUseObject()
    {
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            CarriedObject.Use();
        }
    }

    public static InteractionScript Get()
    {
        return GameObject.Find("Player").GetComponent<InteractionScript>();
    }
}


/*

 BaseInteractable interactable = hit.collider.GetComponent<BaseInteractable>();
 if (interactable != null)
    interactable.Interact(this);

    == 
 
hit.collider.GetComponent<BaseInteractable>()?.Interact(this);

 */

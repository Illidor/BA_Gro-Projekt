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


    private Carryable carriedObject;
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

        if (!IsCarrying)
            HandleThrowing();
    }

    private void CheckInteraction()
    {
        if (IsCarrying)
            CarriedObject.Interact(this);
        else
            SearchForItemToInteract();
    }

    private void SearchForItemToInteract()
    {
        Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(screenCenterRay, out RaycastHit hit, grabingReach))
            hit.collider.GetComponent<BaseInteractable>()?.Interact(this);
    }

    private void HandleThrowing()
    {
        if (CTRLHub.ThrowDown)
            throwChargeTimer = Time.time;

        if (CTRLHub.ThrowUp)
        {
            // time difference times time difference multiplicator times overall multiplicator
            float finalThrowingStrength = 1 + (Time.time - throwChargeTimer) * throwingChargeModifier * throwingStrength; 
            if (finalThrowingStrength > maxThrowingStrength)
                finalThrowingStrength = maxThrowingStrength;

            CarriedObject.Throw(this, finalThrowingStrength);
        }
    }
}


/*

 BaseInteractable interactable = hit.collider.GetComponent<BaseInteractable>();
 if (interactable != null)
    interactable.Interact(this);

    == 
 
hit.collider.GetComponent<BaseInteractable>()?.Interact(this);

 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Max distance to objects the player is able to grab")]
    private float grabingReach = 1.5f;


    private ObjectInteraction usedObject;
    public ObjectInteraction UsedObject
    {
        get { return usedObject; }
        set
        {
            usedObject = value;
            IsInteracting = (value == null ? false : true);
        }
    }

    public bool IsInteracting { get; set; }

    private void Update()
    {
        if (CTRLHub.InteractDown)
            CheckInteraction();

        if (IsInteracting)
            HandledDrop();
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
                    Debug.Log("combine " + CarriedObject.name + " with " + interactableToInteractWith.name);
                    //CarriedObject.Combine(this, interactableToInteractWith);

                    interactableToInteractWith.gameObject.GetComponent<BaseInteractable>().Combine(CarriedObject.gameObject);
                }
                else if (interactableToInteractWith.gameObject.GetComponent<FlashbackInteraction>() == null)
                {
                    interactableToInteractWith.Interact(this);
                }
                else
                {
                    interactableToInteractWith.gameObject.GetComponent<FlashbackInteraction>().Interact(this);
                }
            }
        }
        else
        {
            if (IsCarrying)
            {
                CarriedObject.gameObject.GetComponent<BaseInteractable>().Use();
            }
        }
    }

    private void HandledDrop()
    {
        if (CTRLHub.ThrowUp)
        {
            UsedObject.PutDown(this);
        }
    }
}

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
    [SerializeField]
    [Tooltip("Hand the carried object is parented to")]
    private Transform grabingPoint;

    private ObjectInteraction usedObject;
    public ObjectInteraction UsedObject
    {
        get { return usedObject; }
        set
        {
            usedObject = value;
            IsInteracting = (value == null ? false : true);
            IsPulling = (value == null ? false : true);
        }
    }

    public bool IsInteracting { get; set; }
    public Transform GrabingPoint { get { return grabingPoint; } }

    public bool IsPulling { get; set; }

    private void Update()
    {
        if (CTRLHub.InteractDown)
            CheckInteraction();

        if (IsInteracting)
            HandledDrop();
    }

    public static InteractionScript Get()
    {
        return GameObject.Find("Player").GetComponent<InteractionScript>();
    }

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
                }
                else
                {
                    interactableToInteractWith.Interact(this);
                }
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

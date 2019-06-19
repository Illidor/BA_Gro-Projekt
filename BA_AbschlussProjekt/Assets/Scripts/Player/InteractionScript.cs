using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] [Tooltip("Max distance to objects the player is able to grab empty handed")]
    private float emptyHandedGrabingReach = 1.5f;

    [field: Space, 
        LabelOverride("Grabing Point"), SerializeField, Tooltip("Hand the carried object is parented to")]
    public Transform GrabingPoint { get; private set; }
    [SerializeField] [Tooltip("Handler of the players injuries")]
    private PlayerHealth playerHealth;

    [field: LabelOverride("GUI Interaction Feedback Handler"), SerializeField, 
        Tooltip("The GUI Interaction Feedback Handler of the player. If not supplied the script will search on this gameobject and it's children")]
    public GUIInteractionFeedbackHandler GUIInteractionFeedbackHandler { get; private set; }

    public PlayerHealth PlayerHealth { get { return playerHealth; } }

    private GrabInteractable UsedObject { get; set; }

    public bool IsCarrying { get; private set; }
    public bool IsPushing  { get; private set; }

    private float grabingReach;

    protected void Awake()
    {
        grabingReach = emptyHandedGrabingReach;

        if (GUIInteractionFeedbackHandler == null)
            GUIInteractionFeedbackHandler = GetComponentInChildren<GUIInteractionFeedbackHandler>();
    }

    protected void Update()
    {
        HandleActions();

        if (IsCarrying || IsPushing)
        {
            if (CTRLHub.DropUp)
                UsedObject.PutDown(this);
        }
    }

    private void HandleActions()
    {
        GUIInteractionFeedbackHandler.ResetGUI();

        Ray screenCenterRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit raycastHit;
        bool didRaycastHit = Physics.Raycast(screenCenterRay, out raycastHit, grabingReach);

        if (IsCarrying == false)
        {
            raycastHit.collider?.GetComponent<BaseInteractable>()?.HandleInteraction(this);
        }
        else
        {
            if (didRaycastHit)
            {
                if (raycastHit.collider.GetComponent<ICombinable>()?.HandleCombine(this, UsedObject) == true)
                    return;
            }

            UsedObject?.GetComponent<IUseable>()?.HandleUse(this);
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

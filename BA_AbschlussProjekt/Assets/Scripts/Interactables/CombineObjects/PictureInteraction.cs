using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInteraction : InteractionFoundation, ICombinable
{
    [SerializeField]
    private GameObject objectToInteract;
    [Space]
    [SerializeField]
    private GameObject pictureOnStand;

    public bool IsPictureOnStand { get; private set; }

    private new void Awake()
    {
        pictureOnStand?.SetActive(false);

        IsPictureOnStand = false;

        base.Awake();
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent is Picture picture)
        {
            picture.PutDown(player);

            // the following is the old method. I now put a picture duplicate there to be activated.
            //interactingComponent.GetComponent<Rigidbody>().isKinematic = true;
            //interactingComponent.GetComponent<Collider>().enabled = false;
            //interactingComponent.transform.SetParent(transform);
            //interactingComponent.transform.localPosition = Vector3.zero;
            //interactingComponent.transform.localEulerAngles = Vector3.zero;

            pictureOnStand?.SetActive(true);
            Destroy(interactingComponent.gameObject);

            GetComponent<Collider>().enabled = false;

            IsPictureOnStand = true;

            return true;
        }
        return false;
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

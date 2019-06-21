using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInteraction : InteractionFoundation, ICombinable
{
    private new MeshRenderer renderer;
    public MeshRenderer rendererCanvas;

    public GameObject objectToInteract;

    public GameObject boxToOpen;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
        rendererCanvas.enabled = false;
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteract.name)
        {
            ((GrabInteractable)interactingComponent).PutDown(player);
            interactingComponent.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            interactingComponent.transform.SetParent(transform);
            interactingComponent.transform.localPosition = Vector3.zero;
            interactingComponent.transform.localEulerAngles = Vector3.zero;
            rendererCanvas.enabled = true;
            boxToOpen.transform.Rotate(55f, 0, 0);
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

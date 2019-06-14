using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInteraction : MonoBehaviour, ICombinable
{
    private new MeshRenderer renderer;

    public GameObject objectToInteract;

    public GameObject boxToOpen;

    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
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
            boxToOpen.GetComponent<Animation>().Play();
            return true;
        }
        return false;
    }
}

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
            renderer.enabled = true;
            Destroy(interactingComponent.gameObject);
            boxToOpen.GetComponent<Animation>().Play();
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInteraction : MonoBehaviour, ICombinable
{
    private new MeshRenderer renderer;

    public GameObject objectToInteract;

    public GameObject objectToSpawn;

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
            Instantiate(objectToSpawn);
            return true;
        }
        return false;
    }
}

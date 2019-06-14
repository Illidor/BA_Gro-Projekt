using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInteraction : BaseInteractable
{
    private MeshRenderer renderer;

    public GameObject objectToInteract;

    public GameObject objectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Combine(GameObject otherGameObject)
    {
        if(otherGameObject.name == objectToInteract.name)
        {
            renderer.enabled = true;
            otherGameObject.GetComponent<Carryable>().PutDown(InteractionScript.Get());
            Destroy(otherGameObject);
            GameObject gO = Instantiate(objectToSpawn);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Interact(InteractionScript interactionScript)
    {
        return false;
    }

    public override bool Use()
    {
        return false;
    }

    
}

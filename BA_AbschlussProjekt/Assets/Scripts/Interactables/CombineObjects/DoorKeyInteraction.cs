using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : BaseInteractable
{
    [SerializeField] GameObject keyInLock;
    [SerializeField] GameObject objectToInteractWith;

    public override bool Combine(GameObject otherGameObject)
    {
        if(otherGameObject.name == objectToInteractWith.name)
        {
            keyInLock.SetActive(true);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool Interact(InteractionScript interactionScript)
    {
        throw new System.NotImplementedException();
    }

    public override bool Use()
    {
        throw new System.NotImplementedException();
    }
}

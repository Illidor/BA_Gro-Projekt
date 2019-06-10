using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : BaseInteractable
{
    [SerializeField] GameObject keyInLock;
    [SerializeField] string objectToInteractWith;

    public override bool Combine(GameObject otherGameObject)
    {
        if(otherGameObject.name == objectToInteractWith)
        {
            keyInLock.SetActive(true);
            GetComponent<Animator>().SetTrigger("open");
            Destroy(otherGameObject);
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

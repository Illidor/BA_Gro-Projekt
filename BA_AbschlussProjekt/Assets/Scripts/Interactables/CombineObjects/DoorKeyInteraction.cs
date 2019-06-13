using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : MonoBehaviour, ICombinable        // I see similarities with PictureInteraction
{
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string objectToInteractWith;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteractWith)
        {
            keyInLock.SetActive(true);
            GetComponent<Animator>().SetTrigger("open");
            Destroy(interactingComponent.gameObject);
            return true;
        }
        return false;
    }
}

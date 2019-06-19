using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKeyInteraction : NamedObject, ICombinable        // I implemented ICombinable because there was a combine method ~Seb
{
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string objectToInteractWith;

    protected AudioManager audioManager;
    [SerializeField]
    protected string interactSound;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if(interactingComponent.name == objectToInteractWith)
        {
            keyInLock.SetActive(true);
            
            GetComponent<Animator>().SetTrigger("open");
            ((GrabInteractable)interactingComponent).PutDown(player);
            Destroy(interactingComponent.gameObject);
            return true;
        }
        return false;
    }
    protected void PlaySound(string soundType)
    {
        if (GetComponent<AudioSource>() == null)
        {
            audioManager.AddSound(soundType, this.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + DisplayName + " with " + currentlyHolding.DisplayName; //Inherit from NamedObject to have access to DisplayName

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

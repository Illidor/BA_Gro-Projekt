using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class Book : GrabInteractable, IUseable
{
    [SerializeField] GameObject bookToRead;

    private bool isBookOpened = false;

    protected new void Awake()
    {
        textToDisplayOnHover = "Click to pick up " + DisplayName;
        base.Awake();
    }

    public void HandleUse(InteractionScript player)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to use " + DisplayName; //Inherit from InteractionFoundation (or BaseInteractable) to have access to DisplayName

        if (CTRLHub.InteractDown)
            Use(player);
    }

    public bool Use(InteractionScript player)
    {
        if (!isBookOpened)
        {
            bookToRead.SetActive(true);
            isBookOpened = true;

            return true;
        }
        else
        {
            bookToRead.SetActive(false);
            isBookOpened = false;
        }
        return true;
    }
}

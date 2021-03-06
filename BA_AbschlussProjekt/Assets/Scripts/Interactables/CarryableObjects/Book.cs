﻿using UnityEngine;
using System;

[Obsolete]
public class Book : GrabInteractable
{
    [SerializeField] [Tooltip("Propably named BookToInteract")]
    GameObject bookToRead;

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

    // Method use: Not in use. Removed usable on Book, reading by ui is unimmersive 
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

    public override void PutDown(InteractionScript player)
    {
        bookToRead.SetActive(false);
        isBookOpened = false;

        base.PutDown(player);
    }
}

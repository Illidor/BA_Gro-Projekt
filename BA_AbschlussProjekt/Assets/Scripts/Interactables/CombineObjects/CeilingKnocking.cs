﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingKnocking : InteractionFoundation, ICombinable
{
    [SerializeField]
    private float timeDelayBetweenKnocksInSeconds = 0.5f;
    [SerializeField]
    private HatchInteraction hatch;
    [SerializeField]
    private Sound soundOnKnock;

    private float timeOfLastKnock;

    private new void Awake()
    {
        if (soundOnKnock == null)
            soundOnKnock = GetComponentInChildren<Sound>();

        base.Awake();
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crutch &&
           (Time.time - timeOfLastKnock) > timeDelayBetweenKnocksInSeconds)
        {
            timeOfLastKnock = Time.time;

            soundOnKnock?.PlaySound(0);

            hatch?.EmitDust(10);

            return true;
        }

        return false;
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        // Task did not say display text or symbol, just change cursor
        //player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName; 

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

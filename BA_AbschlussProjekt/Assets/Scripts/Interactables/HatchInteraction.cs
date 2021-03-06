﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : InteractionFoundation, ICombinable
{
    public List<GameObject> correlatingGameObjects;

    private Sound hatchOpenSound;

    private void Start()
    {
        hatchOpenSound = GetComponent<Sound>();
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        try
        {
            GetComponent<Animator>().SetTrigger("open");
            hatchOpenSound.PlaySound(0);
            //AudioManager.audioManager.Play("snd_openattic_ladder");
        }
        catch (System.Exception) { }

        foreach (GameObject cgO in correlatingGameObjects)
        {
            try
            {
                cgO.GetComponent<Animator>().SetTrigger("open");
                //hatchOpenSound.playSound(0);
            }
            catch (System.Exception) { }
        }

        return true;
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        Debug.Log("handle combine hatch");
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

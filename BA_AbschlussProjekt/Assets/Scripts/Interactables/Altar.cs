﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : ConditionedInteraction
{
    [SerializeField]
    private Transform triggerObject;
    [SerializeField]
    private KeyBox keyBox;
    [SerializeField]
    private PictureInteraction pictureInteraction;

    private new void Awake()
    {
        if (pictureInteraction == null)
            pictureInteraction = GetComponentInChildren<PictureInteraction>();

        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (keyBox.IsOpen == false)
            keyBox.OpenKeyBox();

        return keyBox.IsOpen;
    }

    public bool CarryOutSecondInteract(InteractionScript interactionScript)
    {
        if (triggerObject.childCount > 0)
        {
            VoiceLines.instance.PlayDillenVoiceLine(1, 2);
            Picture.InvokePlayerFailed();
            return true;
        }
        return false;
    }

    public override void HandleInteraction(InteractionScript player)
    {
        if (pictureInteraction.IsPictureOnStand == false)
            return;

        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Press E to refuse praying";
        player.GUIInteractionFeedbackHandler.SecondActionDescription.text = "Click to Pray at " + DisplayName;

        if (CTRLHub.InteractDown && player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) > minCondition)
        {
            CarryOutInteraction(player);
        }
        else if (CTRLHub.SecondInteractDown && player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) > minCondition)
        {
            CarryOutSecondInteract(player);
        }
    }
}

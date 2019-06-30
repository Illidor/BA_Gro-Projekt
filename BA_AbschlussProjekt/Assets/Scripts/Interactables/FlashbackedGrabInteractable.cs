using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashbackedGrabInteractable : GrabInteractable
{
    [SerializeField]
    private Sprite flashbackSprite;
    [SerializeField]
    private Image image;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        StartCoroutine(HandleFlashback(player));
        return true;
    }

    private IEnumerator HandleFlashback(InteractionScript player)
    {
        player.IsFrozen = true;
        player.GUIInteractionFeedbackHandler.RemoveGUI();

        image.enabled = true;
        image.sprite = flashbackSprite;

        while (CTRLHub.Drop == false)
            yield return new WaitForEndOfFrame();

        while (CTRLHub.Drop)
            yield return new WaitForEndOfFrame();

        image.enabled = false;
        player.IsFrozen = false;

        base.CarryOutInteraction(player);
    }


}

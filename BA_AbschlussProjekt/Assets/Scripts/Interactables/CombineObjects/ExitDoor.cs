using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ExitDoor : BaseInteractable, ICombinable   
{
    [SerializeField]
    private float timeToWaitUntilDoorOpens = 1.65f;
    [SerializeField]
    GameObject keyInLock;
    [SerializeField]
    string nameOfKeysGameobject = "Key";
    private int interactionCount = 0;

    [SerializeField]
    protected Sound interactSound;
    [SerializeField]
    protected Sound doorOpeningSound;

    private bool isOpen = false;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crowbar crowbar)
            crowbar.Break(player);


        if (interactingComponent.name == nameOfKeysGameobject)
        {
            keyInLock.SetActive(true);

            ((GrabInteractable)interactingComponent).PutDown(player);
            Destroy(interactingComponent.gameObject);

            interactSound?.PlaySound(0);

            StartCoroutine(OpenDoor(player, interactingComponent));

            isOpen = true;

            return true;
        }
        return false;
    }

    private IEnumerator OpenDoor(InteractionScript player, BaseInteractable interactingComponent)
    {
        yield return new WaitForSeconds(timeToWaitUntilDoorOpens);

        GetComponent<PlayableDirector>().Play();
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (isOpen)
            return false;
        switch (interactionCount)
        {
            case 0:
                interactSound?.PlaySound(1);//rütteln(door_rattle) voiceline(help?), dillen3, voice(iknowthatvoice
                VoiceLines.instance.PlayDillenVoiceLine(3, 1f);
                break;
            case 1:
                interactSound?.PlaySound(2);//hämmern und voiceline, dillen
                break;
            case 2:
                interactSound?.PlaySound(3);//hämmern, verletzen
                player.PlayerHealth.ChangeCondition(Conditions.UpperBodyCondition, 0.5f);
                break;
            default:
                VoiceLines.instance.PlayVoiceLine(UnityEngine.Random.Range(10, 13), 0f);
                break;
        }
        interactionCount++;
        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : BaseInteractable
{
    [SerializeField]
    private Transform triggerObject;

    public override bool Interact(InteractionScript player, Conditions condition, float minCondition)
    {
        //TODO: implement praying 
        if (GetComponent<Animation>().isPlaying == false)
        {
            GetComponent<Animation>().Play();
            return true;
        }
        return false;
    }

    public bool SecondInteract(InteractionScript interactionScript)
    {
        if (triggerObject.childCount > 0)
        {
            Destroy(triggerObject.GetChild(0).gameObject);
        }
        //foreach (Transform item in GetComponentInChildren<Transform>())
        //{
        //    Destroy(item.gameObject);
        //}
        //Destroy(gameObject);
        return true;
    }

    public override void HandleInteraction(InteractionScript player, Conditions condition)
    {

        //player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        //player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        //player.GUIInteractionFeedbackHandler.ActionDescription.text = "Press E to Destroy " + DisplayName;


        if(triggerObject.childCount > 0)
        {
            player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
            player.GUIInteractionFeedbackHandler.ActionDescription.text = "Press E to Destroy " + triggerObject.GetChild(0).GetComponent<Picture>().DisplayName;
            player.GUIInteractionFeedbackHandler.SecondActionDescription.text = "Click to Pray at " + DisplayName;
            if (CTRLHub.InteractDown)
            {
                Interact(player, condition, minCondition);
            }
            else if (CTRLHub.SecondInteractDown)
                SecondInteract(player);
        }
        //else if (CTRLHub.SecondInteractDown)
        //    SecondInteract(player);
    }
}

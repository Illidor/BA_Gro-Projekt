using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : NamedObject, ICombinable
{
    public List<BaseInteractable> thingsToInteractWtih;
    public List<GameObject> correlatingGameObjects;

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        Debug.Log("Hatch Combine");
        foreach (BaseInteractable interactable in thingsToInteractWtih)
        {
            if(interactingComponent.name == interactable.name)
            {
                try
                {
                    this.gameObject.GetComponent<Animation>().Play();
                    //AudioManager.audioManager.Play("snd_openattic_ladder");
                }
                catch (System.Exception){}



                foreach (GameObject cgO in correlatingGameObjects)
                {
                    try
                    {
                        cgO.GetComponent<Animation>().Play();
                    }
                    catch (System.Exception) { }
                }

            }
            else
            {
                Debug.Log("Wrong GO");
            }
        }

        return false;
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + DisplayName + " with " + currentlyHolding.DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : InteractionFoundation, ICombinable
{
    public List<BaseInteractable> thingsToInteractWtih;
    public List<GameObject> correlatingGameObjects;

    private Sound hatchOpenSound;

    private void Start() {
        hatchOpenSound = GetComponent<Sound>();
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        Debug.Log("Hatch Combine");
        foreach (BaseInteractable interactable in thingsToInteractWtih)
        {
            if(interactingComponent.name == interactable.name)
            {
                try
                {
                    this.GetComponent<Animator>().SetTrigger("open"); ;
                    hatchOpenSound.PlaySound(0);
                    //AudioManager.audioManager.Play("snd_openattic_ladder");
                }
                catch (System.Exception){}



                foreach (GameObject cgO in correlatingGameObjects)
                {
                    try
                    {
                        cgO.GetComponent<Animator>().SetTrigger("open");
                        //hatchOpenSound.playSound(0);
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
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

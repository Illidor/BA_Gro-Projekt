using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchLever : InteractionFoundation, ICombinable
{
    [SerializeField]
    private LevelCombinatables[] levelCombinables;

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {

        return false;
    }



    [Serializable]
    private class LevelCombinatables
    {
#if UNITY_EDITOR
        [HideInInspector]
        public string name;
#endif
        public GrabInteractable objectToCombineWith;
        public GameObject corespondingObjectToDisplay;
    }

    private void OnValidate()
    {
        foreach (LevelCombinatables levelCombinatable in levelCombinables)
        {
            levelCombinatable.name = "LevelCombinatable";
        }
    }
}


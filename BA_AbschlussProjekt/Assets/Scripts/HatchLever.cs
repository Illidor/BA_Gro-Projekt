using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchLever : BaseInteractable, ICombinable
{
    [SerializeField]
    private HatchInteraction hatch;
    [SerializeField]
    private LevelCombinatables[] levelCombinables;
    [SerializeField]
    private HingeJoint hinge;
    [SerializeField]
    private Sound soundToPlayOnLeverFlip;

    public bool IsLeverDown { get; private set; }

    private bool hasLever = false;
    private bool alreadyOpend = false;

    protected void Start()
    {
        hinge.transform.localRotation = Quaternion.Euler(hinge.limits.min, 0, 0);

        for (int i = 0; i < levelCombinables.Length; i++)
            levelCombinables[ i ].corespondingObjectToDisplay.SetActive(false);

        soundToPlayOnLeverFlip = GetComponent<Sound>();
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

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        for (int i = 0; i < levelCombinables.Length; i++)
        {
            if (levelCombinables[ i ].objectToCombineWith.Equals(interactingComponent))
            {
                levelCombinables[ i ].corespondingObjectToDisplay.SetActive(true);
                hasLever = true;

                player.StopUsingObject();
                Destroy(interactingComponent.gameObject);

                return true;
            }
        }
        return false;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (hasLever == false)
            return false;

        if (IsLeverDown)
        {
            hinge.transform.localRotation = Quaternion.Euler(hinge.limits.min, 0, 0);
        }
        else
        {
            hinge.transform.localRotation = Quaternion.Euler(hinge.limits.max, 0, 0);

            if (!alreadyOpend)
            {
                hatch.Combine(player, this);
                alreadyOpend = true;
            }
        }

        soundToPlayOnLeverFlip?.PlaySound(0);

        IsLeverDown = !IsLeverDown;

        return true;
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


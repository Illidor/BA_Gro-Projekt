using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HatchLever : BaseInteractable, ICombinable
{
    public static event UnityAction<string> UseLeverAnimation;

    [SerializeField]
    private HatchInteraction hatch;
    [SerializeField]
    private LevelCombinatables[] levelCombinables;
    [SerializeField]
    private HingeJoint hinge;
    [SerializeField]
    private Sound soundToPlayOnLeverFlip;
    [SerializeField]
    private Sound soundToPlayOnLeverCraft;

    public bool IsLeverDown { get; private set; }

    private bool hasLever = false;
    private bool alreadyOpend = false;

    protected void Start()
    {
        hinge.transform.localRotation = Quaternion.Euler(hinge.limits.min, 0, 0);

        for (int i = 0; i < levelCombinables.Length; i++)
            levelCombinables[ i ].corespondingObjectToDisplay.SetActive(false);

        soundToPlayOnLeverFlip = GetComponent<Sound>();

        Sound[] allSoundComponents = GetComponents<Sound>();
        if (allSoundComponents.Length >= 1)
            soundToPlayOnLeverCraft = allSoundComponents[ allSoundComponents.Length - 1 ];


        soundToPlayOnLeverCraft = GetComponents<Sound>().GetLast();
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

    public void AddCombinableReference(GrabInteractable grabInteractable, int index) {
        levelCombinables[index].objectToCombineWith = grabInteractable;
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

                soundToPlayOnLeverCraft?.PlaySound(0);

                return true;
            }
        }
        return false;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (hasLever == false || alreadyOpend)
            return false;

        if (IsLeverDown)
        {
            hinge.transform.localRotation = Quaternion.Euler(hinge.limits.min, 0, 0);
        }
        else
        {
            if (!alreadyOpend)
            {
                alreadyOpend = true;
                UseLeverAnimation?.Invoke("GrabMid");
                StartCoroutine(DelayHatchUseage());
            }
        }

        soundToPlayOnLeverFlip?.PlaySound(0);

        IsLeverDown = !IsLeverDown;

        return true;
    }

    private IEnumerator DelayHatchUseage()
    {
        yield return new WaitForSeconds(0.25f);
        hinge.transform.localRotation = Quaternion.Euler(hinge.limits.max, 0, 0);
        hatch.OpenHatch();
    }

    [Serializable]
    private class LevelCombinatables
    {
        [HideInInspector]
        public string name;
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


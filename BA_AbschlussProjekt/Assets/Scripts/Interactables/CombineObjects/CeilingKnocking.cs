using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingKnocking : InteractionFoundation, ICombinable
{
    [SerializeField]
    private float timeDelayBetweenKnocksInSeconds = 0.5f;
    [SerializeField]
    private HatchInteraction hatch;
    [SerializeField]
    private Sound soundOnKnock;

    Coroutine cR;
    [SerializeField] private float animationOffset;
    [SerializeField] private float animationSpeed;

    private float timeOfLastKnock;

    private new void Awake()
    {
        if (soundOnKnock == null)
            soundOnKnock = GetComponentInChildren<Sound>();

        base.Awake();
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crutch)
        {
            if (cR == null)
            {
                cR = StartCoroutine(KnockAnim(player, interactingComponent));
            }

            if (Time.time - timeOfLastKnock > timeDelayBetweenKnocksInSeconds)
            {
                timeOfLastKnock = Time.time;

                soundOnKnock?.PlaySound(0);

                hatch?.EmitDust(10);

                return true;
            }
        }

        return false;
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        // Task did not say display text or symbol, just change cursor
        //player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName; 

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }

    public IEnumerator KnockAnim(InteractionScript player, BaseInteractable interactingComponent)
    {

        while ((gameObject.transform.position.y - interactingComponent.gameObject.transform.position.y) > animationOffset)
        {
            Debug.Log("PRE::" + (gameObject.transform.position.y - interactingComponent.gameObject.transform.position.y));

            player.HandIKRight.position = Vector3.MoveTowards(player.HandIKRight.position, interactingComponent.gameObject.transform.position, animationSpeed);
            player.HandIKRight.rotation = Quaternion.Lerp(player.HandIKRight.rotation, interactingComponent.gameObject.transform.rotation, animationSpeed);


            interactingComponent.gameObject.transform.position = Vector3.MoveTowards(interactingComponent.gameObject.transform.position, new Vector3
                                                                                                                    (interactingComponent.gameObject.transform.position.x,
                                                                                                                    gameObject.transform.position.y,
                                                                                                                    interactingComponent.gameObject.transform.position.z), animationSpeed);

            //interactingComponent.gameObject.transform.LookAt(new Vector3(interactingComponent.gameObject.transform.position.x, gameObject.transform.position.y, interactingComponent.gameObject.transform.position.z));

            yield return new WaitForEndOfFrame();
        }


        StartCoroutine(player.IKToObject(interactingComponent, false));

        yield return new WaitForEndOfFrame();
        cR = null;
    }
}

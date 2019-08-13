using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Altar : ConditionedInteraction
{
    [SerializeField]
    private Transform triggerObject;
    [SerializeField]
    private KeyBox keyBox;
    [SerializeField]
    private PictureInteraction pictureInteraction;
    private RigidbodyFirstPersonController rigidbodyFPSController;
    private Animator playerAnimator;

    private int refusingCounter = 0;

    private new void Awake()
    {
        if (pictureInteraction == null)
            pictureInteraction = GetComponentInChildren<PictureInteraction>();

        rigidbodyFPSController = GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>();
        playerAnimator = rigidbodyFPSController.gameObject.GetComponentInChildren<Animator>();

        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        StartCoroutine(OpenKeyBox());

        return keyBox.IsOpen;
    }

    private IEnumerator OpenKeyBox()
    {
        yield return new WaitForSeconds(4.5f);
        if (keyBox.IsOpen == false)
            keyBox.OpenKeyBox();

        rigidbodyFPSController.freezePlayerCamera = true;
        rigidbodyFPSController.freezePlayerMovement = true;
    }

    public bool CarryOutSecondInteract(InteractionScript interactionScript)
    {
        if (triggerObject.childCount > 0)
        {
            switch (refusingCounter++)
            {
                case 0: 
                    VoiceLines.instance.PlayDillenVoiceLine(1, 2);
                    break;

                case 1:
                    VoiceLines.instance.PlayDillenVoiceLine(2, 2);
                    break;

                default:
                    Picture.InvokePlayerFailed();
                    break;
            }

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
            rigidbodyFPSController.freezePlayerCamera = true;
            rigidbodyFPSController.freezePlayerMovement = true;

            playerAnimator.SetTrigger("Pray");
            CarryOutInteraction(player);
            
        }
        else if (CTRLHub.SecondInteractDown && player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) > minCondition)
        {
            CarryOutSecondInteract(player);
        }
    }
}

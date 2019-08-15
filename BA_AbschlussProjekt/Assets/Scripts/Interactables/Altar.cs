using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class Altar : ConditionedInteraction
{
    public static event UnityAction<Transform> MovePlayerToAltar;
    public static event UnityAction ShockPlayerToDeath;

    [SerializeField]
    private Transform triggerObject;
    [SerializeField]
    private KeyBox keyBox;
    [SerializeField]
    private PictureInteraction pictureInteraction;
    private RigidbodyFirstPersonController rigidbodyFPSController;
    private Animator playerAnimator;
    [SerializeField] Transform rageAnimStartTransform;

    [Space]
    [SerializeField] Rigidbody candleOne;
    [SerializeField] Rigidbody candleTwo;
    [SerializeField] GameObject woodStand;
    [SerializeField] Rigidbody drawing;
    [SerializeField] Rigidbody picture;

    private int refusingCounter = 0;
    private bool hasPrayed = false;
    private bool isAltarDestroyed = false;

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

        if (CTRLHub.InteractDown && player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) > minCondition && hasPrayed == false)
        {
            hasPrayed = true;
            rigidbodyFPSController.freezePlayerCamera = true;
            rigidbodyFPSController.freezePlayerMovement = true;

            VoiceLines.instance.PlayVoiceLine(21, 0f);
            VoiceLines.instance.PlayDillenVoiceLine(13, 7.2f);

            VoiceLines.instance.PlayDillenVoiceLine(14, 16f);

            playerAnimator.SetTrigger("Pray");
            StartCoroutine(StopPrayingAnimation());
            CarryOutInteraction(player);
            
        }
        else if (CTRLHub.SecondInteractDown && player.PlayerHealth.GetCondition(conditionsTypeNeededToInteract) > minCondition && isAltarDestroyed == false)
        {
            isAltarDestroyed = true;
            rigidbodyFPSController.freezePlayerMovement = true;

            MovePlayerToAltar?.Invoke(rageAnimStartTransform);

            playerAnimator.SetTrigger("Rage");
            CarryOutSecondInteract(player);
        }
    }

    private void DestroyAltar()
    {
        candleOne.AddForce((Vector3.right + Vector3.up / 2f) * 6f, ForceMode.Impulse);

        picture.isKinematic = false;
        picture.AddForce((Vector3.right + Vector3.up / 3f) * 6f, ForceMode.Impulse);

        Rigidbody woodStandRb = woodStand.AddComponent<Rigidbody>();
        woodStandRb.useGravity = true;
        woodStandRb.AddForce((Vector3.right + Vector3.up / 3f) * 6f, ForceMode.Impulse);

        candleTwo.AddForce((Vector3.right + Vector3.up / 2f) * 6f, ForceMode.Impulse);

        drawing.isKinematic = false;
        drawing.useGravity = true;
        drawing.AddForce((Vector3.right + Vector3.up / 2f) * 6f, ForceMode.Impulse);

        StartCoroutine(DelayedDeathShockAfterDestroyingAltar());
    }

    private IEnumerator DelayedDeathShockAfterDestroyingAltar()
    {
        yield return new WaitForSeconds(1f);
        ShockPlayerToDeath?.Invoke();
    }

    private IEnumerator StopPrayingAnimation()
    {
        yield return new WaitForSeconds(8f);
        playerAnimator.SetTrigger("StopPraying");
    }

    private void OnEnable()
    {
        PlayerAnimationEvents.PlayerDestroyAltar += DestroyAltar;
    }

    private void OnDisable()
    {
        PlayerAnimationEvents.PlayerDestroyAltar -= DestroyAltar;
    }
}

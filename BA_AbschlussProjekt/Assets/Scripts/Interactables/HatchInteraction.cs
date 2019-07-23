using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : BaseInteractable, ICombinable
{
    [SerializeField]
    private float timeDelayBetweenKnocksInSeconds = 1;
    [SerializeField]
    private int knockingCountToUnlock = 2;
    [SerializeField]
    ParticleSystem dustPs;
    [SerializeField]
    Sound dustParticleSound;
    [SerializeField] [Tooltip("will automatically use first on gameobject")]
    Sound hatchOpenSound;
    [SerializeField] [Tooltip("will automatically use last on gameobject")]
    Sound knockingSound;

    Coroutine cR;
    [SerializeField] private float animationOffset;
    [SerializeField] private float animationSpeed;


    private float dustParticleTicker = 0f;
    private float dustParticleThreshold = 30f;
    private bool isEmitting = true;

    private int knockCounter = 0;

    private float timeOfLastKnock;

    private void Start()
    {
        if (hatchOpenSound == null)
            hatchOpenSound = GetComponent<Sound>();

        if (knockingSound == null)
            knockingSound = GetComponents<Sound>().GetLast();
    }

    private void Update()
    {
        dustParticleTicker += Time.deltaTime;

        if (dustParticleTicker > dustParticleThreshold && isEmitting)
        {
            dustParticleTicker = 0f;
            EmitDust(10);
        }
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crutch)
        {
            if (cR == null)
            {
                cR = StartCoroutine(KnockAnim(interactingComponent));
            }
        }

        return HandleKnockLogicAndOpening();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        return HandleKnockLogicAndOpening();
    }

    private bool HandleKnockLogicAndOpening()
    {
        knockingSound?.PlaySound(0);

        if (knockCounter < knockingCountToUnlock - 1 &&
            (Time.time - timeOfLastKnock) > timeDelayBetweenKnocksInSeconds)
        {

            timeOfLastKnock = Time.time;
            knockCounter++;

            EmitDust(10);

            return false;
        }

        OpenHatch();

        return true;
    }

    public void EmitDust(int emittingStrength)
    {
        dustPs.Emit(emittingStrength);
        dustParticleSound.PlaySound(0);
    }

    public void OpenHatch()
    {
        transform.parent?.GetComponent<Animator>()?.SetTrigger("open");
        hatchOpenSound?.PlaySound(0);
        EmitDust(10);
        isEmitting = false;
        StartCoroutine(DelayDustEffect(0.25f));
    }

    private IEnumerator DelayDustEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        EmitDust(40);
    }
 
    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        if ((Time.time - timeOfLastKnock) < timeDelayBetweenKnocksInSeconds)
            return false;

        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }

    public IEnumerator KnockAnim(BaseInteractable interactingComponent)
    {
        InteractionScript characterInteractionScript = GameObject.FindObjectOfType<InteractionScript>();

        while ((gameObject.transform.position.y - interactingComponent.gameObject.transform.position.y) > animationOffset)
        {
            Debug.Log("PRE::" + (gameObject.transform.position.y - interactingComponent.gameObject.transform.position.y));

            characterInteractionScript.HandIKRight.position = Vector3.MoveTowards(characterInteractionScript.HandIKRight.position, interactingComponent.gameObject.transform.position, animationSpeed);
            characterInteractionScript.HandIKRight.rotation = Quaternion.Lerp(characterInteractionScript.HandIKRight.rotation, interactingComponent.gameObject.transform.rotation, animationSpeed);


            interactingComponent.gameObject.transform.position = Vector3.MoveTowards(interactingComponent.gameObject.transform.position, new Vector3
                                                                                                                    (interactingComponent.gameObject.transform.position.x,
                                                                                                                    gameObject.transform.position.y,
                                                                                                                    interactingComponent.gameObject.transform.position.z), animationSpeed);

            //interactingComponent.gameObject.transform.LookAt(new Vector3(interactingComponent.gameObject.transform.position.x, gameObject.transform.position.y, interactingComponent.gameObject.transform.position.z));

            yield return new WaitForEndOfFrame();
        }


        StartCoroutine(characterInteractionScript.IKToObject(interactingComponent, false));

        yield return new WaitForEndOfFrame();
        cR = null;
    }
}

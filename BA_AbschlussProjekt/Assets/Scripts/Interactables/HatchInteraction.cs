using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : InteractionFoundation, ICombinable
{
    public List<GameObject> correlatingGameObjects;

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
            dustPs.Emit(10);
            dustParticleSound.PlaySound(0);
        }
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        knockingSound?.PlaySound(0);

        if(interactingComponent is Crutch)
        {
            if (cR == null)
            {
                cR = StartCoroutine(KnockAnim(interactingComponent));
            }
        }


        if (knockCounter < knockingCountToUnlock - 1 &&
            (Time.time - timeOfLastKnock) > timeDelayBetweenKnocksInSeconds)
        {

            timeOfLastKnock = Time.time;
            knockCounter++;

            dustPs.Emit(10);
            dustParticleSound.PlaySound(0);

            return false;
        }

        OpenHatch();

        return true;
    }

    public void OpenHatch()
    {
        try
        {
            GetComponent<Animator>().SetTrigger("open");
            hatchOpenSound?.PlaySound(0);
            dustParticleSound?.PlaySound(0);
            isEmitting = false;
            StartCoroutine(DelayDustEffect(0.25f));
            //AudioManager.audioManager.Play("snd_openattic_ladder");
        }
        catch (System.Exception) { }

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

    private IEnumerator DelayDustEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        dustParticleSound?.PlaySound(0);
        dustPs.Emit(40);
    }
 
    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        if ((Time.time - timeOfLastKnock) < timeDelayBetweenKnocksInSeconds)
            return false;

        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(   false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true );

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

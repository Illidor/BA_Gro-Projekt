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
        try
        {
            transform.parent.GetComponent<Animator>().SetTrigger("open");
            hatchOpenSound?.PlaySound(0);
            EmitDust(10);
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
        EmitDust(40);
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
}

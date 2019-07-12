using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchInteraction : InteractionFoundation, ICombinable
{
    public List<GameObject> correlatingGameObjects;

    [SerializeField]
    ParticleSystem dustPs;
    [SerializeField]
    Sound dustParticleSound;
    [SerializeField] [Tooltip("will automatically use first on gameobject")]
    Sound hatchOpenSound;
    [SerializeField] [Tooltip("will automatically use last on gameobject")]
    Sound knockingSound;

    private int knockingCountToUnlock = 2;
    private float dustParticleTicker = 0f;
    private float dustParticleThreshold = 30f;
    private bool isEmitting = true;

    private int knockCOunter = 0;

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
        if (knockCOunter < knockingCountToUnlock - 1)
        {
            knockCOunter++;
            knockingSound.PlaySound(0);
            return false;
        }

        try
        {
            GetComponent<Animator>().SetTrigger("open");
            hatchOpenSound.PlaySound(0);
            dustParticleSound.PlaySound(0);
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

        return true;
    }

    private IEnumerator DelayDustEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        dustParticleSound.PlaySound(0);
        dustPs.Emit(40);
    }
 
    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(   false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true );

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }
}

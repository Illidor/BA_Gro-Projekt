using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLever : GrabInteractable
    
{
    [SerializeField]
    private bool leverOn = false;
    [SerializeField]
    private float[] lightStrengths;
    [SerializeField]
    private GameObject[] lightsSources;
    [SerializeField]
    private float lightOffStrength;

    private AudioSource switchSound;
    private Animator animator;

    private void Start()
    {
        switchSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        StartCoroutine(ChangeLight(false));
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        leverOn = leverOn == false ? true : false;
        if (leverOn)
        {
            animator.SetTrigger("TurnOn");
            StartCoroutine(ChangeLight(true));
            switchSound.Play();
        }
        else
        {
            animator.SetTrigger("TurnOff");
            StartCoroutine(ChangeLight(false));
            switchSound.Play();
        }
        return true;
    }

    private IEnumerator ChangeLight(bool state)
    {
        yield return new WaitForSeconds(0.6f);
        if (state)
        {
            int i = 0;
            foreach (GameObject light in lightsSources)
            {
                light.GetComponent<Light>().intensity = lightStrengths[i];
                i++;
            }
        }
        else
        {
            foreach (GameObject light in lightsSources)
            {
                light.GetComponent<Light>().intensity = lightOffStrength;
            }
        }
        yield return null;
    }
}

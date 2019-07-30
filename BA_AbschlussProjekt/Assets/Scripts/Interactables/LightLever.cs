using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LightLever : BaseInteractable
{
    [SerializeField]
    private bool leverOn = false;
    [SerializeField]
    private Light[] lightsSources;
    [SerializeField]
    private bool useInitialLightStrengthInstead;
    [SerializeField]
    private float[] lightStrengths;
    [SerializeField]
    private float lightOffStrength;

    private AudioSource switchSound;
    //private Animator animator;
    private PlayableDirector playableDirector;

    private bool isVoicelinePlayed = false;

    private void Start()
    {
        switchSound = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
        playableDirector = GetComponentInChildren<PlayableDirector>();

        if (lightStrengths.Length != lightsSources.Length)  // if lightStrengths wasn't set properly, don't use it
        {
            useInitialLightStrengthInstead = true;
            lightStrengths = new float[ lightsSources.Length ];
        }

        if (useInitialLightStrengthInstead) // use the strength already present on the lights, if set to true
        {
            for (int i = 0; i < lightsSources.Length; i++)
                lightStrengths[ i ] = lightsSources[ i ].intensity;
        }

        StartCoroutine(ChangeLight(false)); // turn lights of on game start
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        leverOn = !leverOn;

        if (leverOn)
        {
            //animator.SetTrigger("TurnOn");
            transform.GetChild(0).localPosition = new Vector3(0, 0.02579773f, 0);
            StartCoroutine(ChangeLight(true));
            switchSound.Play();
        }
        else
        {
            //animator.SetTrigger("TurnOff");
            transform.GetChild(0).localPosition = new Vector3(0, -0.02579773f, 0);
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
            for (int i = 0; i < lightsSources.Length; i++)
                lightsSources[ i ].intensity = lightStrengths[ i ];

            if (isVoicelinePlayed == false)
            {
                if(Random.Range(0,2) >= 1)
                    VoiceLines.instance.PlayVoiceLine(1, 1.3f);
                else
                    VoiceLines.instance.PlayVoiceLine(4, 1.3f);

                isVoicelinePlayed = true;
            }
        }
        else
        {
            foreach (Light light in lightsSources)
                light.intensity = lightOffStrength;
        }
    }
}

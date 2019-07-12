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

    public override bool CarryOutInteraction(InteractionScript player)
    {
        leverOn = leverOn == false ? true : false;
        if (leverOn)
        {
            GetComponent<Animator>().SetTrigger("TurnOn");
            StartCoroutine(ChangeLight(player, true));
        }
        else
        {
            GetComponent<Animator>().SetTrigger("TurnOff");
            StartCoroutine(ChangeLight(player, false));
        }
        return true;
    }

    private IEnumerator ChangeLight(InteractionScript player, bool state)
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

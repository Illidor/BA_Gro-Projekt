using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UVLampScript : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float stateToggleChanceInPercent = 10;
    [SerializeField]
    private float dimmedLightStrength;
    [SerializeField]
    private float dimmedUVStrength;
    [Space]
    [SerializeField]
    private Light UVVisible;
    [SerializeField]
    private Light UVLight;

    private float fullLightStrength;
    private float fullUVStrength;

    private bool isOn;

    private void Awake()
    {
        fullLightStrength = UVVisible.intensity;
        fullUVStrength = UVLight.intensity;
    }

    private void Update()
    {
        int randomNumber = Random.Range(1, 100);
        
        if(randomNumber > (100 - stateToggleChanceInPercent))
        {
            //UVVisible.enabled = !UVLight.enabled;
            //UVLight.enabled = !UVLight.enabled;

            ChangeIntensity(!isOn);
            isOn = !isOn;
        }
    }

    private void ChangeIntensity(bool setToOn)
    {
        if (setToOn)
        {
            UVVisible.intensity = fullLightStrength;
            UVLight.intensity = fullUVStrength;
        }
        else
        {
            UVVisible.intensity = dimmedLightStrength;
            UVLight.intensity = dimmedUVStrength;
        }
    }
}

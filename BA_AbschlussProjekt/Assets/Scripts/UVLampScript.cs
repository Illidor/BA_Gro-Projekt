using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVLampScript : MonoBehaviour
{
    [SerializeField]
    [Range(0, 100)]
    private float stateToggleChanceInPercent = 10;
    [Space]
    [SerializeField]
    private Light UVVisible;
    [SerializeField]
    private Light UVLight;

    private void Update()
    {
        int randomNumber = Random.Range(1, 100);
        
        if(randomNumber > (100 - stateToggleChanceInPercent))
        {
            UVVisible.enabled = !UVLight.enabled;
            UVLight.enabled = !UVLight.enabled;
        }
    }
}

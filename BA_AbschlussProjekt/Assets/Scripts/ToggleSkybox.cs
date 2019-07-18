using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkybox : MonoBehaviour
{
    [SerializeField]
    private Material skyboxMaterial;
    [SerializeField]
    private Texture dayTimeTexture;
    [SerializeField]
    private Texture nightTimeTexture;

    public void SetDaytimeSkybox()
    {
        skyboxMaterial.SetTexture(3, dayTimeTexture);
    }

    public void SetNightTimeSkybox()
    {
        skyboxMaterial.SetTexture(3, nightTimeTexture);
    }

    
}

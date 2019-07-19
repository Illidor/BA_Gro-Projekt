using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSkybox : MonoBehaviour
{
    [SerializeField]
    private Material daytimeSkyboxMaterial;
    [SerializeField]
    private Material nighttimeSkyboxMaterial;

    public void SetDaytimeSkybox()
    {
        RenderSettings.skybox = daytimeSkyboxMaterial;
    }

    public void SetNightTimeSkybox()
    {
        RenderSettings.skybox = nighttimeSkyboxMaterial;
    }


}

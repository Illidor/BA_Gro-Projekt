using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostEffectScript : MonoBehaviour {

    public Material shaderIncludedMaterial;

    void OnRenderImage( RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, shaderIncludedMaterial);
    }
}

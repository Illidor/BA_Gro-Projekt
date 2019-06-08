using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrame : ObjectInteraction
{
    [SerializeField]
    private bool broken = false;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.material.bounciness < 0.6 && !broken)
        {
            Break();
        }
    }
    private void Break()
    {
        Debug.Log("Picture broke");
        broken = true;
        
        //TODO: add a broken frame
    }
}

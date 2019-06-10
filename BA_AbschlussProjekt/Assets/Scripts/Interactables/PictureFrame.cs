using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrame : ObjectInteraction
{
    [SerializeField]
    private bool broken = false;
    [SerializeField]
    private float velocity;
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.material.bounciness < 0.6 && !broken && velocity < -10)
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
    private void FixedUpdate()
    {
        PushPull();
        velocity = rigidbody.velocity.y;
    }
}

using UnityEngine;

public class PictureFrame : GrabInteractable
{
    [SerializeField]
    private bool broken = false;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.material.bounciness < 0.6 && !broken && rigid.velocity.y < -10)
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

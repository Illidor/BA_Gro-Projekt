using UnityEngine;

public class PictureFrame : GrabInteractable
{
    [SerializeField]
    private bool broken = false;

    protected new void Awake()
    {
        textToDisplayOnHover = "Click to pick up " + DisplayName;
        base.Awake();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.material.bounciness < 0.6 && !broken && rigidbody.velocity.y < -10)
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

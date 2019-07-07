using UnityEngine;

public class Crutch : GrabInteractable
{
    [SerializeField]
    private float reachIncreaseOnCarry = 1;

    private bool isMusicPlaying = false;
    [SerializeField] Sound bgMusic;

    protected override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        player.IncreaseReach(reachIncreaseOnCarry);
        GetComponent<Sound>().PlaySound(0);

        if (isMusicPlaying == false)
        {
            isMusicPlaying = true;
            bgMusic.PlaySound(0);
        }

        gameObject.layer = LayerMask.NameToLayer("NoPlayerCollision");
        transform.parent = player.GrabingPoint.transform;
        transform.localPosition = new Vector3(-0.159f, -0.06506f, 0.348f);
        transform.localEulerAngles = new Vector3(119.955f, -349.499f, 345.164f);
        rigidbody.isKinematic = true;
        player.SetCarriedObject(this);
        IsBeeingCarried = true;

        return true;
    }

    public override void PutDown(InteractionScript player)
    {
        player.ResetReachToDefault();
        base.PutDown(player);
        GetComponent<Sound>()?.PlaySound(0);
    }
}

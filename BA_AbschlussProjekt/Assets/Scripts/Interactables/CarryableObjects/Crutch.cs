using UnityEngine;
using System;
using System.Collections;

public class Crutch : GrabInteractable
{
    [SerializeField]
    private float reachIncreaseOnCarry = 1;

    private bool isMusicPlaying = false;

    [SerializeField] GameObject crutchChild;

    protected override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        player.IncreaseReach(reachIncreaseOnCarry);
        GetComponent<Sound>().PlaySound(0);

        if (isMusicPlaying == false)
        {
            isMusicPlaying = true;
        }
        
        //<<<<<<< HEAD
        return base.CarryOutInteraction_Carry(player); 

        /*

        //gameObject.layer = LayerMask.NameToLayer("NoPlayerCollision");
        //transform.parent = player.GrabingPoint.transform;
        //transform.localPosition = new Vector3(-0.159f, -0.06506f, 0.348f);
        //transform.localEulerAngles = new Vector3(119.955f, -349.499f, 345.164f);
        //rigidbody.isKinematic = true;
        //player.SetCarriedObject(this);
        //IsBeeingCarried = true;
        =======
        gameObject.layer = LayerMask.NameToLayer("NoPlayerCollision");
        transform.parent = player.GrabingPoint.transform;
        transform.localPosition = new Vector3(-0.159f, -0.06506f, 0.348f);
        transform.localEulerAngles = new Vector3(119.955f, -349.499f, 345.164f);
        rigidbody.isKinematic = true;
        player.SetCarriedObject(this);
        collider.enabled = false;
        IsBeeingCarried = true;
        >>>>>>> Merge

        return true; */
    }

    public override void PutDown(InteractionScript player)
    {
        player.ResetReachToDefault();
        base.PutDown(player);
        GetComponent<Sound>()?.PlaySound(0);
    }

    private void DisableAndReenable()
    {
        StartCoroutine(EnableAfterDelay());
    }

    private IEnumerator EnableAfterDelay()
    {
        crutchChild.SetActive(false);

        yield return new WaitForSeconds(5f);

        crutchChild.SetActive(true);
    }

    private void OnEnable()
    {
        HatchInteraction.PlayCrutchAnim += DisableAndReenable;
    }

    private void OnDisable()
    {
        HatchInteraction.PlayCrutchAnim -= DisableAndReenable;
    }
}

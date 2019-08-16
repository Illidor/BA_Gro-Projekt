using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : GrabInteractable
{
    [SerializeField]
    private Sound crowbarBreakSound;


    public override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        base.CarryOutInteraction_Carry(player);
        transform.localEulerAngles = new Vector3(-4.379f, 1.04f, -94.928f);
        //transform.localPosition = new Vector3(-0.019f, 0.103f, 0.024f);
        return true;
    }

    public void Break(InteractionScript player)
    {
        PutDown(player);
        Destroy(gameObject);
        crowbarBreakSound?.PlaySound(0);
    }
}

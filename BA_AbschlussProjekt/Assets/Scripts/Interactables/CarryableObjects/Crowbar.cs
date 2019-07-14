using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : GrabInteractable
{
    [SerializeField]
    private Sound crowbarBreakSound;

    public void Break(InteractionScript player)
    {
        PutDown(player);
        Destroy(gameObject);
        crowbarBreakSound?.PlaySound(0);
    }
}

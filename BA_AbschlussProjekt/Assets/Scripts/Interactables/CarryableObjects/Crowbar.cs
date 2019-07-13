using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : GrabInteractable
{
    [SerializeField]
    private Sound breakSound;

    public void Break(InteractionScript player)
    {
        PutDown(player);
        Destroy(gameObject);
        breakSound?.PlaySound(0);
    }
}

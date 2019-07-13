using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowbar : GrabInteractable
{
    public void Break(InteractionScript player)
    {
        PutDown(player);
        Destroy(gameObject);
    }
}

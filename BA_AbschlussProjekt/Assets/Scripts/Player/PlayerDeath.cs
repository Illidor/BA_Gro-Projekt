using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public delegate void PlayerDeathEvent();
    public static PlayerDeathEvent playerDeath;

    private void KillPlayer()
    {

        gameObject.GetComponentInChildren<Animator>().enabled = false;

        foreach (BoxCollider parent in gameObject.GetComponentsInChildren<BoxCollider>())
        {
            parent.isTrigger = false;
        }

        foreach (Rigidbody parent in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            parent.useGravity = true;
        }
    }

    private void OnEnable()
    {
        playerDeath += KillPlayer;
    }

    private void OnDisable()
    {
        playerDeath -= KillPlayer;
    }
}

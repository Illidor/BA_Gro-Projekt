﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crutch : FlashbackedGrabInteractable
{
    [SerializeField]
    private float reachIncreaseOnCarry = 1;
    [SerializeField]
    private Transform objectAttachpoint;
    [SerializeField]
    private bool attachingToObj = false;
    [SerializeField]
    private bool wasAttached = false;
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float attachTime = 0;

    private bool isMusicPlaying = false;
    [SerializeField] Sound bgMusic;

    private new void Awake()
    {
        playerAnim = FindObjectOfType<AnimationController>().animator;
        base.Awake();
    }
    protected override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        player.IncreaseReach(reachIncreaseOnCarry);
        attachingToObj = true;
        GetComponent<Sound>().PlaySound(0);

        if(isMusicPlaying == false) {
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
        attachingToObj = false;
        wasAttached = false;
        base.PutDown(player);
        GetComponent<Sound>().PlaySound(0);
    }
    private void Update()
    {
        if(playerAnim == null)
        {
            playerAnim = FindObjectOfType<AnimationController>().animator;
        }

        if (attachingToObj && !wasAttached)
        {
            wasAttached = true;
            //playerAnim.Play("character@Grab", 0);
            attachTime += Time.deltaTime;
        }
    }
    public void OnAnimatorIKFunc()
    {
        Debug.Log("onanimatorfunc");
        playerAnim.SetIKPosition(AvatarIKGoal.RightHand, objectAttachpoint.position);
        playerAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.6f);
    }
}

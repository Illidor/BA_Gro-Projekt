using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crutch : GrabInteractable
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

    private new void Awake()
    {
        playerAnim = FindObjectOfType<AnimationController>().animator;
        base.Awake();
    }
    protected override bool CarryOutInteraction_Carry(InteractionScript player)
    {
        player.IncreaseReach(reachIncreaseOnCarry);
        attachingToObj = true;
        GetComponent<Sound>().playSound(0);

        //return base.CarryOutInteraction_Carry(player);
        return true;
    }

    public override void PutDown(InteractionScript player)
    {
        player.ResetReachToDefault();
        attachingToObj = false;
        wasAttached = false;
        base.PutDown(player);
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
        else if(attachingToObj && wasAttached)
        {
            
            if (attachTime > 2.2f)
            {
                attachTime = 0;
                base.CarryOutInteraction_Carry(FindObjectOfType<InteractionScript>());
            }
            if (attachTime > 0)
            {
                attachTime += Time.deltaTime;
            }
        }
    }
    public void OnAnimatorIKFunc()
    {
        Debug.Log("onanimatorfunc");
        playerAnim.SetIKPosition(AvatarIKGoal.RightHand, objectAttachpoint.position);
        playerAnim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0.6f);
    }
}

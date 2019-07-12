using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class FastIKFabric : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightIK = null;
    public Transform rightHintIK = null;

    public Transform leftIK = null;
    public Transform leftHintIK = null;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    public void OnAnimatorIK(int layerIndex)
    {
        if (animator)
        {
            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {
                // Set the right hand target position and rotation, if one has been assigned
                if (rightIK != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightIK.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightIK.rotation);

                    animator.SetIKHintPosition(AvatarIKHint.RightElbow, rightHintIK.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1);
                }

                if(leftIK != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftIK.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftIK.rotation);

                    animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHintIK.position);
                    animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1);
                }
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
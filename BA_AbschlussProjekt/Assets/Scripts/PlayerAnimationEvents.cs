﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static event UnityAction ReachedLadderEnd;
    public static event UnityAction ShockPlayerAtStart;

    public static PlayerAnimationEvents instance = null; 


    private RigidbodyFirstPersonController fpController;

    [SerializeField] Sound footstepSound;
    [SerializeField] Sound footstepSoundAttic;
    [SerializeField] Sound knockOnWoodSound;
    [SerializeField] Sound climbLadderSound;

    [SerializeField] ParticleSystem dustPs;
    [SerializeField] Sound dustParticleSound;

    private Animator playerAnimator;
    private Transform playerTransform;
    private RigidbodyFirstPersonController playerController;

    private Transform mainCamTransform;

    private int footstepSoundCount = 0;
    private bool isAtAttic = false;
    private int previousFootStepId = 0;
    private int currentFootStepId = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        fpController = GetComponentInParent<RigidbodyFirstPersonController>();
        playerAnimator = GetComponent<Animator>();
        playerTransform = transform.parent;
        playerController = transform.parent.GetComponent<RigidbodyFirstPersonController>();
        mainCamTransform = Camera.main.transform;
    }

    private void FreezeMovement()
    {
        fpController.freezePlayerMovement = true;
    }

    private void UnfreezePlayerMovement()
    {
        fpController.freezePlayerMovement = false;
    }

    private void PlayFootstepSound() {
        footstepSoundCount++;
        if (footstepSoundCount % 2 == 0) {
            if (playerController.IsPlayerAtAttic)
            {
                while(currentFootStepId == previousFootStepId)
                    currentFootStepId = Random.Range(0, footstepSoundAttic.clips.Count);

                previousFootStepId = currentFootStepId;

                footstepSoundAttic.PlaySound(currentFootStepId, 1);
            }
            else
            {
                while (currentFootStepId == previousFootStepId)
                    currentFootStepId = Random.Range(0, footstepSound.clips.Count);

                previousFootStepId = currentFootStepId;

                footstepSound.PlaySound(currentFootStepId, 1);
            }
        }
        else {
            if (playerController.IsPlayerAtAttic)
            {
                while (currentFootStepId == previousFootStepId)
                    currentFootStepId = Random.Range(0, footstepSoundAttic.clips.Count);

                previousFootStepId = currentFootStepId;

                footstepSoundAttic.PlaySound(currentFootStepId, 2);
            }
            else
            {
                while (currentFootStepId == previousFootStepId)
                    currentFootStepId = Random.Range(0, footstepSound.clips.Count);

                previousFootStepId = currentFootStepId;

                footstepSound.PlaySound(currentFootStepId, 2);
            }
                
        }
    }

    private void FreezeCamera()
    {
        fpController.freezePlayerCamera = true;
    }

    private void UnfreezCamera()
    {
        fpController.freezePlayerCamera = false;
    }

    private void PlayDyingSound()
    {
        //dyingSound.Play();
    }

    private void PlayCollapseSound()
    {
        //collapseSound.Play();
    }

    private void PlayLadderSound()
    {
        climbLadderSound.PlaySound(0);
    }

    private void SetAnimatorTrigger(string triggerName)
    {
        playerAnimator.SetTrigger(triggerName);
    }

    private void SetAnimatorBool(string boolName, bool state)
    {
        playerAnimator.SetBool(boolName, state);
    }

    public void SnapPlayerToTargetPosition(Transform targetTransform)
    {
        Debug.Log(targetTransform.name);
        playerTransform.position = targetTransform.position;
        fpController.TargetRotation = targetTransform.rotation;

    }

    private void ShockPlayer()
    {
        ShockPlayerAtStart?.Invoke();
    }

    private void PlayWoodKnockAndEmitDust()
    {
        knockOnWoodSound.PlaySound(0);
        dustPs.Emit(10);
        dustParticleSound.PlaySound(0);
    }

    public void PlayAnimation(string trigger)
    {
        playerAnimator.SetTrigger(trigger);
    }

    public void DisableTrigger(string trigger)
    {
        playerAnimator.ResetTrigger(trigger);
    }

    public void ReachedEndOfLadder()
    {
        ReachedLadderEnd?.Invoke();
    }

    private void ChangeAtticState(bool state) {
        isAtAttic = state;
    }

    private void OnEnable()
    {
        ExitDoor.OpenDoorAnim += SetAnimatorTrigger;
        LadderInteraction.ClimbLadder += ChangeAtticState;
    }

    private void OnDisable()
    {
        ExitDoor.OpenDoorAnim -= SetAnimatorTrigger;
        LadderInteraction.ClimbLadder -= ChangeAtticState;
    }
}

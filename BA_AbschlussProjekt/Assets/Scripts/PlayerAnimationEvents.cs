using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerAnimationEvents : MonoBehaviour
{
    private RigidbodyFirstPersonController fpController;

    [SerializeField] AudioSource dyingSound;
    [SerializeField] AudioSource collapseSound;

    private Animator playerAnimator;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        fpController = GetComponentInParent<RigidbodyFirstPersonController>();
        playerAnimator = GetComponent<Animator>();
        playerTransform = transform.parent;
    }

    private void FreezeMovement()
    {
        fpController.freezePlayerMovement = true;
    }

    private void UnfreezePlayerMovement()
    {
        fpController.freezePlayerMovement = false;
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
        dyingSound.Play();
    }

    private void PlayCollapseSound()
    {
        collapseSound.Play();
    }

    private void SetAnimatorTrigger(string triggerName)
    {
        playerAnimator.SetTrigger(triggerName);
    }

    private void SetAnimatorBool(string boolName, bool state)
    {
        playerAnimator.SetBool(boolName, state);
    }

    private void SnapPlayerToTargetPosition(Transform targetTransform)
    {
        playerTransform.position = targetTransform.position;
    }

    private void OnEnable()
    {
        ExitDoor.OpenDoorAnim += SetAnimatorTrigger;
        ExitDoor.MovePlayerToTargetPosition += SnapPlayerToTargetPosition;
    }

    private void OnDisable()
    {
        ExitDoor.OpenDoorAnim -= SetAnimatorTrigger;
        ExitDoor.MovePlayerToTargetPosition -= SnapPlayerToTargetPosition;
    }
}

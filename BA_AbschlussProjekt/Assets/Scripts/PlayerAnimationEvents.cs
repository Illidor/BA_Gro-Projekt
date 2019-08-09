using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerAnimationEvents : MonoBehaviour
{
    public static event UnityAction ReachedLadderEnd;

    public static PlayerAnimationEvents instance = null; 


    private RigidbodyFirstPersonController fpController;

    [SerializeField] AudioSource dyingSound;
    [SerializeField] AudioSource collapseSound;

    private Animator playerAnimator;
    private Transform playerTransform;

    private Transform mainCamTransform;

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

    public void SnapPlayerToTargetPosition(Transform targetTransform)
    {
        playerTransform.position = targetTransform.position;
        fpController.TargetRotation = targetTransform.rotation;

    }

    public void PlayAnimation(string trigger)
    {
        playerAnimator.SetTrigger(trigger);
    }

    public void ReachedEndOfLadder()
    {
        ReachedLadderEnd?.Invoke();
    }

    private void OnEnable()
    {
        ExitDoor.OpenDoorAnim += SetAnimatorTrigger;
    }

    private void OnDisable()
    {
        ExitDoor.OpenDoorAnim -= SetAnimatorTrigger;
    }
}

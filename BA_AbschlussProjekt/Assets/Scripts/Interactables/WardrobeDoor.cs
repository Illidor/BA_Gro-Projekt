using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WardrobeDoor : HingedInteraction, ICombinable
{
    public static event UnityAction ShockPlayer;

    [SerializeField]
    private string wardrobeKeyName = "WardrobeKey";
    [SerializeField]
    private WardrobeDoor otherDoor;
    [SerializeField]
    private Sound doorBreakOpenSound;
    [SerializeField]
    private Rigidbody handcuffRB;
    [SerializeField]
    private Sound unlockWithKeySound;
    [SerializeField]
    private Animation lockanimationToPlay;
    public bool open;

    [SerializeField] Animator handcuffAnim;
    private bool wasHandcuffOpened = false;

    public bool IsLocked { get; set; }

    private new void Awake()
    {
        if(handcuffRB != null)
        {
            IsLocked = true;
        }
        open = false;
        base.Awake();
    }

    public bool HandleCombine(InteractionScript player, BaseInteractable currentlyHolding)
    {
        player.GUIInteractionFeedbackHandler.StandardCrosshair.SetActive(false);
        player.GUIInteractionFeedbackHandler.InteractionCrosshair.SetActive(true);
        player.GUIInteractionFeedbackHandler.ActionDescription.text = "Click to combine " + currentlyHolding.DisplayName + " with " + DisplayName;

        if (CTRLHub.InteractDown)
            return Combine(player, currentlyHolding);

        return false;
    }

    public bool Combine(InteractionScript player, BaseInteractable interactingComponent)
    {
        if (interactingComponent is Crowbar)
        {
            //doorBreakOpenSound?.PlaySound(0);
            ShockPlayer?.Invoke();
            return true;
        }
        else if (interactingComponent.name.Equals(wardrobeKeyName))
        {
            unlockWithKeySound?.PlaySound(0);
        }
        else    // if wrong combine object, don't execute code below
        {
            return false; 
        }

        IsLocked = false;
        otherDoor.IsLocked = false;

        if(wasHandcuffOpened == false)
            handcuffAnim.SetTrigger("Unlock");

        //if (handcuffRB != null)
        //{
        //    Destroy(handcuffRB.gameObject.GetComponent<HingeJoint>());
        //    Destroy(otherDoor.GetComponent<WardrobeDoor>().handcuffRB.GetComponent<HingeJoint>());

        //    handcuffRB.useGravity = !IsLocked;
        //    handcuffRB.isKinematic = IsLocked;
        //}

        return true;
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        if (IsLocked)
        {
            if (gameObject.name == "mdl_wardrobe_door_right")
            {
                lockanimationToPlay.Play();
            }
            else
            {
                lockanimationToPlay.Play();
            }
            return false;

        }
        else
        {
            //Open Door animation
            StartCoroutine(doorAnimation());
        }

        return true;
        //return base.CarryOutInteraction(player);
    }

    public IEnumerator doorAnimation()
    {
        if (!open)
        {
            if (gameObject.name == "mdl_wardrobe_door_right")
            {
                while (gameObject.transform.localEulerAngles.y > 270)
                {
                    gameObject.transform.Rotate(0, -1, 0);
                    otherDoor.transform.Rotate(0, 1, 0);
                    yield return new WaitForFixedUpdate();
                }
            }
            else
            {
                while (gameObject.transform.localEulerAngles.y < 90)
                {
                    gameObject.transform.Rotate(0, 1, 0);
                    otherDoor.transform.Rotate(0, -1, 0);
                    yield return new WaitForFixedUpdate();
                }
            }
        }
        else
        {
            if (gameObject.name == "mdl_wardrobe_door_right")
            {
                while (gameObject.transform.localEulerAngles.y > 0)
                {
                    gameObject.transform.Rotate(0, 1, 0);
                    otherDoor.transform.Rotate(0, -1, 0);
                    yield return new WaitForFixedUpdate();
                }
            }
            else
            {
                while (gameObject.transform.localEulerAngles.y > 0)
                {
                    gameObject.transform.Rotate(0, -1, 0);
                    otherDoor.transform.Rotate(0, 1, 0);
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        open = !open;
        otherDoor.GetComponent<WardrobeDoor>().open = open;
        yield return new WaitForFixedUpdate();
    }
}
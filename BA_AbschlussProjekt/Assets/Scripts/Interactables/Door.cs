using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : BaseInteractable
{
    public static event UnityAction TriggerAnim1; 
    public static event UnityAction TriggerAnim2; 
    public static event UnityAction TriggerAnim3; 

    public Animation openDoor;

    private bool isOpen = false;

    //Check if the Door is locked
    public bool isLocked = false;

    private int interactionCounter = 0;

    protected new void Awake()
    {
        textToDisplayOnHover = "Click to try to break down " + DisplayName;
        base.Awake();
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        return OpenDoor();
    }

    /// <summary>
    /// Opens the door. Returns whether the door got successfully opend or not.
    /// </summary>
    /// <returns>Whether the door got opend or not</returns>
    private bool OpenDoor()
    {
        if (isLocked)
        {
            switch (interactionCounter)
            {
                case 0:
                    if (TriggerAnim1 != null)
                    {
                        TriggerAnim1();
                        interactionCounter++;
                    }
                    break;
                case 1:
                    if (TriggerAnim2 != null)
                    {
                        TriggerAnim2();
                        interactionCounter++;
                    }
                    break;
                case 2:
                    if (TriggerAnim3 != null)
                    {
                        TriggerAnim3();
                    }
                    break;
            }
        }
        else if (!openDoor.isPlaying && !isOpen && !isLocked)
        {
            openDoor.Play();
            isOpen = true;
            return true;
        }
        
        return false;
    }

    // if combining door with key to unlock is needed, inherit ICombinable

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackTurnHead : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad7) )
        {
            TurnHead();
        }

        if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            KickDoor();
        }

        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Punch();
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            ShoulderTackle();
        }
    }

    private void TurnHead()
    {
        animator.SetTrigger("TurnHead");

    }

    private void KickDoor()
    {
        animator.SetTrigger("KickDoor");

    }

    private void Punch()
    {
        animator.SetTrigger("Punch");

    }

    private void ShoulderTackle()
    {
        animator.SetTrigger("OpenDoorWithShoulder");
    }

    private void OnEnable()
    {
        Door.TriggerAnim3 += KickDoor;
        Door.TriggerAnim1 += Punch;
        Door.TriggerAnim2 += ShoulderTackle;
    }

    private void OnDisable()
    {
        Door.TriggerAnim3 -= KickDoor;
        Door.TriggerAnim1 -= Punch;
        Door.TriggerAnim2 -= ShoulderTackle;
    }
}

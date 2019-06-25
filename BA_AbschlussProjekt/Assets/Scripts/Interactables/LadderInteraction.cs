using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : BaseInteractable
{
    [SerializeField]
    private float standardClimbingSpeed = 0.01f;
    [SerializeField]
    private float slowClimbingSpeed = 0.005f;
    [SerializeField][Tooltip("The minimal distance from the bottom of the ladder the player snaps onto")]
    private Vector3 ladderSnapOffsetFromBelow = new Vector3(0, 0.075f, 0);
    [SerializeField][Tooltip("The minimal distance from the top of the ladder the player snaps onto")]
    private Vector3 ladderSnapOffsetFromAbove = new Vector3(0, -1f, 0);
    [Space] // Serialized Fields that don't need to be touched to be tweeked
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;

    public float CurrentClimbingSpeed { get; protected set; }

    private InteractionScript currentClimber;
    private bool isBeeingClimbed;

    private bool IsBeeingClimbed
    {
        get => isBeeingClimbed;
        set
        {
            isBeeingClimbed = value;
            if (value == false)
                currentClimber = null;
        }
    }

    private void OnValidate()
    {
        if (standardClimbingSpeed <= 0)
            standardClimbingSpeed = 0.001f;
    }

    // Audio ticker that plays sound after X seconds when player is on ladder
    private float climbTicker = 5f;
    private float climbAudioThreshold = 0.75f;

    private new void Awake()
    {
        CurrentClimbingSpeed = standardClimbingSpeed;

        base.Awake();
    }

    void Update()
    {
        if (!IsBeeingClimbed)
            return;

        currentClimber.transform.localPosition += (endPoint.position - startPoint.position).normalized * (CurrentClimbingSpeed * CTRLHub.VerticalAxis);

        if (currentClimber.transform.position.y < startPoint.position.y ||
            currentClimber.transform.position.y > endPoint.position.y     )
        {
            DetachFromLadder();
        }


        // <Ladder audio handling>
        climbTicker += Time.deltaTime;

        if(climbTicker > climbAudioThreshold && (Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < -0.1f))
        {
            climbTicker = 0f;
            //AudioManager.audioManager.Play("snd_climbing_ladder");
        }
        // </Ladder audio handling>
    }

    private void DetachFromLadder()
    {
        Rigidbody currentClimblerRigidbody = currentClimber.GetComponent<Rigidbody>();
        currentClimblerRigidbody.isKinematic = false;
        currentClimblerRigidbody.useGravity = true;

        IsBeeingClimbed = false;
        currentClimber = null;
    }

    public override bool Interact(InteractionScript player, Conditions condition, float minCondition)
    {
        if ((float)condition < minCondition)
            SetClimbingSpeedToSlow();
        else
            ResetClimbingSpeedToStandard();

        currentClimber = player;
        Rigidbody currentClimblerRigidbody = currentClimber.GetComponent<Rigidbody>();
        currentClimblerRigidbody.isKinematic = true;
        currentClimblerRigidbody.useGravity = false;

        //Snap Player onto nearest Pos on Ladder
        Vector3 heading = endPoint.position - startPoint.position;
        float magnitudeMax = heading.magnitude;
        heading.Normalize();
        Vector3 lhs = currentClimber.transform.position - startPoint.position;
        float dotProd = Vector3.Dot(lhs, heading);
        dotProd = Mathf.Clamp(dotProd, 0f, magnitudeMax);
        Vector3 ladderMountingPos = startPoint.position + heading * dotProd;

        if (ladderMountingPos.y <= startPoint.position.y + ladderSnapOffsetFromBelow.y)
        {
            ladderMountingPos = startPoint.position + ladderSnapOffsetFromBelow;
        }
        else if (ladderMountingPos.y > endPoint.position.y + ladderSnapOffsetFromAbove.y)
        {
            ladderMountingPos = endPoint.position + ladderSnapOffsetFromAbove;
        }

        currentClimber.transform.position = ladderMountingPos;

        IsBeeingClimbed = true;

        return true;
    }

    public void ResetClimbingSpeedToStandard()
    {
        CurrentClimbingSpeed = standardClimbingSpeed;
    }

    public void SetClimbingSpeedToSlow()
    {
        CurrentClimbingSpeed = slowClimbingSpeed;
    }
}

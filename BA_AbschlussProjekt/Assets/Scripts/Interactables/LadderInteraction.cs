using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : ConditionedInteraction
{
    [SerializeField]
    private float standardClimbingSpeed = 0.01f;
    [SerializeField]
    private float slowClimbingSpeed = 0.005f;
    [SerializeField]
    private float minConditionToClimbFast = 1.5f;
    [SerializeField][Tooltip("The minimal distance from the bottom of the ladder the player snaps onto")]
    private Vector3 ladderSnapOffsetFromBelow = new Vector3(0, 0.075f, 0);
    [SerializeField][Tooltip("The minimal distance from the top of the ladder the player snaps onto")]
    private Vector3 ladderSnapOffsetFromAbove = new Vector3(0, -1f, 0);
    [Space] // Serialized Fields that don't need to be touched to be tweeked
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;
    [SerializeField]
    private Transform LeftIKHand;
    [SerializeField]
    private Transform RightIKHand;
    [SerializeField]
    private Transform GrabingPoint;
    [SerializeField]
    private List<Transform> LeftHandGrabPoints;
    [SerializeField]
    private List<Transform> RightHandGrabPoints;

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

    // climbing audio
    private Sound climbingSound;
    [SerializeField] private float climbingSoundTicker = 1f;
    [SerializeField] private float climbingSoundThreshold = 1.5f;
    private int climbCount = 0;

    private void Start() {
        climbingSound = GetComponent<Sound>();
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

        if (CTRLHub.DropDown)
            DetachFromLadder();

        Transform leftHandGrabPoint = LeftHandGrabPoints[0];
        foreach (Transform item in LeftHandGrabPoints)
        {
            if ((item.position - GrabingPoint.position).magnitude < (leftHandGrabPoint.position - GrabingPoint.position).magnitude)
                leftHandGrabPoint = item;
        }

        Transform rightHandGrabPoint = RightHandGrabPoints[0];
        foreach (Transform item in RightHandGrabPoints)
        {
            if ((item.position - GrabingPoint.position).magnitude < (rightHandGrabPoint.position - GrabingPoint.position).magnitude)
                rightHandGrabPoint = item;
        }

        LeftIKHand.transform.position = Vector3.MoveTowards(LeftIKHand.transform.position, leftHandGrabPoint.position, .4f);
        LeftIKHand.transform.rotation = Quaternion.Lerp(LeftIKHand.transform.rotation, leftHandGrabPoint.rotation, .4f);
        RightIKHand.transform.position = Vector3.MoveTowards(RightIKHand.transform.position, rightHandGrabPoint.position, .4f);
        RightIKHand.transform.rotation = Quaternion.Lerp(RightIKHand.transform.rotation, rightHandGrabPoint.rotation, .4f);

        currentClimber.transform.localPosition += (endPoint.position - startPoint.position).normalized * (CurrentClimbingSpeed * CTRLHub.VerticalAxis);

        if (currentClimber.transform.position.y < startPoint.position.y ||
            currentClimber.transform.position.y > endPoint.position.y     )
        {
            DetachFromLadder();
        }

        // <Ladder audio handling>
        climbingSoundTicker += Time.deltaTime; 
        if (climbingSoundTicker > climbingSoundThreshold && (Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < -0.1f)) {
            climbingSoundTicker = 0f;
            climbCount++;
            // Play sounds at different audio sources so they don't get killed before fully played
            if (climbCount % 2 == 0) {
                climbingSound.PlaySound(0, 1);
            }
            else {
                climbingSound.PlaySound(0, 2);
            }
        }
        // </Ladder audio handling>
    }

    private void DetachFromLadder()
    {
        Rigidbody currentClimblerRigidbody = currentClimber.GetComponent<Rigidbody>();
        currentClimblerRigidbody.isKinematic = false;
        currentClimblerRigidbody.useGravity = true;
        currentClimber.ResetIK();

        IsBeeingClimbed = false;
        currentClimber = null;
    }

    public override void HandleInteraction(InteractionScript player)
    {
        if (player.PlayerHealth.GetSummedCondition() > minConditionToClimbFast)
            ResetClimbingSpeedToStandard();
        else
            SetClimbingSpeedToSlow();

        base.HandleInteraction(player);
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
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

    private void OnEnable()
    {
        InteractionScript.StartPickup += DetachFromLadder;
    }

    private void OnDisable()
    {
        InteractionScript.StartPickup -= DetachFromLadder;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderInteraction : BaseInteractable
{
    [SerializeField]
    private float climbingSpeed;
    [Space]
    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;

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

    public override bool Interact(InteractionScript interactionScript)
    {
        currentClimber = interactionScript;
        currentClimber.GetComponent<Rigidbody>().isKinematic = true;
        currentClimber.GetComponent<Rigidbody>().useGravity = false;

        //Snap Player onto nearest Pos on Ladder
        Vector3 heading = endPoint.position - startPoint.position;
        float magnitudeMax = heading.magnitude;
        heading.Normalize();
        Vector3 lhs = currentClimber.transform.position - startPoint.position;
        float dotProd = Vector3.Dot(lhs, heading);
        dotProd = Mathf.Clamp(dotProd, 0f, magnitudeMax);
        Vector3 ladderMountingPos = startPoint.position + heading * dotProd;

        if (ladderMountingPos.y <= startPoint.position.y)
            ladderMountingPos = startPoint.position + new Vector3(0, 0.075f, 0);
        else if (ladderMountingPos.y >= startPoint.position.y)
            ladderMountingPos = startPoint.position + new Vector3(0, -0.075f, 0);
        currentClimber.transform.position = ladderMountingPos;

        IsBeeingClimbed = true;

        return true;
    }

    void Update()
    {
        if (!IsBeeingClimbed)
            return;

        currentClimber.transform.localPosition += (endPoint.position - startPoint.position).normalized * (climbingSpeed * CTRLHub.VerticalAxis);

        if (currentClimber.transform.position.y < startPoint.position.y)
        {
            //Vector3 ladderDetachOffset = transform.parent.position - startPoint.position;
            //ladderDetachOffset.y = startPoint.position.y;
            DetachFromLadder();
        }
        else if (currentClimber.transform.position.y > endPoint.position.y)
        {
            DetachFromLadder();
        }
    }

    private void DetachFromLadder()
    {
        currentClimber.GetComponent<Rigidbody>().isKinematic = false;
        currentClimber.GetComponent<Rigidbody>().useGravity = true;
        IsBeeingClimbed = false;
        currentClimber = null;
    }


    public override bool Combine(GameObject gameObject)
    {
        throw new NotImplementedException();
    }

    public override bool Use()
    {
        throw new NotImplementedException();
    }
}

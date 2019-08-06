using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommodeInteraction : BaseInteractable
{
    private float firstPos;
    [SerializeField]
    private float openDistance;
    [SerializeField]
    private float interactionSpeed;
    private bool isOpen = false;

    public override bool CarryOutInteraction(InteractionScript player)
    {
        isOpen = !isOpen;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen && transform.localPosition.x > firstPos - openDistance)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(firstPos - openDistance, transform.localPosition.y, transform.localPosition.z), interactionSpeed);
        }
        else if(!isOpen && transform.localPosition.x < firstPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(firstPos, transform.localPosition.y, transform.localPosition.z), interactionSpeed);
        }
    }



}

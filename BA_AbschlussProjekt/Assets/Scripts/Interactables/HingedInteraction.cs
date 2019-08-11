using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingedInteraction : BaseInteractable
{
    private const float rotationMargin = 0.05f;

    [SerializeField]
    private float minLocalRotation;
    [SerializeField]
    private float maxLocalRotation;
    [SerializeField]
    private Vector3 axis = new Vector3(0, 1, 0);
    [Space]
    [SerializeField]
    private Transform transformToHinge;
    private bool open;

    private bool isOpen;

    private void OnValidate()
    {
        if (transformToHinge == null)
            transformToHinge = transform;
    }

    protected new void Awake()
    {
        if (transformToHinge == null)
            transformToHinge = transform;

        if (axis.x + axis.y + axis.z != 1)
            axis = Vector3.zero;

        //OpenCloseDoor(false);
        isOpen = false;
        base.Awake();
    }

    /// <summary>
    /// Sets door to open or close. Pass null (or nothing) to switch states
    /// </summary>
    private void OpenCloseDoor(bool? setOpen = null)
    {
        StartCoroutine(openAnimation());

        //if (setOpen == true)
        //{
        //    transformToHinge.localRotation = Quaternion.Euler(GetHingeExtreme(Extreme.Max));
        //    isOpen = true;
        //}
        //else if (setOpen == false)
        //{
        //    transformToHinge.localRotation = Quaternion.Euler(GetHingeExtreme(Extreme.Min));
        //    isOpen = false;
        //}
        //else
        //{
        //    OpenCloseDoor( ! isOpen);   // switch door position
        //}
    }

    public IEnumerator openAnimation()
    {
        if (open)
        {
            while (gameObject.transform.localEulerAngles.x >= 270)
            {
                gameObject.transform.Rotate(-1, 0, 0);
                yield return new WaitForFixedUpdate();
            }
        }
        else
        {
            while (gameObject.transform.localEulerAngles.x <= 270)
            {
                gameObject.transform.Rotate(1, 0, 0);
                yield return new WaitForFixedUpdate();
            }
        }

        open = !open;
    }

    private Vector3 GetHingeExtreme(Extreme extreme)
    {
        switch (extreme)
        {
            case Extreme.Min: return axis * minLocalRotation;
            case Extreme.Max: return axis * maxLocalRotation;
            default:          return Vector3.zero;
        }
    }

    public override bool CarryOutInteraction(InteractionScript player)
    {
        OpenCloseDoor();
        return true;
    }

    private enum Extreme
    {
        None,
        Min,
        Max
    }

}

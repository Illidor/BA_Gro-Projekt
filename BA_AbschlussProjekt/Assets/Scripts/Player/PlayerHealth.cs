using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEnums;
using System;


public enum Conditions
{
    UpperBodyCondition,
    LowerBodyCondition
}

public class PlayerHealth : MonoBehaviour
{



    private float upperBodyCondition;
    private float lowerBodyCondition;

    private void Awake()
    {
        upperBodyCondition = 2f;
        lowerBodyCondition = 2f;
    }

    public void changeCondition(Conditions which, float value)
    {
        switch (which)
        {
            case (Conditions.UpperBodyCondition):

                if (upperBodyCondition - value > 0)
                {
                    upperBodyCondition -= value;
                }
                else
                {
                    upperBodyCondition = 0f;
                }

                break;
            case (Conditions.LowerBodyCondition):

                if (lowerBodyCondition - value > 0)
                {
                    lowerBodyCondition -= value;
                }
                else
                {
                    lowerBodyCondition = 0f;
                }

                break;
        }
    }

    public float getCondition(Conditions which)
    {
        switch (which)
        {
            case (Conditions.UpperBodyCondition):

                return upperBodyCondition;

            case (Conditions.LowerBodyCondition):

                return lowerBodyCondition;
        }
        return 0f;
    }

    //ToDo: Play Sound
}
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

    [SerializeField]
    private Sound smallConditionSound;
    [SerializeField]
    private Sound bigConditionSound;

    private void Awake()
    {
        upperBodyCondition = 2f;
        lowerBodyCondition = 2f;
    }

    public void ChangeCondition(Conditions which, float value)
    {
        switch (which)
        {
            case (Conditions.UpperBodyCondition):

                if (upperBodyCondition - value > 0)
                {
                    upperBodyCondition -= value;
                    // Check which sound to play
                    if(Mathf.Approximately(upperBodyCondition, 1.5f)){
                        smallConditionSound.PlaySound(0);
                    }
                    else if(Mathf.Approximately(upperBodyCondition, 1f)) {
                        bigConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(upperBodyCondition, 0.5f)) {
                        smallConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(upperBodyCondition, 0f)) {
                        bigConditionSound.PlaySound(0);
                    }
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

                    // Check which sound to play
                    if (Mathf.Approximately(lowerBodyCondition, 1.5f)) {
                        smallConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(lowerBodyCondition, 1f)) {
                        bigConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(lowerBodyCondition, 0.5f)) {
                        smallConditionSound.PlaySound(0);
                    }
                    else if (Mathf.Approximately(lowerBodyCondition, 0f)) {
                        bigConditionSound.PlaySound(0);
                    }
                }
                else
                {
                    lowerBodyCondition = 0f;
                }

                break;
        }
    }

    public float GetCondition(Conditions which)
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

    public float GetSummedCondition()
    {
        return upperBodyCondition + lowerBodyCondition;
    }

    //ToDo: Play Sound
}
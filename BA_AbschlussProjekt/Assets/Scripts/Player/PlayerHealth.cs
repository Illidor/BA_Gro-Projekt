﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEnums;
using System;
using UnityEngine.Events;


public enum Conditions
{
    UpperBodyCondition,
    LowerBodyCondition
}

public class PlayerHealth : MonoBehaviour
{
    public static event UnityAction PlayerDied;

    private float upperBodyCondition;
    private float lowerBodyCondition;

    [SerializeField]
    private Sound smallConditionSound;
    [SerializeField]
    private Sound bigConditionSound;

    public Camera monitorRoomCamera;
    public Camera mainCamera;

    private void Awake()
    {
        upperBodyCondition = 2f;
        lowerBodyCondition = 2f;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlayerDeath();
        }
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

    public void PlayerDeath()
    {
        Debug.Log("He dead");
        mainCamera.enabled = false;
        monitorRoomCamera.enabled = true;

        if (PlayerDied != null)
            PlayerDied();
    }

    //ToDo: Play Sound

    private void OnEnable()
    {
        Picture.PlayerFailed += PlayerDeath;
    }

    private void OnDisable()
    {
        Picture.PlayerFailed -= PlayerDeath;
    }

}
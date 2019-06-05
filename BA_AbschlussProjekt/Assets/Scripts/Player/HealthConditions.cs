using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthConditions : MonoBehaviour
{
    // bool param is true for adding condition and false for removing
    public static event UnityAction<Condition, bool> ChangeCondition;

    public enum Condition { ArmDislocated, HandSprained, TwistedAnkle, BrokenLeg }
    private List<Condition> currentConditions = new List<Condition>();

    void Update()
    {
        // Test functions to add conditions to the player
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddConditionToCurrentConditions(Condition.ArmDislocated);
            Debug.Log("Disloacte Arm");
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AddConditionToCurrentConditions(Condition.HandSprained);
            Debug.Log("Sprain Hand");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            AddConditionToCurrentConditions(Condition.TwistedAnkle);
            Debug.Log("Twist Ankle");
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AddConditionToCurrentConditions(Condition.BrokenLeg);
            Debug.Log("Break Leg");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            RemoveConditionFromCurrentConditions(Condition.ArmDislocated);
            RemoveConditionFromCurrentConditions(Condition.BrokenLeg);
            RemoveConditionFromCurrentConditions(Condition.HandSprained);
            RemoveConditionFromCurrentConditions(Condition.TwistedAnkle);
        }
    }

    // Add condition to current conditions list and calls an event which casues the Player Controller to react with value changes
    private void AddConditionToCurrentConditions(Condition con)
    {
        if(currentConditions.Contains(con) == false)
        {
            currentConditions.Add(con);
            if (ChangeCondition != null)
            {
                // bool param is true for adding condition and false for removing
                ChangeCondition(con, true);
            }
        }
    }

    // Remove condition from current conditions when a twisted ankle gets "healed" and call an event to change back the Player Values
    private void RemoveConditionFromCurrentConditions(Condition con)
    {
        if(currentConditions.Contains(con) == true)
        {
            currentConditions.Remove(con);
            if (ChangeCondition != null)
            {
                // bool param is true for adding condition and false for removing
                ChangeCondition(con, false);
            }
        }
    }

    public List<Condition> GetConditions()
    {
        return currentConditions;
    }
}

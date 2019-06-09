using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    public enum PlayerInjuries
    {
        LeftArmDislocated,
        RightArmDislocated,

        LeftHandSprained,
        RightHandSprained,

        LeftHandSplinter,
        RightHandSplinter,

        LeftFootSprained,
        RightFootSprained,

        LeftFootBroken,
        RightFootBroken
    }
    public enum ObjectParameters
    {
        CarryOneHand,
        PullOneHand,
        CarryTwoHands,
        PullTwoHands
    }
}

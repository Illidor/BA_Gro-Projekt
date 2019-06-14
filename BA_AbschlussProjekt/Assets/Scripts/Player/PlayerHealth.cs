using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEnums;
using System;

public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// List of all injuries currently affecting the player
    /// </summary>
    [EnumFlag]
    public PlayerInjuries playerInjuries = PlayerInjuries.None;

    /// <summary>
    /// Add an injury to the player. (For debuging, an injury can also be added in the inspector)
    /// </summary>
    /// <returns>Whether it successfully added the injury (true), or not (false, it was already present)</returns>
    public bool AddInjury(PlayerInjuries injury)
    {
        if (playerInjuries.HasFlag(injury))
            return false;

        playerInjuries |= injury;   //Bitwise OR

        return true;
    }

    /// <summary>
    /// Remove an injury from the player. (For debuging, an injury can also be removed in the inspector)
    /// </summary>
    /// <returns>Whether it successfully removed the injury (true), or not (false, it wasn't present)</returns>
    public bool RemoveInjury(PlayerInjuries injury)
    {
        if (playerInjuries.HasFlag(injury) == false)
            return false;

        playerInjuries ^= injury;   //Bitwise XOR

        return true;
    }

    /// <summary>
    /// Converts the set of player injuries into the corresponding interaction condition
    /// </summary>
    public InteractionConditions GetInjuriesAsInteractionConditions()
    {
        switch ((int)playerInjuries)
        {
            case 0b__00_00__00_00: return InteractionConditions.EverythingFine;
            case 0b__00_00__00_01: return InteractionConditions.OneHandSprained_RestFine;
            case 0b__00_00__00_10: return InteractionConditions.OneHandSprained_RestFine;
            case 0b__00_00__00_11: return InteractionConditions.BothHandsSprained_FeetFine;
            case 0b__00_00__01_00: return InteractionConditions.OneArmDislocated_RestFine;
            case 0b__00_00__10_00: return InteractionConditions.OneArmDislocated_RestFine;
            case 0b__00_00__01_01: throw new NotPossibleException();
            case 0b__00_00__01_10: return InteractionConditions.OneArmDislocatedOneSprained_FeetFine;
            case 0b__00_00__01_11: throw new NotPossibleException();
            case 0b__00_00__10_01: return InteractionConditions.OneArmDislocatedOneSprained_FeetFine;
            case 0b__00_00__10_10: throw new NotPossibleException();
            case 0b__00_00__10_11: throw new NotPossibleException();
            case 0b__00_00__11_00: return InteractionConditions.BothArmsDislocated_FeetFine;
            case 0b__00_00__11_01: throw new NotPossibleException();
            case 0b__00_00__11_10: throw new NotPossibleException();
            case 0b__00_00__11_11: throw new NotPossibleException();
            case 0b__00_01__00_00: return InteractionConditions.OneFootSprained_RestFine;
            case 0b__00_01__00_01: return InteractionConditions.OneFootAndOneHandSprained_RestFine;
            case 0b__00_01__00_10: return InteractionConditions.OneFootAndOneHandSprained_RestFine;
            case 0b__00_01__00_11: return InteractionConditions.BothHandsSprainedOneFootSprained;
            case 0b__00_01__01_00: return InteractionConditions.OneFootSprainedOneArmDislocated_RestFine;
            case 0b__00_01__01_01: throw new NotPossibleException();
            case 0b__00_01__01_10: return InteractionConditions.OneHandSprainedOneDislocatedOneFootSprained;
            case 0b__00_01__01_11: throw new NotPossibleException();
            case 0b__00_01__10_00: return InteractionConditions.OneFootSprainedOneArmDislocated_RestFine;
            case 0b__00_01__10_01: return InteractionConditions.OneHandSprainedOneDislocatedOneFootSprained;
            case 0b__00_01__10_10: throw new NotPossibleException();
            case 0b__00_01__10_11: throw new NotPossibleException();
            case 0b__00_01__11_00: return InteractionConditions.BothArmsDislocatedOneFootSprained;
            case 0b__00_01__11_01: throw new NotPossibleException();
            case 0b__00_01__11_10: throw new NotPossibleException();
            case 0b__00_01__11_11: throw new NotPossibleException();
            case 0b__00_10__00_00: return InteractionConditions.OneFootSprained_RestFine;
            case 0b__00_10__00_01: return InteractionConditions.OneFootAndOneHandSprained_RestFine;
            case 0b__00_10__00_10: return InteractionConditions.OneFootAndOneHandSprained_RestFine;
            case 0b__00_10__00_11: return InteractionConditions.BothHandsSprainedOneFootSprained;
            case 0b__00_10__01_00: return InteractionConditions.OneFootSprainedOneArmDislocated_RestFine;
            case 0b__00_10__10_00: return InteractionConditions.OneFootSprainedOneArmDislocated_RestFine;
            case 0b__00_10__01_01: throw new NotPossibleException();
            case 0b__00_10__01_10: return InteractionConditions.OneHandSprainedOneDislocatedOneFootSprained;
            case 0b__00_10__10_01: return InteractionConditions.OneHandSprainedOneDislocatedOneFootSprained;
            case 0b__00_10__01_11: throw new NotPossibleException();
            case 0b__00_10__10_10: throw new NotPossibleException();
            case 0b__00_10__10_11: throw new NotPossibleException();
            case 0b__00_10__11_00: return InteractionConditions.BothArmsDislocatedOneFootSprained;
            case 0b__00_10__11_01: throw new NotPossibleException();
            case 0b__00_10__11_10: throw new NotPossibleException();
            case 0b__00_10__11_11: throw new NotPossibleException();
            case 0b__00_11__00_00: return InteractionConditions.BothFeetSprained_HandsFine;
            case 0b__00_11__00_01: return InteractionConditions.OneHandSprainedBothFeetSprained;
            case 0b__00_11__00_10: return InteractionConditions.OneHandSprainedBothFeetSprained;
            case 0b__00_11__00_11: return InteractionConditions.BothHandsAndBothFeetSprained;
            case 0b__00_11__01_00: return InteractionConditions.OneArmDislocatedBothFeetSprained;
            case 0b__00_11__01_01: throw new NotPossibleException();
            case 0b__00_11__01_10: return InteractionConditions.OneHandSprainedOneDislocatedBothFeetSprained;
            case 0b__00_11__01_11: throw new NotPossibleException();
            case 0b__00_11__10_00: return InteractionConditions.OneArmDislocatedBothFeetSprained;
            case 0b__00_11__10_01: return InteractionConditions.OneHandSprainedOneDislocatedBothFeetSprained;
            case 0b__00_11__10_10: throw new NotPossibleException();
            case 0b__00_11__10_11: throw new NotPossibleException();
            case 0b__00_11__11_00: return InteractionConditions.BothArmsDislocatedBothFeetSprained;
            case 0b__00_11__11_01: throw new NotPossibleException();
            case 0b__00_11__11_10: throw new NotPossibleException();
            case 0b__00_11__11_11: throw new NotPossibleException();
            case 0b__01_00__00_00: return InteractionConditions.OneFootBroken_RestFine;
            case 0b__01_00__00_01: return InteractionConditions.OneFootBrokenOneHandSprained_RestFine;
            case 0b__01_00__00_10: return InteractionConditions.OneFootBrokenOneHandSprained_RestFine;
            case 0b__01_00__00_11: return InteractionConditions.BothHandsSprainedOneFootBroken;
            case 0b__01_00__01_00: return InteractionConditions.OneFootBrokenOneArmDislocated_RestFine;
            case 0b__01_00__01_01: throw new NotPossibleException();
            case 0b__01_00__01_10: return InteractionConditions.OneHandSprainedOneDislocatedOneFootBroken;
            case 0b__01_00__01_11: throw new NotPossibleException();
            case 0b__01_00__10_00: return InteractionConditions.OneFootBrokenOneArmDislocated_RestFine;
            case 0b__01_00__10_01: return InteractionConditions.OneHandSprainedOneDislocatedOneFootBroken;
            case 0b__01_00__10_10: throw new NotPossibleException();
            case 0b__01_00__10_11: throw new NotPossibleException();
            case 0b__01_00__11_00: return InteractionConditions.BothArmsDislocatedOneFootBroken;
            case 0b__01_00__11_01: throw new NotPossibleException();
            case 0b__01_00__11_10: throw new NotPossibleException();
            case 0b__01_00__11_11: throw new NotPossibleException();
            case 0b__01_01__00_00: throw new NotPossibleException();
            case 0b__01_01__00_01: throw new NotPossibleException();
            case 0b__01_01__00_10: throw new NotPossibleException();
            case 0b__01_01__00_11: throw new NotPossibleException();
            case 0b__01_01__01_00: throw new NotPossibleException();
            case 0b__01_01__01_01: throw new NotPossibleException();
            case 0b__01_01__01_10: throw new NotPossibleException();
            case 0b__01_01__01_11: throw new NotPossibleException();
            case 0b__01_01__10_00: throw new NotPossibleException();
            case 0b__01_01__10_01: throw new NotPossibleException();
            case 0b__01_01__10_10: throw new NotPossibleException();
            case 0b__01_01__10_11: throw new NotPossibleException();
            case 0b__01_01__11_00: throw new NotPossibleException();
            case 0b__01_01__11_01: throw new NotPossibleException();
            case 0b__01_01__11_10: throw new NotPossibleException();
            case 0b__01_01__11_11: throw new NotPossibleException();
            case 0b__01_10__00_00: return InteractionConditions.OneFootSprainedOneFootBroken_HandsFine;
            case 0b__01_10__00_01: return InteractionConditions.OneHandSprainedOneFootSprainedOneFootBroken;
            case 0b__01_10__00_10: return InteractionConditions.OneHandSprainedOneFootSprainedOneFootBroken;
            case 0b__01_10__00_11: return InteractionConditions.BothHandSprainedOneFootSprainedOneBroken;
            case 0b__01_10__01_00: return InteractionConditions.OneArmDislocatedOneFootSprainedOneFootBroken;
            case 0b__01_10__01_01: throw new NotPossibleException();
            case 0b__01_10__01_10: return InteractionConditions.OneArmAndOneFootSprainedOneArmDislocatedOneFootBroken;
            case 0b__01_10__01_11: throw new NotPossibleException();
            case 0b__01_10__10_00: return InteractionConditions.OneArmDislocatedOneFootSprainedOneFootBroken;
            case 0b__01_10__10_01: return InteractionConditions.OneHandSprainedOneFootSprainedOneFootBroken;
            case 0b__01_10__10_10: throw new NotPossibleException();
            case 0b__01_10__10_11: throw new NotPossibleException();
            case 0b__01_10__11_00: return InteractionConditions.BothArmsDislocatedOneFootSprainedOneBroken;
            case 0b__01_10__11_01: throw new NotPossibleException();
            case 0b__01_10__11_10: throw new NotPossibleException();
            case 0b__01_10__11_11: throw new NotPossibleException();
            case 0b__01_11__00_00: throw new NotPossibleException();
            case 0b__01_11__00_01: throw new NotPossibleException();
            case 0b__01_11__00_10: throw new NotPossibleException();
            case 0b__01_11__00_11: throw new NotPossibleException();
            case 0b__01_11__01_00: throw new NotPossibleException();
            case 0b__01_11__01_01: throw new NotPossibleException();
            case 0b__01_11__01_10: throw new NotPossibleException();
            case 0b__01_11__01_11: throw new NotPossibleException();
            case 0b__01_11__10_00: throw new NotPossibleException();
            case 0b__01_11__10_01: throw new NotPossibleException();
            case 0b__01_11__10_10: throw new NotPossibleException();
            case 0b__01_11__10_11: throw new NotPossibleException();
            case 0b__01_11__11_00: throw new NotPossibleException();
            case 0b__01_11__11_01: throw new NotPossibleException();
            case 0b__01_11__11_10: throw new NotPossibleException();
            case 0b__01_11__11_11: throw new NotPossibleException();
            case 0b__10_00__00_00: return InteractionConditions.OneFootBroken_RestFine;
            case 0b__10_00__00_01: return InteractionConditions.OneFootBrokenOneHandSprained_RestFine;
            case 0b__10_00__00_10: return InteractionConditions.OneFootBrokenOneHandSprained_RestFine;
            case 0b__10_00__00_11: return InteractionConditions.BothArmsDislocatedOneFootSprained;
            case 0b__10_00__01_00: return InteractionConditions.OneFootBrokenOneArmDislocated_RestFine;
            case 0b__10_00__01_01: throw new NotPossibleException();
            case 0b__10_00__01_10: return InteractionConditions.OneHandSprainedOneDislocatedOneFootBroken;
            case 0b__10_00__01_11: throw new NotPossibleException();
            case 0b__10_00__10_00: return InteractionConditions.OneFootBrokenOneArmDislocated_RestFine;
            case 0b__10_00__10_01: return InteractionConditions.OneHandSprainedOneDislocatedOneFootBroken;
            case 0b__10_00__10_10: throw new NotPossibleException();
            case 0b__10_00__10_11: throw new NotPossibleException();
            case 0b__10_00__11_00: return InteractionConditions.BothArmsDislocatedOneFootBroken;
            case 0b__10_00__11_01: throw new NotPossibleException();
            case 0b__10_00__11_10: throw new NotPossibleException();
            case 0b__10_00__11_11: throw new NotPossibleException();
            case 0b__10_01__00_00: return InteractionConditions.OneFootSprainedOneFootBroken_HandsFine;
            case 0b__10_01__00_01: return InteractionConditions.OneHandSprainedOneFootSprainedOneFootBroken;
            case 0b__10_01__00_10: return InteractionConditions.OneHandSprainedOneFootSprainedOneFootBroken;
            case 0b__10_01__00_11: return InteractionConditions.BothHandSprainedOneFootSprainedOneBroken;
            case 0b__10_01__01_00: return InteractionConditions.OneArmDislocatedOneFootSprainedOneFootBroken;
            case 0b__10_01__01_01: throw new NotPossibleException();
            case 0b__10_01__01_10: return InteractionConditions.OneArmAndOneFootSprainedOneArmDislocatedOneFootBroken;
            case 0b__10_01__10_01: return InteractionConditions.OneArmAndOneFootSprainedOneArmDislocatedOneFootBroken;
            case 0b__10_01__01_11: throw new NotPossibleException();
            case 0b__10_01__10_00: return InteractionConditions.OneArmDislocatedOneFootSprainedOneFootBroken;
            case 0b__10_01__10_10: throw new NotPossibleException();
            case 0b__10_01__10_11: throw new NotPossibleException();
            case 0b__10_01__11_00: return InteractionConditions.BothHandSprainedOneFootSprainedOneBroken;
            case 0b__10_01__11_01: throw new NotPossibleException();
            case 0b__10_01__11_10: throw new NotPossibleException();
            case 0b__10_01__11_11: throw new NotPossibleException();
            case 0b__10_10__00_00: throw new NotPossibleException();
            case 0b__10_10__00_01: throw new NotPossibleException();
            case 0b__10_10__00_10: throw new NotPossibleException();
            case 0b__10_10__00_11: throw new NotPossibleException();
            case 0b__10_10__01_00: throw new NotPossibleException();
            case 0b__10_10__01_01: throw new NotPossibleException();
            case 0b__10_10__01_10: throw new NotPossibleException();
            case 0b__10_10__01_11: throw new NotPossibleException();
            case 0b__10_10__10_00: throw new NotPossibleException();
            case 0b__10_10__10_01: throw new NotPossibleException();
            case 0b__10_10__10_10: throw new NotPossibleException();
            case 0b__10_10__10_11: throw new NotPossibleException();
            case 0b__10_10__11_00: throw new NotPossibleException();
            case 0b__10_10__11_01: throw new NotPossibleException();
            case 0b__10_10__11_10: throw new NotPossibleException();
            case 0b__10_10__11_11: throw new NotPossibleException();
            case 0b__10_11__00_00: throw new NotPossibleException();
            case 0b__10_11__00_01: throw new NotPossibleException();
            case 0b__10_11__00_10: throw new NotPossibleException();
            case 0b__10_11__00_11: throw new NotPossibleException();
            case 0b__10_11__01_00: throw new NotPossibleException();
            case 0b__10_11__01_01: throw new NotPossibleException();
            case 0b__10_11__01_10: throw new NotPossibleException();
            case 0b__10_11__01_11: throw new NotPossibleException();
            case 0b__10_11__10_00: throw new NotPossibleException();
            case 0b__10_11__10_01: throw new NotPossibleException();
            case 0b__10_11__10_10: throw new NotPossibleException();
            case 0b__10_11__10_11: throw new NotPossibleException();
            case 0b__10_11__11_00: throw new NotPossibleException();
            case 0b__10_11__11_01: throw new NotPossibleException();
            case 0b__10_11__11_10: throw new NotPossibleException();
            case 0b__10_11__11_11: throw new NotPossibleException();
            case 0b__11_00__00_00: return InteractionConditions.BothFeetBroken_HandsFine;
            case 0b__11_00__00_01: return InteractionConditions.OneHandSprainedBothFeetBroken;
            case 0b__11_00__00_10: return InteractionConditions.OneHandSprainedBothFeetBroken;
            case 0b__11_00__00_11: return InteractionConditions.BothHandsSprainedBothFeetBroken;
            case 0b__11_00__01_00: return InteractionConditions.OneArmDislocatedBothFeetBroken;
            case 0b__11_00__10_00: return InteractionConditions.OneArmDislocatedBothFeetBroken;
            case 0b__11_00__01_01: throw new NotPossibleException();
            case 0b__11_00__01_10: return InteractionConditions.OneHandSprainedOneDislocatedBothFeetBroken;
            case 0b__11_00__10_01: return InteractionConditions.OneHandSprainedOneDislocatedBothFeetBroken;
            case 0b__11_00__01_11: throw new NotPossibleException();
            case 0b__11_00__10_10: throw new NotPossibleException();
            case 0b__11_00__10_11: throw new NotPossibleException();
            case 0b__11_00__11_00: return InteractionConditions.BothArmsDislocatedBothFeetBroken;
            case 0b__11_00__11_01: throw new NotPossibleException();
            case 0b__11_00__11_10: throw new NotPossibleException();
            case 0b__11_00__11_11: throw new NotPossibleException();
            case 0b__11_01__00_00: throw new NotPossibleException();
            case 0b__11_01__00_01: throw new NotPossibleException();
            case 0b__11_01__00_10: throw new NotPossibleException();
            case 0b__11_01__00_11: throw new NotPossibleException();
            case 0b__11_01__01_00: throw new NotPossibleException();
            case 0b__11_01__01_01: throw new NotPossibleException();
            case 0b__11_01__01_10: throw new NotPossibleException();
            case 0b__11_01__01_11: throw new NotPossibleException();
            case 0b__11_01__10_00: throw new NotPossibleException();
            case 0b__11_01__10_01: throw new NotPossibleException();
            case 0b__11_01__10_10: throw new NotPossibleException();
            case 0b__11_01__10_11: throw new NotPossibleException();
            case 0b__11_01__11_00: throw new NotPossibleException();
            case 0b__11_01__11_01: throw new NotPossibleException();
            case 0b__11_01__11_10: throw new NotPossibleException();
            case 0b__11_01__11_11: throw new NotPossibleException();
            case 0b__11_10__00_00: throw new NotPossibleException();
            case 0b__11_10__00_01: throw new NotPossibleException();
            case 0b__11_10__00_10: throw new NotPossibleException();
            case 0b__11_10__00_11: throw new NotPossibleException();
            case 0b__11_10__01_00: throw new NotPossibleException();
            case 0b__11_10__01_01: throw new NotPossibleException();
            case 0b__11_10__01_10: throw new NotPossibleException();
            case 0b__11_10__01_11: throw new NotPossibleException();
            case 0b__11_10__10_00: throw new NotPossibleException();
            case 0b__11_10__10_01: throw new NotPossibleException();
            case 0b__11_10__10_10: throw new NotPossibleException();
            case 0b__11_10__10_11: throw new NotPossibleException();
            case 0b__11_10__11_00: throw new NotPossibleException();
            case 0b__11_10__11_01: throw new NotPossibleException();
            case 0b__11_10__11_10: throw new NotPossibleException();
            case 0b__11_10__11_11: throw new NotPossibleException();
            case 0b__11_11__00_00: throw new NotPossibleException();
            case 0b__11_11__00_01: throw new NotPossibleException();
            case 0b__11_11__00_10: throw new NotPossibleException();
            case 0b__11_11__00_11: throw new NotPossibleException();
            case 0b__11_11__01_00: throw new NotPossibleException();
            case 0b__11_11__01_01: throw new NotPossibleException();
            case 0b__11_11__01_10: throw new NotPossibleException();
            case 0b__11_11__01_11: throw new NotPossibleException();
            case 0b__11_11__10_00: throw new NotPossibleException();
            case 0b__11_11__10_01: throw new NotPossibleException();
            case 0b__11_11__10_10: throw new NotPossibleException();
            case 0b__11_11__10_11: throw new NotPossibleException();
            case 0b__11_11__11_00: throw new NotPossibleException();
            case 0b__11_11__11_01: throw new NotPossibleException();
            case 0b__11_11__11_10: throw new NotPossibleException();
            case 0b__11_11__11_11: throw new NotPossibleException();

            default: break;
        }

        throw new Exception("Impossible combination received. ");
    }
}

public enum PlayerInjuries
{
    None = 0,
    All = ~0,

    LeftHandSprained = 1 << 0,
    RightHandSprained = 1 << 1,

    LeftArmDislocated = 1 << 2,
    RightArmDislocated = 1 << 3,

    LeftFootSprained = 1 << 4,
    RightFootSprained = 1 << 5,

    LeftFootBroken = 1 << 6,
    RightFootBroken = 1 << 7
}

public class NotPossibleException : ArgumentException
{
    public override string Message
    {
        get
        {
            return "Player has to many limbs." + base.Message;
        }
    }
}




/*  Old method, possible, meh readable, inefficient:
 
  if (PlayerInjuries == PlayerInjuries.None)
      return InteractionConditions.EverythingFine;

  // every condition not applying is a 0 bit. The equals operand therefore onlyevalutates to true if only this condition is applying
  if (PlayerInjuries == PlayerInjuries.LeftHandSprained ||    
      PlayerInjuries == PlayerInjuries.RightHandSprained)
  {
      return InteractionConditions.OneHandSprained_RestFine;
  }

  if (PlayerInjuries == PlayerInjuries.LeftArmDislocated ||
      PlayerInjuries == PlayerInjuries.RightArmDislocated)
  {
      return InteractionConditions.OneArmDislocated_RestFine;
  }
  return InteractionConditions.Unable;
*/

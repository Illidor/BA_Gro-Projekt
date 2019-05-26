using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinPuzzleCrank : Carryable
{
    [SerializeField]
    private MannequinPuzzleCrankTypes mannequinPuzzleCrankType;

    public override bool Combine(InteractionScript interactionScript, BaseInteractable combinationComponent)
    {
        MannequinPuzzleDoor mannequinPuzzleDoor = combinationComponent as MannequinPuzzleDoor;
        if (mannequinPuzzleDoor == null)    //return if combination component is not mannequin door
            return false;

        switch (mannequinPuzzleCrankType)
        {
            case MannequinPuzzleCrankTypes.RealCrank:
                mannequinPuzzleDoor.UnlockAndOpen();

                GetComponent<AudioSource>().Play();
                return true;

            case MannequinPuzzleCrankTypes.FakeCrank:
                PlayerDeath.playerDeath();

                GetComponent<AudioSource>().Play();
                return true;

            default:
                return false;
        }
    }

    private enum MannequinPuzzleCrankTypes
    {
        none,
        RealCrank,
        FakeCrank
    }

}

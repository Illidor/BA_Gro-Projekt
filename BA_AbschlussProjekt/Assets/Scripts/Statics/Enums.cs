
/// <summary>
/// Namespace for all enums. Needed to be able to use names already used internally.
/// To use, just write "using CustomEnums;" at the top of the file.
/// </summary>
namespace CustomEnums
{

    public enum ObjectParameters
    {
        CarryOneHand,
        PullOneHand,
        CarryTwoHands,
        PullTwoHands
    }

    public enum InteractionConditions
    {
        Unable          = 0, // Custom name for "Nothing" option
        All             = ~0, // Custom name for "Everything" option
        EverythingFine  = 1 << 0,

        // All feet fine
        OneHandSprained_RestFine                     = 1 <<  1,
        OneArmDislocated_RestFine                    = 1 <<  2,
        BothHandsSprained_FeetFine                   = 1 <<  3,
        BothArmsDislocated_FeetFine                  = 1 <<  4,
        OneArmDislocatedOneSprained_FeetFine         = 1 <<  5,
                                                             
        // All hands Fine                                    
        OneFootSprained_RestFine                     = 1 <<  6,
        OneFootBroken_RestFine                       = 1 <<  7,
        BothFeetSprained_HandsFine                   = 1 <<  8,
        BothFeetBroken_HandsFine                     = 1 <<  9,
        OneFootSprainedOneFootBroken_HandsFine       = 1 << 10,
                                                     
        // Mixed                                     
        OneFootAndOneHandSprained_RestFine           = 1 << 11,
        OneFootBrokenOneHandSprained_RestFine        = 1 << 12,
        OneFootSprainedOneArmDislocated_RestFine     = 1 << 13,
        OneFootBrokenOneArmDislocated_RestFine       = 1 << 14,
        //BothArmsDislocatedBothFeetBroken             = 1 << 15,       //space ran out
        //BothArmsDislocatedOneFootBroken              = 1 << 16,       //space ran out
        //BothArmsDislocatedOneFootSprained            = 1 << 17,       //space ran out
        //BothArmsDislocatedBothFeetSprained           = 1 << 18,       //space ran out
        BothHandsAndBothFeetSprained                 = 1 << 15,
        BothHandsSprainedOneFootSprained             = 1 << 16,
        BothHandsSprainedBothFeetBroken              = 1 << 17,
        BothHandsSprainedOneFootBroken               = 1 << 18,
        OneHandSprainedOneDislocatedOneFootSprained  = 1 << 19,
        OneHandSprainedOneDislocatedOneFootBroken    = 1 << 20,
        OneHandSprainedOneDislocatedBothFeetSprained = 1 << 21,
        OneHandSprainedOneDislocatedBothFeetBroken   = 1 << 22,
        OneHandSprainedBothFeetSprained              = 1 << 23,
        OneArmDislocatedBothFeetSprained             = 1 << 24,
        OneHandSprainedBothFeetBroken                = 1 << 25,
        OneHandSprainedOneFootSprainedOneFootBroken  = 1 << 26,
        OneArmDislocatedOneFootSprainedOneFootBroken = 1 << 27,
        BothHandSprainedOneFootSprainedOneBroken     = 1 << 28,
        BothArmsDislocatedOneFootSprainedOneBroken   = 1 << 29,
        //OneArmAndOneFootSprainedOneArmDislocatedOneFootBroken = 1 << 34,      //space ran out
        OneArmDislocatedBothFeetBroken               = 1 << 30,

        BothArmsDislocatedBothFeetBroken                      = 1 << 31,  // to reimplement them right, you need to make the enumFlagDrawer work with 
        BothArmsDislocatedOneFootBroken                       = 1 << 31,  // long type enums. "public enum InteractionConditions : long".
        BothArmsDislocatedOneFootSprained                     = 1 << 31,  // No idea why is doesn't, but I don't have time for this now. //TODO
        BothArmsDislocatedBothFeetSprained                    = 1 << 31,  //
        OneArmAndOneFootSprainedOneArmDislocatedOneFootBroken = 1 << 31,  //


        // Easy but not useable solution
        // Unable = 0,
        // HealthyAndHealthy = 1 << 0,
        // HealthyAndSprained = 1 << 1,
        // HealthyAndDislocated = 1 << 2,
        // SprainedAndSprained = 1 << 3,
        // SprainedAndDislocated = 1 << 4,
        // DislocatedAndDislocated = 1 << 5,
        // All = ~0,
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////
// INSTRUCTIONS FOR ADDING PLAYER ANIMATION TRIGGERS
////////////////////////////////////////////
/*
 * 1. Add a new value to the SpecialThrowItem enumeration that represents the item
 * 2. Add an entry to the TriggerNames dictionary in the PlayerAnimationTriggers.Initialize method
 *      - Use your new enumeration value as the key 
 *      - Use the name of the trigger set in the player animation controller for the respective animation as the key
*/

/// <summary>
/// An enumeration of items with special player throw animations
/// </summary>
public enum ThrowAnimation
{
    DEFAULT,
    DAGGER,
    SPEAR,
    WARHAMMER_SPIN,
    WARHAMMER_RELEASE,
    DECORATION_THROW
}

/// <summary>
/// A class to hold the dictionary of player animation trigger names
/// </summary>
public static class PlayerAnimationTriggers
{
    /// <summary>
    /// Whether the trigger dictionary has been initialized
    /// </summary>
    public static bool Initialized { get; private set; }

    /// <summary>
    /// Dictionary of player animator trigger names
    /// </summary>
    public static Dictionary<ThrowAnimation, string> TriggerNames { get; private set; }

    /// <summary>
    /// Initializes the animation trigger dictionary
    /// </summary>
    public static void Initialize()
    {
        if (!Initialized)
        {
            Initialized = true;

            // Create dictionary
            TriggerNames = new Dictionary<ThrowAnimation, string>();

            // Add trigger names
            TriggerNames = new Dictionary<ThrowAnimation, string>();
            TriggerNames.Add(ThrowAnimation.DEFAULT, "Throw");
            TriggerNames.Add(ThrowAnimation.DAGGER, "DaggerThrow");
            TriggerNames.Add(ThrowAnimation.SPEAR, "SpearThrow");
            TriggerNames.Add(ThrowAnimation.WARHAMMER_SPIN, "WarhammerSpin");
            TriggerNames.Add(ThrowAnimation.WARHAMMER_RELEASE, "WarhammerRelease");
            TriggerNames.Add(ThrowAnimation.DECORATION_THROW, "RoomItemThrow");
        }
    }
}

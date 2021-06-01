using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class to provide global access to config data
/// </summary>
public static class ConfigurationUtils
{
    static ConfigurationData configData;    // configuration data object
    static bool initialized = false;                // Whether the config utils have been initialized

    #region Properties

    /// <summary>
    /// Whether the config utils have been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Gets the player move speed
    /// </summary>
    public static float PlayerMoveSpeed
    {
        get { return configData.PlayerMoveSpeed; }
    }

    /// <summary>
    /// Medium speed for projectiles
    /// </summary>
    public static float MediumProjectileSpeed
    {
        get { return configData.MediumProjectileSpeed; }
    }

    /// <summary>
    /// Fast speed for projectiles
    /// </summary>
    public static float FastProjectileSpeed
    {
        get { return configData.FastProjectileSpeed; }
    }

    /// <summary>
    /// Distance the dagger can travel
    /// </summary>
    public static float DaggerTravelDistance
    {
        get { return configData.DaggerTravelDistance; }
    }

    /// <summary>
    /// Dictionary of weapon spawn locations in relation to the player
    /// </summary>
    public static Dictionary<WeaponName, Vector2> WeaponSpawnLocations
    {
        get { return configData.WeaponSpawnLocations; }
    }

    /// <summary>
    /// Names of animations for the player using weapons
    /// </summary>
    public static Dictionary<WeaponName, string> PlayerWeaponAnimationNames
    {
        get { return configData.PlayerWeaponAnimationNames; }
    }

    #endregion

    /// <summary>
    /// Initializees the configuration utils
    /// </summary>
    public static void Initialize()
    {
        initialized = true;
        configData = new ConfigurationData();
    }
}

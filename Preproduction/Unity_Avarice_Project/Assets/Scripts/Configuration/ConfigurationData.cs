using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that contains all configuration data for the game
/// </summary>
public class ConfigurationData
{
    // TODO: Configuration data file name

    // Configuration data w/ default values
    float playerMoveSpeed = 35;
    float mediumProjectileSpeed = 8f;
    float fastProjectileSpeed = 10f;
    float daggerTravelDistance = 8f;
    Dictionary<WeaponName, Vector2> weaponSpawnLocations;
    Dictionary<WeaponName, string> playerWeaponAnimationNames;


    #region Public Properties

    /// <summary>
    /// The player's move speed
    /// </summary>
    public float PlayerMoveSpeed
    {
        get { return playerMoveSpeed; }
    }

    /// <summary>
    /// Medium speed for projectiles
    /// </summary>
    public float MediumProjectileSpeed
    {
        get { return mediumProjectileSpeed; }
    }

    /// <summary>
    /// Fast speed for projectiles
    /// </summary>
    public float FastProjectileSpeed
    {
        get { return fastProjectileSpeed; }
    }

    /// <summary>
    /// Distance the dagger can travel
    /// </summary>
    public float DaggerTravelDistance
    {
        get { return daggerTravelDistance; }
    }

    /// <summary>
    /// Dictionary of weapon spawn locations in relation to the player
    /// </summary>
    public Dictionary<WeaponName, Vector2> WeaponSpawnLocations
    {
        get { return weaponSpawnLocations; }
    }

    /// <summary>
    /// Names of animations for the player using weapons
    /// </summary>
    public Dictionary<WeaponName, string> PlayerWeaponAnimationNames
    {
        get { return playerWeaponAnimationNames; }
    }

    #endregion

    #region Constructor

    public ConfigurationData()
    {
        // TODO: Read config data in from csv file

        weaponSpawnLocations = new Dictionary<WeaponName, Vector2>();
        weaponSpawnLocations.Add(WeaponName.DAGGER, new Vector2(0.28f, 1f));
        weaponSpawnLocations.Add(WeaponName.RAGNAROK, new Vector2(0.28f, 1f));
        weaponSpawnLocations.Add(WeaponName.CROSSBOW, new Vector2(0.28f, 1f));

        playerWeaponAnimationNames = new Dictionary<WeaponName, string>();
        playerWeaponAnimationNames.Add(WeaponName.DAGGER, "Dagger");
        playerWeaponAnimationNames.Add(WeaponName.RAGNAROK, "Ragnarok");
        playerWeaponAnimationNames.Add(WeaponName.CROSSBOW, "Crossbow");
    }

    #endregion
}

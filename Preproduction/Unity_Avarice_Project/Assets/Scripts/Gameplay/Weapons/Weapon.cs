using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for weapons
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    public WeaponName Name { get; protected set; }  // Name of the weapon
    public WeaponType Type { get; protected set; }  // Type of weapon
    public float Damage { get; protected set; }     // Damage dealt by the weapon

    /// <summary>
    /// Handles weapon attack behavior
    /// </summary>
    public abstract void Attack();
}

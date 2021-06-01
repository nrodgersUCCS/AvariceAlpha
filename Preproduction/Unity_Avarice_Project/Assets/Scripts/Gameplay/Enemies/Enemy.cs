using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for enemies
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    public float Damage { get; protected set; }     // Damage dealt by the enemy
    public float Health { get; protected set; }     // Enemy health

    /// <summary>
    /// Enemy idle behavior
    /// </summary>
    public abstract void Idle();

    /// <summary>
    /// Enemy aggro behavior
    /// </summary>
    public abstract void Aggro();

    /// <summary>
    /// Enemy attack behavior
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// Deal damage to the enemy
    /// </summary>
    /// <param name="damage">Amount of damage to do</param>
    public abstract void TakeDamage(float damage);

    /// <summary>
    /// Enemy death behavior
    /// </summary>
    public virtual void Death()
    {
        // Enemy death animation
        GameObject deathPoof = Instantiate(Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/EnemyDeathPoof"), transform);
        deathPoof.transform.parent = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// An enumeration of damage types
/// </summary>
public enum DamageType
{
    NORMAL,
    FLAME,
    FROST
}

/// <summary>
/// A class to represent a weapon
/// </summary>
public class Weapon : ThrowableItem
{
    // Damage vars
    protected float TEMPERED_MULTIPLIER;            // The multiplier for tempered damage

    // Damage vars
    [Tooltip("This must be true to set weapon types manually")]
    [SerializeField]
    protected bool overrideType;                    // Whether the weapon's type is written over in the inspector
    public DamageType Type;                         // The weapon's damage type
    public bool IsTempered;                         // Whether the weapon is tempered
    protected float DAMAGE;                         // The weapon's damage
    protected float FLAME_POWER;                    // The flame damage done per tick by flame weapons
    protected int FROST_POWER;                      // The slow counters added per hit by frost weapons

    /// <summary>
    /// The rarity of the weapon
    /// </summary>
    public Rarity Rarity { get; protected set; }

    /// <summary>
    /// The glow color of the item
    /// </summary>
    public Color GlowColor { get; protected set; }

    /// <summary>
    /// Whether the weapon is legendary
    /// </summary>
    public bool IsLegendary { get; protected set; }

    /// <summary>
    /// Called each frame
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // If the type is being overridden
        if (overrideType)
        {
            // Set the weapon's type from the inspector
            SetType(IsTempered, Type, IsLegendary);
        }

        // Otherwise, set the type to normal
        else
        {
            SetType(isTempered: false, DamageType.NORMAL);
        }
    }

    /// <summary>
    /// Handles behavior when the weapon lands after being thrown
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Land(float delay)
    {
        // Wait for travel time
        yield return new WaitForSeconds(delay);

        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();

        // Make item grabbable again
        canBePickedUp = true;

        if (rb2d != null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }

        gameObject.layer = LayerMask.NameToLayer("ItemPickups");
        collider.isTrigger = true;
        collider.sharedMaterial = null;
        InFlight = false;
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="col">Collision info</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        TilemapCollider2D tMCollider = collision.gameObject.GetComponent<TilemapCollider2D>();
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
        Decoration decorationScript = collision.gameObject.GetComponent<Decoration>();

        // If colliding with a tilemap
        if (tMCollider != null || decorationScript != null)
        {
            // Play item wall hit sound
            AudioManager.Play(wallHitSound, audioSource);
        }

        // If colliding with enemy
        else if (enemyScript != null)
        {
            // Play enemy hit sound
            AudioManager.Play(enemyHitSound, audioSource);

            // Damage enemy
            float damage = DAMAGE * player.GetComponent<PlayerInventory>().GreedMultiplier;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, Type, FLAME_POWER, FROST_POWER);
        }
    }

    /// <summary>
    /// Set's the weapon's appearance and damage variables based on type
    /// </summary>
    /// <param name="isTempered">Whether the weapon is tempered</param>
    /// <param name="type">The weapon's damage type</param>
    /// <param name="isLegendary">Whether the weapon is legendary</param>
    public void SetType(bool isTempered, DamageType type, bool isLegendary = false)
    {
        // Determine rarity
        int rarity = 0;
        if (isLegendary)
        {
            rarity = 3;
        }
        else
        {
            if (isTempered)
                rarity++;
            if (type != DamageType.NORMAL)
                rarity++;
        }

        // Set constants
        Rarity = (Rarity)rarity;
        WeaponValues container = (WeaponValues)Constants.Values(containerType);
        DAMAGE = container.Damage;
        IsTempered = isTempered;
        Type = type;
        IsLegendary = isLegendary;

        if (type == DamageType.FLAME)
            FLAME_POWER = container.FlamePower;
        else
            FLAME_POWER = 0f;
        if (type == DamageType.FROST)
            FROST_POWER = container.FrostPower;
        else
            FROST_POWER = 0;

        // Set tempered damage
        if (isTempered)
        {
            TEMPERED_MULTIPLIER = ((WeaponVariantValues)Constants.Values(ContainerType.WEAPON_VARIANT)).TemperedMultiplier;
            DAMAGE *= TEMPERED_MULTIPLIER;
        }

        // Set glow color
        WeaponVariantValues glowContainer = (WeaponVariantValues)Constants.Values(ContainerType.WEAPON_VARIANT);
        if (isTempered)
        {
            if (Type == DamageType.FLAME)
                GlowColor = glowContainer.TemperedFlameGlow;
            else if (Type == DamageType.FROST)
                GlowColor = glowContainer.TemperedFrostGlow;
            else if (!IsLegendary)
                GlowColor = glowContainer.TemperedGlow;
        }
        else if (isLegendary)
        {
            GlowColor = glowContainer.LegendaryGlow;
        }
        else
        {
            if (Type == DamageType.FLAME)
                GlowColor = glowContainer.FlameGlow;
            else if (Type == DamageType.FROST)
                GlowColor = glowContainer.FrostGlow;
            else
                GlowColor = Color.white;
        }
        highlightMaterial.SetColor("GlowColor", GlowColor);
    }
}

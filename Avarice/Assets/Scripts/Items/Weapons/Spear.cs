using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    // Constants
    private float VELOCITY_MULTIPLIER;                  // How much the velocity of the spear should change every frame
    private float VELOCITY_MULTIPLIER_HIT;              // How much the velocity of the spear should change when it hits an enemy

    // Variables
    private List<Enemy> hitEnemies;                     // Enemies that have been hit by the spear
    GameObject child;                                   // Child of the spear
    [SerializeField]

    /// <summary>
    /// The range of the spear when thrown
    /// </summary>
    public override float Range { get { return ((SpearValues)Constants.Values(ContainerType.SPEAR)).Range; } }


    /// <summary>
    /// Called before start
    /// </summary>
    protected override void Awake()
    {
        // Call parent method
        base.Awake();

        // Set constants
        VELOCITY_MULTIPLIER = ((SpearValues)Constants.Values(ContainerType.SPEAR)).VelocityMultiplier;
        VELOCITY_MULTIPLIER_HIT = ((SpearValues)Constants.Values(ContainerType.SPEAR)).VelocityMultiplierHit;

        // Set override values
        throwMaterial = Resources.Load<PhysicsMaterial2D>("Materials/PhysicsMaterials/Spear");
        collider = GetComponent<CapsuleCollider2D>();
        ThrowAnimation = ThrowAnimation.SPEAR;
        PickupSound = AudioClipName.PICK_UP_SOUND;
        if (IsTempered)
            throwSound = AudioClipName.TEMPERED_SPEAR_THROW;
        else if (Type == DamageType.FLAME)
            throwSound = AudioClipName.FIRE_SPEAR_THROW;
        else if (Type == DamageType.FROST)
            throwSound = AudioClipName.FROST_SPEAR_THROW;
        else
            throwSound = AudioClipName.SPEAR_THROW;
        wallHitSound = AudioClipName.SPEAR_HIT_WALL;
        enemyHitSound = AudioClipName.SPEAR_HIT_ENEMY;
        SprHUD = Resources.Load<Sprite>("Sprites/UI/spr_spearIcon");
        ShadowSprHUD = Resources.Load<Sprite>("Sprites/UI/spr_spearIconShadow");

        // Create list of enemies
        hitEnemies = new List<Enemy>();

        // Get child
        child = gameObject.transform.GetChild(1).gameObject;
    }

    /// <summary>
    /// Slow down the spear over time
    /// </summary>
    protected override void Update()
    {
        base.Update();

        // If in flight
        if (InFlight)
        {
            // Slow down the spear
            gameObject.GetComponent<Rigidbody2D>().velocity *= VELOCITY_MULTIPLIER;
        }
    }

    /// <summary>
    /// Detaches the spear from the player and gives it a force
    /// </summary>
    public override void Detach()
    {
        base.Detach();

        // Change the layer of the spear to Spear so it doesn't collide with enemies
        gameObject.layer = LayerMask.NameToLayer("Spear");

        // Reenable the child's collider
        child.GetComponent<Collider2D>().enabled = true;
        child.GetComponent<Collider2D>().isTrigger = true;

        // Set travel time for spear
        StartCoroutine(Land(TRAVEL_TIME));
    }

    /// <summary>
    /// Makes the object land after its travel time
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator Land(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (InFlight)
        {
            // Make item grabbable again
            canBePickedUp = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            gameObject.layer = LayerMask.NameToLayer("ItemPickups");
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            collider.isTrigger = true;
            collider.sharedMaterial = null;
            InFlight = false;

            // Clear the list of enemies
            hitEnemies.Clear();
        }
    }


    /// <summary>
    /// Called upon the child of the spear making a collision
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();

        // If colliding with an enemy that hasn't already been damaged by the spear
        if (enemyScript != null && InFlight && !hitEnemies.Contains(enemyScript))
        {
            // Add enemy to the list
            hitEnemies.Add(enemyScript);

            // Play enemy hit sound
            AudioManager.Play(enemyHitSound, audioSource);

            // Damage enemy
            float damage = DAMAGE * player.GetComponent<PlayerInventory>().GreedMultiplier;
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, Type, FLAME_POWER, FROST_POWER);

            // Slow down the spear
            gameObject.GetComponent<Rigidbody2D>().velocity *= VELOCITY_MULTIPLIER_HIT;
        }

        // If colliding with a decoration
        Decoration decoration = collision.gameObject.GetComponent<Decoration>();
        if (decoration != null && InFlight)
        {
            // Stop movement if decoration is not destroyed
            if (decoration.CurrentHealth > 1)
            {
                StartCoroutine(Land(0f));

                // Move into the wall slightly
                transform.position += -transform.up * 0.5f;
            }

            // Otherwise, slow movement down
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity *= VELOCITY_MULTIPLIER_HIT;
            }

            // Damage decoration
            decoration.DamageItem(collision.transform.position);
        }
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="col">Collision info</param>
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Play spear hit wall sound
        AudioManager.Play(wallHitSound, audioSource);

        // Stop movement if spear hits wall
        StartCoroutine(Land(0f));

        // Move into the wall slightly
        transform.position += -transform.up * 0.5f;
    }
}
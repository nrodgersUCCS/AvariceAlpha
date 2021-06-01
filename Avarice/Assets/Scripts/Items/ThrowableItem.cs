using System.Collections;
using UnityEngine;

/// <summary>
/// This script is used to be a subclass of items that can be thrown.
/// </summary>
public abstract class ThrowableItem : Item
{
    // Physics vars
    protected float THROW_SPEED;                    // The speed at which the item is thrown
    protected float TRAVEL_TIME;                    // The travel time of the object

    // Physics vars 
    protected new Collider2D collider;              // The item's collider
    protected PhysicsMaterial2D throwMaterial;      // The objects physics material used for flying

    // Audio vars
    protected AudioClipName throwSound;             // The item's throw sound
    protected AudioClipName wallHitSound;           // The item's wall hit sound
    protected AudioClipName enemyHitSound;          // The item's enemy hit sound

    /// <summary>
    /// The player throw animation associated with this object
    /// </summary>
    public ThrowAnimation ThrowAnimation { get; protected set; }

    /// <summary>
    /// The weight of the object
    /// </summary>
    public float Weight { protected set; get; }

    /// <summary>
    /// The range of the object when thrown
    /// </summary>
    public virtual float Range { get { return TRAVEL_TIME * THROW_SPEED; } }

    /// <summary>
    /// Whether the item is in flight
    /// </summary>
    public bool InFlight { get; protected set; }

    /// <summary>
    /// Called before start
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        // Set constants
        ThrowableItemValues container = (ThrowableItemValues)Constants.Values(containerType);
        THROW_SPEED = container.ThrowSpeed;
        TRAVEL_TIME = container.TravelTime;
        Weight = container.Weight;

        // Initialization
        collider = GetComponent<Collider2D>();
        throwMaterial = Resources.Load<PhysicsMaterial2D>("Materials/PhysicsMaterials/ThrownObject");
        InFlight = false;

        // Default sounds
        throwSound = AudioClipName.DAGGER_THROW;
        wallHitSound = AudioClipName.DAGGER_HIT_WALL;
        enemyHitSound = AudioClipName.DAGGER_HIT_ENEMY2;

        // Default throw animation
        ThrowAnimation = ThrowAnimation.DAGGER;
    }

    /// <summary>
    /// Optional method for behavior upon being picked up
    /// </summary>
    public virtual void PickedUp() { }

    /// <summary>
    /// Detaches the item from the player and gives it a force
    /// </summary>
    public virtual void Detach()
    {
        // Detach from player
        Vector2 throwDirection = -transform.root.up;
        transform.parent = null;
        InFlight = true;
        canBePickedUp = false;

        // Add throw force
        Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
        if (rb2d == null)
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        rb2d.isKinematic = false;
        rb2d.AddForce(throwDirection * THROW_SPEED, ForceMode2D.Impulse);

        // Enable collider
        collider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("PlayerAttacks");
        collider.isTrigger = false;

        // Set throw material
        collider.sharedMaterial = throwMaterial;

        // Play item throw sound
        AudioManager.Play(throwSound, audioSource);

        // Start timer for landing
        StartCoroutine(Land(TRAVEL_TIME));
    }

    /// <summary>
    /// Makes the object land after its travel time
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerator Land(float delay);


    /// <summary>
    /// Public access to the Land coroutine for special cases
    /// </summary>
    /// <param name="delay"></param>
    public virtual void SpecialLand(float delay)
    {
        StartCoroutine(Land(delay));
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="col">Collision info</param>
    protected abstract void OnCollisionEnter2D(Collision2D collision);
}
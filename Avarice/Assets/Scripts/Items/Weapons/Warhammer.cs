using UnityEngine;

/// <summary>
/// A warhammer that can be spun and thrown
/// </summary>
public class Warhammer : Weapon
{
    private AudioClipName swingSound;                                                   // Sound for when the warhammer swing
    private ThrowAnimation releaseAnimation;                                            // Animation for player releasing the warhammer
    public PlayerMovement spinSlowDown;                                                 // Access the PlayerMovement script

    // For changing the warhammer's rotation during spin
    private Transform spriteObj;                                                        // The child object containing the sprite
    private Transform trailObj;                                                         // The child object containing the trail renderer
    private BoxCollider2D warhammerCollider;                                            // The warhammer's collider
    private Vector3 startRotation;                                                      // Starting rotation of the sprite
    private Vector3 spriteStartPos;                                                     // Starting position of the sprite
    private Vector3 trailStartPos;                                                      // Starting position of the trail renderer
    private Vector2 startColliderSize;                                                  // Starting size of the collider
    private Vector2 startCollideroffset;                                                // Starting offset of the collider

    // Constants for updating rotation
    private Vector3 thrownRotation = new Vector3(0, 0, 180);                            // Starting rotation of the sprite
    private Vector3 spriteThrownPos = new Vector3(0, -0.3f, 0);                         // Starting position of the sprite
    private Vector3 trailThrownPos = new Vector3(0, -0.67f, 0);                         // Starting position of the trail renderer
    private Vector2 thrownColliderSize = new Vector2(0.387252927f, 0.612951696f);       // Starting size of the collider
    private Vector2 thrownCollideroffset = new Vector2(0.00185742974f, -0.517050505f);  // Starting offset of the collider

    /// <summary>
    /// Whether the warhammer can be thrown
    /// </summary>
    public bool CanThrow { private get; set; }

    /// <summary>
    /// The range of the spear when thrown
    /// </summary>
    public override float Range { get { return ((WarhammerValues)Constants.Values(ContainerType.WARHAMMER)).Range; } }

    /// <summary>
    /// Start Method
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Gets PlayerMovement script from player object;
        spinSlowDown = player.GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Sets warhammer override values
    /// </summary>
    protected override void Awake()
    {
        // Call parent method
        base.Awake();

        // Set override values
        // Physics
        throwMaterial = Resources.Load<PhysicsMaterial2D>("Materials/PhysicsMaterials/Warhammer");

        // Animation
        ThrowAnimation = ThrowAnimation.WARHAMMER_SPIN;
        releaseAnimation = ThrowAnimation.WARHAMMER_RELEASE;

        // Sounds
        PickupSound = AudioClipName.PICK_UP_SOUND;
        if (IsTempered)
        {
            throwSound = AudioClipName.TEMPERED_WARHAMMER_THROW;
            swingSound = AudioClipName.WARHAMMER_SPIN;
        }
        else if (Type == DamageType.FLAME)
        {
            throwSound = AudioClipName.FIRE_WARHAMMER_THROW;
            swingSound = AudioClipName.FIRE_WARHAMMER_SPIN;
        }
        else if (Type == DamageType.FROST)
        {
            throwSound = AudioClipName.FROST_WARHAMMER_THROW;
            swingSound = AudioClipName.FROST_WARHAMMER_SPIN;
        }
        else
        {
            throwSound = AudioClipName.WARHAMMER_THROW;
            swingSound = AudioClipName.WARHAMMER_SPIN;
        }
        enemyHitSound = AudioClipName.WARHAMMER_ENEMY_HIT;
        wallHitSound = AudioClipName.WARHAMMER_WALL_HIT;

        // Get child objects
        spriteObj = GetComponentInChildren<SpriteRenderer>().transform;
        trailObj = GetComponentInChildren<TrailRenderer>().transform;
        warhammerCollider = (BoxCollider2D)collider;

        // Get start values
        startRotation = spriteObj.localEulerAngles;
        spriteStartPos = spriteObj.localPosition;
        trailStartPos = trailObj.localPosition;
        startColliderSize = warhammerCollider.size;
        startCollideroffset = warhammerCollider.offset;

        // Get HUD sprites
        SprHUD = Resources.Load<Sprite>("Sprites/UI/spr_hammerIcon");
        ShadowSprHUD = Resources.Load<Sprite>("Sprites/UI/spr_hammerIconShadow");

        // Make sure the warhammer can't be thrown until it is picked up
        CanThrow = false;
    }

    /// <summary>
    /// Transition to warhammer throw on mouse up
    /// </summary>
    protected override void Update()
    {
        base.Update();

        // On LMB up
        if (Input.GetMouseButtonUp(0) && CanThrow && PlayerMovement.ControlsLocked != true)
        {
            // Check if currently held by the player
            if (transform.root.tag == "Player")
            {
                // Trigger throw animation
                spinSlowDown.IsSpinning = false;
                Animator anim = transform.root.GetComponent<Animator>();
                anim.SetTrigger(PlayerAnimationTriggers.TriggerNames[releaseAnimation]);
                anim.applyRootMotion = true;

                // Make sure the warhammer can't be thrown until it is picked up again
                CanThrow = false;
            }
        }
    }

    /// <summary>
    /// Detaches the warhammer from the player and gives it a force
    /// </summary>
    public override void Detach()
    {
        // Make player face mouse
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.root.position;
        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90;
        transform.root.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Call parent method
        base.Detach();
    }

    /// <summary>
    /// Resets warhammer rotation when picked up
    /// </summary>
    public override void PickedUp()
    {
        base.PickedUp();
        spriteObj.localEulerAngles = startRotation;
        spriteObj.localPosition = spriteStartPos;
        trailObj.localPosition = trailStartPos;
        warhammerCollider.size = startColliderSize;
        warhammerCollider.offset = startCollideroffset;
    }

    /// <summary>
    /// Turns the warhammer's collider on
    /// </summary>
    public void TurnColliderOn()
    {
        // Allow the warhammer to be thrown
        CanThrow = true;

        // Give the hammer a kinematic rigidbody
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rb2d.isKinematic = true;

        // Enables warhammer collider
        spinSlowDown.IsSpinning = true;
        collider.enabled = true;
        collider.isTrigger = false;
        gameObject.layer = LayerMask.NameToLayer("PlayerAttacks");

        // Change sprite to align correctly for spin
        spriteObj.localEulerAngles = thrownRotation;
        spriteObj.localPosition = spriteThrownPos;
        trailObj.localPosition = trailThrownPos;
        warhammerCollider.size = thrownColliderSize;
        warhammerCollider.offset = thrownCollideroffset;

        // Lock the player's rotation control
        player.GetComponent<PlayerMovement>().LockRotationControl();
    }

    /// <summary>
    /// Plays the warhammer swing sound
    /// </summary>
    public void PlaySwingSound()
    {
        AudioManager.Play(swingSound, audioSource);
    }
}


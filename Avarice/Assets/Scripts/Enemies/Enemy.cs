using System.Collections;
using UnityEngine;

/// <summary>
/// A parent class for all enemies
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    // Player
    protected GameObject player;                        // The player object

    // Components
    protected AudioSource audioSource;                  // Enemy audio source
    protected Animator animator;                        // Enemy animator
    protected new Collider2D collider;                  // Enemy collider
    protected SpriteRenderer spriteRenderer;            // Enemy srite renderer
    protected Rigidbody2D rb2d;                         // Enemy rigidbody
    protected GameObject bloodSplatter;                 // Enemy blood splatter
    protected GameObject ashParticles;                  // Enemy ash death
    protected GameObject fireParticles;              // Enemy flaming particles

    // Movement vars
    protected float PASSIVE_MOVE_SPEED;                 // Current move speed while idle/passive
    protected float AGGRESSIVE_MOVE_SPEED;              // Current move speed when aggresive
    protected float AGGRO_RANGE;                        // Range at which the enemy will become aggresive
    protected float FLEE_RANGE;                         // Range at which the enemy will flee from the player

    // Attack vars
    protected float ATTACK_DELAY;                       // Delay between each attack
    protected float ATTACK_RANGE;                       // Range of attacks

    // Damage vars
    private float REDFLASH_OPACITY;                     // Opacity of the damage flash
    private float REDFLASH_TIME;                        // Duration of the damage flash
    protected float FLAME_RESISTANCE;                   // The amount of flame tick damage mitigated
    protected int FREEZE_THRESHOLD;                     // The number of slow counters needed to complletely freeze the enemy
    protected int currentSlowCount;                     // The number of slow counters currently on the enemy
    protected float BURN_DURATION;                      // The duration of the flame effect on the enemy;
    protected float FREEZE_DURATION;                    // The duration for which the enemy is frozen
    protected bool isBurning;                           // Whether the enemy is taking burn damage
    protected bool isFrozen;                            // Whether the enemy is frozen
    protected bool isSlowed;                            // Whether the enemy is slowed by frost
    protected float startPassiveSpeed;                  // Starting passive move speed
    protected float startAggressiveSpeed;               // Starting aggressive move speed
    protected RigidbodyConstraints2D startConstraints;  // Starting rigidbody constraints
    protected float startAnimSpeed;                     // Starting animation speed
    protected Color startColor;                         // Starting sprite color
    Coroutine damageCoroutine;                          // Coroutine for weapon effects

    // Big variant vars
    public bool IsBigEnemy;                             // Makes enemy a big enemy; set in inspector
    protected float SCALE_MULTIPLIER;                   // Scale multiplier for large enemy variants
    protected float HEALTH_MULTIPLIER;                  // Health multiplier for large enemy variants

    /// <summary>
    /// Damage dealt by the enemy
    /// </summary>
    public float Damage { get; protected set; }

    /// <summary>
    /// Max enemy health
    /// </summary>
    public float MaxHealth { get; protected set; }

    /// <summary>
    /// Current enemy health
    /// </summary>
    public float CurrentHealth { get; protected set; }

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected virtual void Awake()
    {
        // Get components
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        // load blood splatter prefab
        bloodSplatter = Resources.Load<GameObject>("Prefabs/ParticleEffects/BloodSplatter");

        // load ash prefab
        ashParticles = Resources.Load<GameObject>("Prefabs/ParticleEffects/AshParticles");

        // load ash prefab
        fireParticles = Resources.Load<GameObject>("Prefabs/ParticleEffects/FireParticles");

        // Set global enemy vars
        REDFLASH_OPACITY = ((EnemyValues)Constants.Values(ContainerType.ENEMY)).RedFlashOpacity;
        REDFLASH_TIME = ((EnemyValues)Constants.Values(ContainerType.ENEMY)).RedFlashTime;
        FREEZE_DURATION = ((WeaponVariantValues)Constants.Values(ContainerType.WEAPON_VARIANT)).FreezeDuration;
        BURN_DURATION = ((WeaponVariantValues)Constants.Values(ContainerType.WEAPON_VARIANT)).BurnDuration;
        currentSlowCount = 0;
        isSlowed = false;
        isFrozen = false;

        // Get enemy type
        ContainerType containerType = ContainerType.ENEMY;
        if (this is AlgorMortis) containerType = ContainerType.ALGOR_MORTIS;
        else if (this is BanditRanger) containerType = ContainerType.BANDIT;
        else if (this is Brute) containerType = ContainerType.BRUTE;
        else if (this is CorpseEater) containerType = ContainerType.CORPSE_EATER;
        else if (this is DemonKing) containerType = ContainerType.DEMON_KING;
        else if (this is Cherub) containerType = ContainerType.CHERUB;
        else if (this is Shadowblade) containerType = ContainerType.SHADOWBLADE;
        else if (this is SkeloDevil) containerType = ContainerType.SKELODEVIL;
        else if (this is Hermit) containerType = ContainerType.HERMIT;

        // Set inherited vars
        EnemyValues container = (EnemyValues)Constants.Values(containerType);
        MaxHealth = container.MaxHealth;
        CurrentHealth = MaxHealth;
        FLAME_RESISTANCE = container.FlameResistance;
        FREEZE_THRESHOLD = container.FreezeThreshold;
        PASSIVE_MOVE_SPEED = container.PassiveMoveSpeed;
        AGGRESSIVE_MOVE_SPEED = container.AggressiveMoveSpeed;
        startPassiveSpeed = PASSIVE_MOVE_SPEED;
        startAggressiveSpeed = AGGRESSIVE_MOVE_SPEED;
        AGGRO_RANGE = container.AggroRange;
        FLEE_RANGE = container.FleeRange;
        ATTACK_DELAY = container.AttackDelay;
        ATTACK_RANGE = container.AttackRange;
        SCALE_MULTIPLIER = container.ScaleMultiplier;
        HEALTH_MULTIPLIER = container.HealthMultiplier;

        // Set starting vars for damage effects
        startAggressiveSpeed = AGGRESSIVE_MOVE_SPEED;
        startPassiveSpeed = PASSIVE_MOVE_SPEED;
        startConstraints = rb2d.constraints;
        startAnimSpeed = animator.speed;
        startColor = spriteRenderer.color;

        // Makes the enemy big if IsBigEnemy is true
        if (IsBigEnemy)
        {
            transform.localScale *= SCALE_MULTIPLIER;
            CurrentHealth *= HEALTH_MULTIPLIER;
        }
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected virtual void Start()
    {
        // Get player object
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

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
    public virtual void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        // Apply damage type effects
        if (type == DamageType.FLAME)
        {
            // Restart timers if taking new burn damage
            if (isBurning || isSlowed)
            {
                StopCoroutine(damageCoroutine);
                if (isSlowed)
                    StartCoroutine(Unfreeze(0));
            }

            // Deal burn damage
            damageCoroutine = StartCoroutine(Burn(flamePower));
        }
        else if (type == DamageType.FROST)
        {
            // Restart timers if taking new frost damage
            if (isBurning || isSlowed)
            {
                StopCoroutine(damageCoroutine);
            }

            // Freeze movement
            damageCoroutine = StartCoroutine(Freeze(frostPower));
        }

        if (damage > 0)
        {
            // Deal damage
            CurrentHealth -= damage;

            // Makes enemies blink red
            if (!isSlowed)
                StartCoroutine(FlashRed(REDFLASH_TIME));
        }

        // Check if dead
        if (CurrentHealth <= 0)
        {
            Instantiate(ashParticles, transform.position, transform.rotation);
            AudioManager.Play(AudioClipName.UNIVERSAL_DEATH, transform.position);
            ParticleSystem ashParticlesPS = bloodSplatter.GetComponent<ParticleSystem>();
            ashParticlesPS.Play();
            Death();
        }
        else
        {
            // if the hit isn't going to kill the enemy and it's an enemy that bleeds, play the blood splatter
            if (!(this is AlgorMortis) && !(this is SkeloDevil))
            {
                Instantiate(bloodSplatter, transform.position, transform.rotation);
                ParticleSystem bloodSplatterPS = bloodSplatter.GetComponent<ParticleSystem>();
                bloodSplatterPS.Play();
            }
        }
    }

    /// <summary>
    /// Deals burn damage from flame weapons
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Burn(float flamePower)
    {
        // Play initial flame sound
        AudioManager.Play(AudioClipName.BURNING, audioSource);

        // Start burn damage
        StartCoroutine(BurnTimer());
        while (isBurning)
        {
            yield return new WaitForSeconds(((WeaponVariantValues)Constants.Values(ContainerType.WEAPON_VARIANT)).BurnDelay);

            // TODO: Play burn tick sound
            //AudioManager.Play(AudioClipName.FLAME_TICK, audioSource);

            // Damage enemy
            float flameDamage = Mathf.Clamp(flamePower - FLAME_RESISTANCE, 0, flamePower);
            TakeDamage(flameDamage, DamageType.NORMAL);
        }
    }

    /// <summary>
    /// The timer for the duration of burn damage
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator BurnTimer()
    {
        isBurning = true;

        //  Add flaming particle system
        GameObject fP = Instantiate(fireParticles);
        fP.transform.parent = transform;
        fP.transform.position = transform.position;
        ParticleSystem fireSystem = fP.GetComponent<ParticleSystem>();
        fireSystem.Play();

        yield return new WaitForSeconds(BURN_DURATION);

        isBurning = false;

        // Destroy fire particles
        fireSystem.Stop();
        Destroy(fP);
    }

    /// <summary>
    /// Freezes the enemy when hit by a frost weapon
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Freeze(int frostPower)
    {
        isSlowed = true;
        currentSlowCount += frostPower;

        // Tint sprite blue
        spriteRenderer.color = Color.cyan;

        // Freeze enemy if threshold is reached
        if (currentSlowCount >= FREEZE_THRESHOLD)
        {
            isFrozen = true;

            // Freeze rigidbody
            if (rb2d != null)
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;

            // Freeze animations
            if (animator != null)
                animator.speed = 0f;

            // Play freeze sound
            AudioManager.Play(AudioClipName.FREEZING, audioSource);
        }

        // Slow the enemy down by (count / threshold)%
        float speedMultiplier = 1f - ((float)currentSlowCount / (float)FREEZE_THRESHOLD);
        PASSIVE_MOVE_SPEED = startPassiveSpeed * speedMultiplier;
        AGGRESSIVE_MOVE_SPEED = startAggressiveSpeed * speedMultiplier;

        // Wait for freeze timer
        yield return new WaitForSeconds(FREEZE_DURATION);

        // Play unfreeze sound
        if (isFrozen)
            AudioManager.Play(AudioClipName.UNFREEZING, audioSource);

        // Unfreeze the enemy once the sound has played to the correct point
        float clipLength = AudioManager.ClipLength(AudioClipName.UNFREEZING);
        StartCoroutine(Unfreeze(clipLength / 2f));
    }

    /// <summary>
    /// Unfreezes the enemy
    /// </summary>
    /// <param name="delay">Delay to wait for unfreezing</param>
    private IEnumerator Unfreeze(float delay)
    {
        // Wait for delay
        yield return new WaitForSeconds(delay);

        // Reset to starting values
        PASSIVE_MOVE_SPEED = startPassiveSpeed;
        AGGRESSIVE_MOVE_SPEED = startAggressiveSpeed;
        rb2d.constraints = startConstraints;
        animator.speed = startAnimSpeed;
        spriteRenderer.color = startColor;
        isSlowed = false;
        isFrozen = false;
        currentSlowCount = 0;
    }

    /// <summary>
    /// Enemy death behavior
    /// </summary>
    public virtual void Death()
    {
        // Drop loot
        LootManager.DropLoot(transform.position);

        // Increases player's ferocity
        FerocityManager.Instance.IncreaseFerocity();

        // Destroy enemy
        Destroy(gameObject);
    }

    // Used to make enemies flash red for a brief second
    IEnumerator FlashRed(float timeToWait)
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, REDFLASH_OPACITY);
        yield return new WaitForSeconds(timeToWait);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

using System.Collections;
using UnityEngine;

/// <summary>
/// The Brute enemy
/// </summary>
public class Brute : Enemy
{
    // Constants
    private float WALK_TIME;                    // How long brute should walk before changing directions

    // Saved for effeciency
    private GameObject shieldPrefab;            // The shield prefab
    private GameObject myShield;                // The brute's shield
    private bool attack;                        // Checks whether the brute has attacked or not

    private WallCheck wallChecker;              // Used for changing walking direction
    private Coroutine currentWalkCoroutine;     // Used to reset WalkingTimer coroutine
    private bool hasAttacked;                   // Set by animator; resets animation to idle after attacking
    private bool hasIdlePlayed;                 // Set by animator; allows brute to walk when true
    

    // Used for tracking timer status
    private bool attackTimerRunning;            // How long the attack timer runs
    private bool walkTimerRunning;              // How long the walk timer runs
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        // Set constants
        WALK_TIME = ((BruteValues)Constants.Values(ContainerType.BRUTE)).WalkTime;

        // Spawns new shield
        shieldPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Brute/Shield");
        myShield = Instantiate<GameObject>(shieldPrefab);
        myShield.GetComponent<BruteShield>().anchorPoint = transform.GetChild(0).gameObject.transform;

        // Set up walking vars
        wallChecker = transform.GetChild(1).gameObject.GetComponent<WallCheck>();

        // Makes the brute & shield big if IsBigEnemy is true
        if (IsBigEnemy)
        {
            myShield.transform.localScale *= SCALE_MULTIPLIER;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            // If player is close & alive, chase after them
            if (!attack)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < AGGRO_RANGE)
                    Aggro();
                else
                    Idle();
            }

            //Reset animation to idle after attack
            if (hasAttacked)
                animator.SetTrigger("idle");
        }
    }

    /// <summary>
    /// Idle behaviour
    /// </summary>
    public override void Idle()
    {
        // Change walk direction when close to wall
        if (wallChecker.wallHit)
        {
            // Restart walk timer
            if (currentWalkCoroutine != null)
                StopCoroutine(currentWalkCoroutine);
            currentWalkCoroutine = StartCoroutine(WalkingTimer(WALK_TIME));
            
            // Rotate bandit to face new move direction
            transform.Rotate(0, 0, 90);

            // Disables the wall checker's circle collider so brute isn't always touching a wall
            wallChecker.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (!walkTimerRunning)
        {
            // Plays idle animation
            animator.SetTrigger("idle");

            // Start moving after idle animation plays
            if (hasIdlePlayed)
            {
                currentWalkCoroutine = StartCoroutine(WalkingTimer(WALK_TIME));
                transform.Rotate(0, 0, 90);
            }
        }
        else
        {
            // Plays walking animation
            if (!hasAttacked)
            {
                animator.SetTrigger("walk");
                hasIdlePlayed = false;
            }

            // Moves brute
            Vector3 downVector = transform.rotation * new Vector3(0f, -1f);
            rb2d.MovePosition(transform.position + (downVector * PASSIVE_MOVE_SPEED * Time.deltaTime));
        }

        // Re-enables wall checker's circle collider
        wallChecker.GetComponent<CircleCollider2D>().enabled = true;
    }

    /// <summary>
    /// Aggro behaviour
    /// </summary>
    public override void Aggro()
    {
        if (!attack)
        {
            // Move brute closer to player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, PASSIVE_MOVE_SPEED * Time.deltaTime);

            // Plays move animation
            animator.SetTrigger("walk");

            // Attack when close
            if (Vector2.Distance(transform.position, player.transform.position) < ATTACK_RANGE && !attackTimerRunning)
                Attack();

            // Rotate brute to face player
            Vector3 targ = player.transform.position - transform.position;
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }

        // Plays idle animation after attacking while still in aggro range
        if (attack && attackTimerRunning)
            animator.SetTrigger("idle");
        
    }

    /// <summary>
    /// Attack behaviour
    /// </summary>
    public override void Attack()
    {
        if (!attackTimerRunning)
        {
            // Sets attack to true
            attack = true;

            StartCoroutine(AttackTimer(ATTACK_DELAY));

            // Play attack anim
            animator.SetTrigger("attack");

            // Play attack sound
            if (!audioSource.isPlaying)
                AudioManager.Play(AudioClipName.BRUTE_ATTACK + Random.Range(0, 1), audioSource);

            if (myShield != null)
            {
                myShield.GetComponent<BoxCollider2D>().enabled = false;
                myShield.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    /// <summary>
    /// Damage behaviour
    /// </summary>
    /// <param name="damage"> amount of damage to take </param>
    /// <param name="type"> type of damage to take </param>
    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);

        if (!audioSource.isPlaying)
            AudioManager.Play(AudioClipName.BRUTE_HURT + Random.Range(0, 2), transform.position);
    }

    /// <summary>
    /// Death behaviour
    /// </summary>
    public override void Death()
    {
        base.Death();

        // Destroys shield if not already destroyed
        if (myShield != null)
            Destroy(myShield);
    }

    /// <summary>
    /// A timer used for attacking
    /// </summary>
    /// <param name="timeToWait">How long the timer runs</param>
    /// <returns></returns>
    IEnumerator AttackTimer(float timeToWait)
    {
        attackTimerRunning = true;
        yield return new WaitForSeconds(timeToWait);
        attackTimerRunning = false;
        attack = false;
        hasAttacked = false;
        if (myShield != null)
        {
            myShield.GetComponent<BoxCollider2D>().enabled = true;
            myShield.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    /// <summary>
    /// A timer used for changing walking direction
    /// </summary>
    /// <param name="timeToWait"> How long to wait</param>
    /// <returns></returns>
    IEnumerator WalkingTimer(float timeToWait)
    {
        walkTimerRunning = true;
        yield return new WaitForSeconds(timeToWait);
        walkTimerRunning = false;
    }
}

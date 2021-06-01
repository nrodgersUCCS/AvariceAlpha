using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An algormortis enemy
/// </summary>
public class AlgorMortis : Enemy
{
    // Constants
    private float PASSIVE_RANGE;                                                                            // When the player's distance to the algormortis is greater than or equal to this value, it becomes passive
    private float MOVE_WAIT;                                                                                // The time in seconds that the algormortis waits between moving when passive
    private float MOVE_TIME;                                                                                // The time in seconds that the algormortis spends moving when passive
    private float RAM_DELAY;                                                                                // The time in seconds that the algormortis waits before charging at the player
    private float ATTACK_TIME;                                                                              // The time in seconds that the algormortis spends charging toward the player during each of its attacks

    // Variables
    private bool aggressive = false;                                                                        // Whether the algormortis is aggressive toward the player
    private bool canMove = true;                                                                            // Whether the algormortis is allowed to move
    private bool canAttack = true;                                                                          // Whether the algormortis is allowed to attack
    private bool isAttacking = true;                                                                        // Whether the algormortis is attacking
    private Vector2 movementDirection;                                                                      // Direction to move
    private GameObject explosionPrefab;                                                                     // The ice explosion

    // Animation and sound variables                                        
    private AudioSource[] algorAudioSources = null;                                                         // Algormortis's audio sources
    private List<AudioClipName> idleWindClips = new List<AudioClipName>                                     // List of passive wind audio clips to randomly choose from
    {
        AudioClipName.ALGOR_MORTIS_PASSIVE, AudioClipName.ALGOR_MORTIS_PASSIVEWIND_OLD,
        AudioClipName.ALGOR_MORTIS_PASSIVEWIND2_OLD, AudioClipName.ALGOR_MORTIS_PASSIVEWIND3_OLD
    };
    private List<AudioClipName> aggroWindClips = new List<AudioClipName>                                    // List of aggressive wind audio clips to randomly choose from
    {
        AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND, AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND2,
        AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND3, AudioClipName.ALGOR_MORTIS_AGGRESSIVEWIND4
    };
    private List<AudioClipName> idleGrowlClips = new List<AudioClipName>                                    // List of passive growl audio clips to randomly choose from
    {
        AudioClipName.ALGOR_MORTIS_PASSIVEGROWL_OLD, AudioClipName.ALGOR_MORTIS_PASSIVEGROWL2_OLD,
        AudioClipName.ALGOR_MORTIS_PASSIVEGROWL3_OLD
    };
    private List<AudioClipName> aggroGrowlClips = new List<AudioClipName>                                   // List of aggressive growl audio clips to randomly choose from
    {
        AudioClipName.ALGOR_MORTIS_AGGRESSIVEGROWL, AudioClipName.ALGOR_MORTIS_AGGRESSIVEGROWL2,
        AudioClipName.ALGOR_MORTIS_AGGRESSIVEGROWL3
    };

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Set constants
        AlgorMortisValues container = (AlgorMortisValues)Constants.Values(ContainerType.ALGOR_MORTIS);
        PASSIVE_RANGE = container.PassiveRange;
        MOVE_WAIT = container.MoveWait;
        MOVE_TIME = container.MoveTime;
        RAM_DELAY = container.RamDelay;
        ATTACK_TIME = container.AttackTime;

        // Get the Rigidbody2D from the algormortis
        rb2d = GetComponent<Rigidbody2D>();

        // Get animator and audio source
        algorAudioSources = GetComponents<AudioSource>();

        // Set up prefabs and other game objects
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Enemies/AlgorMortis/IceExplosion");
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        if (!isFrozen)
        {
            if (aggressive)
            {
                // If player gets too far, set aggressive to false
                if (Vector2.Distance(player.transform.position, transform.position) >= PASSIVE_RANGE)
                {
                    aggressive = false;
                    canAttack = false;
                    animator.SetBool("Passive", true);
                }

                Aggro();
            }
            else
            {
                Idle();
            }
        }
    }

    /// <summary>
    /// Algormortis idle behavior
    /// </summary>
    public override void Idle()
    {
        // If the algormortis is allowed to move
        if (canMove)
        {
            StartCoroutine(Move());
            canMove = false;

            // Get random idle clips and play them
            int rand;
            if (!algorAudioSources[0].isPlaying)
            {
                rand = Random.Range(0, idleWindClips.Count);
                AudioManager.Play(idleWindClips[rand], algorAudioSources[0]);
            }
            if (!algorAudioSources[1].isPlaying)
            {
                rand = Random.Range(0, idleGrowlClips.Count);
                AudioManager.Play(idleGrowlClips[rand], algorAudioSources[1]);
            }
        }
    }

    /// <summary>
    /// Algormortis aggro behavior
    /// </summary>
    public override void Aggro()
    {
        // Make the algormortis start an attack if it can
        if (canAttack)
        {
            Attack();
            canAttack = false;
            isAttacking = true;

            // Get random aggro clips and play them
            int rand;
            if (!algorAudioSources[0].isPlaying)
            {
                rand = Random.Range(0, aggroWindClips.Count);
                AudioManager.Play(aggroWindClips[rand], algorAudioSources[0]);
            }
            if (!algorAudioSources[1].isPlaying)
            {
                rand = Random.Range(0, aggroGrowlClips.Count);
                AudioManager.Play(aggroGrowlClips[rand], algorAudioSources[1]);
            }
        }
    }

    /// <summary>
    /// Algormortis attack behavior
    /// </summary>
    public override void Attack()
    {
        StartCoroutine(Ram());
    }

    /// <summary>
    /// Makes the algormortis attempt to ram the player by having it run toward the player in a straight line
    /// </summary>
    IEnumerator Ram()
    {
        // Calculate the direction to the player
        movementDirection = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

        // Move toward movementDirection after ATTACK_DELAY seconds
        yield return new WaitForSeconds(RAM_DELAY);
        Move(AGGRESSIVE_MOVE_SPEED, movementDirection);
        animator.SetBool("Attacking", true);

        // Stop moving after ATTACK_TIME seconds
        yield return new WaitForSeconds(ATTACK_TIME);
        movementDirection = Vector2.zero;
        Move(0, movementDirection);
        isAttacking = false;
        animator.SetBool("Attacking", false);

        // Wait ATTACK_WAIT seconds before allowing the algormortis to make another attack
        yield return new WaitForSeconds(ATTACK_DELAY);
        canAttack = true;
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Upon collision with the player while attacking
        if (collision.gameObject.tag == "Player" && isAttacking)
        {
            Death();
        }

        // Stop the algormortis on collision with a wall
        if (collision.gameObject.layer == 15)
        {
            movementDirection = Vector2.zero;
        }
        
    }

    /// <summary>
    /// Makes the algormortis only move for a certain amount of time before it stops when passive
    /// </summary>
    IEnumerator Move()
    {
        // Calculate a random direction to travel
        movementDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        Move(PASSIVE_MOVE_SPEED, movementDirection);

        // The algormortis stops moving after MOVE_TIME seconds
        yield return new WaitForSeconds(MOVE_TIME);
        movementDirection = Vector2.zero;
        Move(0, movementDirection);

        // The algormortis can move again after MOVE_WAIT seconds
        yield return new WaitForSeconds(MOVE_WAIT);
        canMove = true;
    }

    /// <summary>
    /// Makes the algormortis move
    /// </summary>
    /// <param name="speed">The speed to move at</param>
    /// /// <param name="direction">The direction to move in</param>
    private void Move(float speed, Vector2 direction)
    {
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Deal damage to the enemy
    /// </summary>
    /// <param name="damage">Amount of damage to do</param>
    /// /// <param name="type">Type of damage to take</param>
    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);

        // Set aggressive to true if not already aggressive
        if (!aggressive)
        {
            aggressive = true;

            // Play shriek sounds to indicate the change to aggressiveness
            algorAudioSources[1].Stop();
            AudioManager.Play(AudioClipName.ALGOR_MORTIS_SHRIEK2, algorAudioSources[1]);
            algorAudioSources[0].Stop();
            AudioManager.Play(AudioClipName.ALGOR_MORTIS_SHRIEK4, algorAudioSources[0]);
        }

        // Switch algormortis to its aggressive animation
        animator.SetBool("Passive", false);
    }

    /// <summary>
    /// Freeze effect does nothing on the Algor Mortis
    /// </summary>
    protected override IEnumerator Freeze(int frostPower)
    {
        yield return null;
    }

    /// <summary>
    /// Algormortis death behavior
    /// </summary>
    public override void Death()
    {
        // Create an ice explosion and then destroy itself
        GameObject myExplosion = Instantiate<GameObject>(explosionPrefab, (Vector2)transform.position, Quaternion.identity);

        // If big enemy, scales explosion by same amount as enemy
        if (IsBigEnemy)
            myExplosion.transform.localScale *= SCALE_MULTIPLIER;

        base.Death();
    }
}

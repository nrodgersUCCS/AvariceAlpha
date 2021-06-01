using System.Collections;
using UnityEngine;

/// <summary>
/// The Corpse eater enemy
/// </summary>
public class CorpseEater : Enemy
{
    // Constants
    private float CHASE_RANGE;
    private float RISE_TIME;

    private bool aggro;
    private bool attack;
    private bool riseSoundPlayed;

    // Used for tracking timer status
    private bool attackTimerRunning;
    private bool riseTimerRunning;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Set constants
        CorpseEaterValues container = (CorpseEaterValues)Constants.Values(ContainerType.CORPSE_EATER);
        CHASE_RANGE = container.ChaseRange;
        RISE_TIME = container.RiseTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            // Stops sliding
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            // If player is close, chase after them
            if (!attack)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < AGGRO_RANGE || aggro)
                    Aggro();

                if (Vector2.Distance(transform.position, player.transform.position) > CHASE_RANGE && aggro)
                    Idle();
            }
            else
                Attack();
        }
    }

    /// <summary>
    /// Idle behaviour
    /// </summary>
    public override void Idle()
    {
        // Stops object from sliding around
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

        // Allows new rise sound to play
        riseSoundPlayed = false;

        aggro = false;

        animator.SetTrigger("PlayDead");
    }

    /// <summary>
    /// Aggro behaviour
    /// </summary>
    public override void Aggro()
    {
        aggro = true;

        // Plays random rise sound
        if (!audioSource.isPlaying && !riseSoundPlayed)
        {
            AudioManager.Play(AudioClipName.CORPSE_EATER_RISE + Random.Range(0, 5), audioSource);
            riseSoundPlayed = true;
            StartCoroutine(RiseTimer(RISE_TIME));
            animator.SetTrigger("Idle");
        }

        if (!riseTimerRunning)
        {
            // Move corpse eater towards player
            if (Vector2.Distance(transform.position, player.transform.position) > ATTACK_RANGE && !attack)
            {
                animator.SetTrigger("Charging");
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, AGGRESSIVE_MOVE_SPEED * Time.deltaTime);
            }
            else
            {
                Attack();
            }
        }

        // Rotate corpse eater to face player
        Vector3 targ = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    /// <summary>
    /// Attack behaviour
    /// </summary>
    public override void Attack()
    {
        animator.SetTrigger("Attack");

        if (!attackTimerRunning)
        {
            // Sets attack to true
            attack = true;

            StartCoroutine(AttackTimer(ATTACK_DELAY));

            // Stops corpse eater from sliding
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            // Play attack sound
            if (!audioSource.isPlaying)
                AudioManager.Play(AudioClipName.CORPSE_EATER_ATTACK2 + Random.Range(0, 4), audioSource);
        }
    }

    /// <summary>
    /// Deal damage to the corpse eater
    /// </summary>
    /// <param name="damage">Amount of damage to do</param>
    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);

        // Activate aggro behavior
        aggro = true;

        // play hurt sound
        if (!audioSource.isPlaying)
            AudioManager.Play(AudioClipName.CORPSE_EATER_HURT, audioSource);
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
    }

    // A timer used to delay chasing after the corpse eater rises
    IEnumerator RiseTimer(float timeToWait)
    {
        riseTimerRunning = true;
        animator.speed = 0;
        yield return new WaitForSeconds(timeToWait);
        riseTimerRunning = false;
        animator.speed = 1;
    }
}

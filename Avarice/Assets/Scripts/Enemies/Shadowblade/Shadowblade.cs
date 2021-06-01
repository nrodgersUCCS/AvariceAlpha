using System.Collections;
using UnityEngine;

/// <summary>
/// The Shadowblade enemy
/// </summary>
public class Shadowblade : Enemy
{
    // Set by animator; resets animation to idle after attacking
    private bool hasAttacked;

    private bool attack;

    // Used for tracking timer status
    private bool attackTimerRunning;

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
        // Plays idle animation
        animator.SetTrigger("idle");
    }

    /// <summary>
    /// Aggro behaviour
    /// </summary>
    public override void Aggro()
    {
        // Move shadowblade closer to player
        if (!attack)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, AGGRESSIVE_MOVE_SPEED * Time.deltaTime);

            // Plays move animation
            animator.SetTrigger("move");
        }

        // Attack when close
        if (Vector2.Distance(transform.position, player.transform.position) < ATTACK_RANGE && !attackTimerRunning)
            Attack();

        // Rotate shadowblade to face player
        Vector3 targ = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
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
                AudioManager.Play(AudioClipName.SHADOWBLADE_ATTACK + Random.Range(0, 3), audioSource);
        }
    }

    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);

        if (!audioSource.isPlaying)
            AudioManager.Play(AudioClipName.SHADOWBLADE_HURT + Random.Range(0, 3), audioSource);
    }

    /// <summary>
    /// Death behaviour
    /// </summary>
    public override void Death()
    {
        base.Death();

        // Plays death sound
        AudioManager.Play(AudioClipName.SHADOWBLADE_DEATH + Random.Range(0, 4), transform.position);
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
    }
}

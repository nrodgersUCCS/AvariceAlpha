using System.Collections;
using UnityEngine;

/// <summary>
/// A class to handle to handle the Fallen Cherub
/// </summary>
public class Cherub : Enemy
{
    // Constants
    private float PASSIVE_RANGE;                            // When the player's distance to the cherub is greater than or equal to this value, it becomes passive
    private float ATTACK_CHARGE1;                           // The time in seconds that the cherub spends charging the first part of an attack
    private float ATTACK_CHARGE2;                           // The time in seconds that the cherub spends charging the second part of an attack
    private float ATTACK_CHARGE3;                           // The time in seconds that the cherub spends charging the final part of an attack
    private float MOVE_WAIT;                                // The time in seconds that the cherub waits between moving when passive
    private float MOVE_TIME;                                // The time in seconds that the cherub spends moving when passive

    // Variables
    private bool aggressive = false;                        // Whether the cherub is aggressive toward the player
    private bool canMove = true;                            // Whether the cherub is allowed to move
    private bool canAttack = true;                          // Whether the cherub is allowed to attack
    private Vector2 movementDirection;                      // Direction to move
    private Vector2 destination;                            // Position the cherub is trying go get to
    private GameObject fireballPrefab;                      // The fireball
    private GameObject cherubManager;                       // The cherub manager

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Set constants
        CherubValues container = (CherubValues)Constants.Values(ContainerType.CHERUB);
        PASSIVE_RANGE = container.PassiveRange;
        ATTACK_CHARGE1 = container.Charge1Time;
        ATTACK_CHARGE2 = container.Charge2Time;
        ATTACK_CHARGE3 = container.Charge3Time;
        MOVE_WAIT = container.MoveDelay;
        MOVE_TIME = container.MoveTime;

        // Set up prefabs and other game objects
        fireballPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Cherub/Fireball");
        cherubManager = CherubManager.Instance;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        if (!isFrozen)
        {
            // Cherub moves differently depending on whether it has detected the player
            // While the cherub is aggressive it rely on the cherub manager to dictate its movement while it occasionally stops to shoot fireballs
            // While the cherub is passive, it will move a short distance in a random direction, stop for a short time, and then move again
            if (aggressive)
            {
                // If player gets too far, set aggressive to false
                if (Vector2.Distance(player.transform.position, transform.position) >= PASSIVE_RANGE)
                {
                    aggressive = false;
                    canAttack = false;

                    // Remove self from the list of cherubs
                    cherubManager.GetComponent<CherubManager>().RemoveCherub(gameObject);
                }
                else
                {
                    Aggro();
                }
            }
            else
            {
                // If player gets too close, set aggressive to true and begin attacking
                if (Vector2.Distance(player.transform.position, transform.position) <= AGGRO_RANGE)
                {
                    aggressive = true;
                    canAttack = true;

                    // Add self to the list of cherubs
                    cherubManager.GetComponent<CherubManager>().AddCherub(gameObject);
                }
                else
                {
                    Idle();
                }
            }
        }
    }

    /// <summary>
    /// Cherub aggro behavior
    /// </summary>
    public override void Aggro()
    {
        // If the cherub is allowed to move
        if (canMove)
        {
            // Calculate the direction to the current destination
            movementDirection = (destination - (Vector2)transform.position).normalized;
            Move(AGGRESSIVE_MOVE_SPEED, movementDirection);
        }
        else
        {
            // Stop any movement already in progress
            movementDirection = Vector2.zero;
            Move(0, movementDirection);
        }

        // Make the cherub start an attack if it can
        if (canAttack)
        {
            Attack();
            canAttack = false;
        }
    }

    /// <summary>
    /// Cherub idle behavior
    /// </summary>
    public override void Idle()
    {
        // If the cherub is allowed to move
        if (canMove)
        {
            StartCoroutine(Move());
            canMove = false;  
        }
        transform.up = Vector2.up;
    }

    /// <summary>
    /// Cherub attack behavior
    /// </summary>
    public override void Attack()
    {
        StartCoroutine(ShootFireball());
    }

    /// <summary>
    /// Makes the cherub pause and shoot a fireball at the player
    /// </summary>
    IEnumerator ShootFireball()
    {
        // Cherub will stand in place while charging up its attack
        // While this is happening it will play its attack sounds sequentially while its attack animation is played
        canMove = false;
        animator.SetBool("Attacking", true);
        AudioManager.Play(AudioClipName.CHERUB_ATTACK, audioSource);
        yield return new WaitForSeconds(ATTACK_CHARGE1);
        AudioManager.Play(AudioClipName.CHERUB_ATTACK2, audioSource);
        yield return new WaitForSeconds(ATTACK_CHARGE2);
        AudioManager.Play(AudioClipName.CHERUB_ATTACK3, audioSource);

        // Shoot a fireball
        GameObject myFireball = Instantiate<GameObject>(fireballPrefab, (Vector2)transform.position, Quaternion.identity);

        // If big enemy, scales fireball by same amount as enemy
        if (IsBigEnemy)
            myFireball.transform.localScale *= SCALE_MULTIPLIER;
        myFireball.GetComponent<Fireball>().Shoot();
        yield return new WaitForSeconds(ATTACK_CHARGE3);

        // Start the attack cooldown and return to flying animation
        animator.SetBool("Attacking", false);
        canMove = true;
        StartCoroutine(AttackCooldown());
    }

    /// <summary>
    /// Makes the cherub wait certain amount of time before it can attack again
    /// </summary>
    IEnumerator AttackCooldown()
    {
        // The cherub can attack again after ATTACK_WAIT seconds
        yield return new WaitForSeconds(ATTACK_DELAY);
        if (aggressive)
        {
            canAttack = true;
        }
    }

    /// <summary>
    /// Makes the cherub only move for a certain amount of time before it stops when passive
    /// </summary>
    IEnumerator Move()
    {
        // Calculate a random direction to travel
        movementDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
        Move(PASSIVE_MOVE_SPEED, movementDirection);

        // The cherub stops moving after MOVE_TIME seconds
        yield return new WaitForSeconds(MOVE_TIME);
        movementDirection = Vector2.zero;
        Move(0, movementDirection);

        // The cherub can move again after MOVE_WAIT seconds
        yield return new WaitForSeconds(MOVE_WAIT);
        canMove = true;
    }

    /// <summary>
    /// Makes the cherub move
    /// </summary>
    /// <param name="speed">The speed to move at</param>
    /// /// <param name="direction">The direction to move in</param>
    private void Move(float speed, Vector2 direction)
    {
        // Rotate the cherub to face the player if it is aggressive, or rotate it upright if it is passive
        if (aggressive)
        {
            transform.up = -1 * (player.transform.position - gameObject.transform.position).normalized;
        }
        else
        {
            transform.up = Vector2.up;
        }

        // Move the cherub
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Sets the destination
    /// </summary>
    /// /// <param name="position">Position for the cherub to go to</param>
    public void SetDestination(Vector2 position)
    {
        destination = position;
    }

    /// <summary>
    /// Cherub death behavior
    /// </summary>
    public override void Death()
    {
        // Remove the cherub from the list of cherubs and then destroy it
        if (aggressive)
        {
            cherubManager.GetComponent<CherubManager>().RemoveCherub(gameObject);
        }

        base.Death();
    }
}
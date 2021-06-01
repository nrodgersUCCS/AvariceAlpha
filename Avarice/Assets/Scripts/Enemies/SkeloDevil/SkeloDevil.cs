using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SkeloDevil Enemy
/// </summary>
public class SkeloDevil : Enemy
{
    // Constants
    private float FLEE_SPEED;
    private float CHASE_RANGE;
    private float CHASE_SPEED;
    private float IMMUNE_DELAY;

    //Sound Varibles
    private List<AudioClipName> SkelofootstepClips = new List<AudioClipName>
    {
        AudioClipName.SKELODEVIL_WALK_1,AudioClipName.SKELODEVIL_WALK_2,AudioClipName.SKELODEVIL_WALK_3
    };


    // Set by animator
    [SerializeField]
    private bool animPaused;

    // Stops Death method from always running 
    private bool deathSet;

    private BoxCollider2D boxCollider;

    // Used for attacking
    private bool attack;
    private int attackingPart;
    private bool attackTimerRunning = false;
    private bool assembling = false;

    //Used for walking
    private bool walkTimerRunning = false;

    //Body Part GameObjects
    List<GameObject> parts;
    GameObject head;
    GameObject torso;
    GameObject left_Arm;
    GameObject right_Arm;
    GameObject left_leg;
    GameObject right_leg;

    //Targeting vars
    private Vector3 AimLocation;
    private float ROTATION_SPEED = 80.0f;
    private int path;
    private bool immuneTimerRunning = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Set constants
        SkelodevilValues container = (SkelodevilValues)Constants.Values(ContainerType.SKELODEVIL);
        FLEE_SPEED = container.FleeSpeed;
        CHASE_RANGE = container.ChaseRange;
        CHASE_SPEED = container.ChaseSpeed;
        IMMUNE_DELAY = container.ImmuneDelay;

        // The current part being thrown
        attackingPart = 7;

        // The list of current parts
        parts = new List<GameObject>();
        parts.Capacity = 6;

        //sets player to target
        player = FindObjectOfType<PlayerMovement>().gameObject;

        //Find Children
        head = transform.GetChild(0).gameObject;
        torso = transform.GetChild(3).gameObject;
        left_Arm = transform.GetChild(1).gameObject;
        right_Arm = transform.GetChild(2).gameObject;
        left_leg = transform.GetChild(4).gameObject;
        right_leg = transform.GetChild(5).gameObject;

        //Add parts to the parts list
        parts.Add(head);
        parts.Add(torso);
        parts.Add(left_Arm);
        parts.Add(right_Arm);
        parts.Add(left_leg);
        parts.Add(right_leg);

        boxCollider = (BoxCollider2D)collider;
        HideParts();
    }

    // Update is called once per frame
    private void Update()
    {
        rb2d.velocity = Vector2.zero;

        if (!isFrozen)
        {
            // Checks if skelodevil is not assembling, immune, and dying
            if (!assembling && !immuneTimerRunning && !deathSet)
            {
                // Checks if player exists & if skelodevil is not attacking 
                if (player != null && !attack)
                {
                    // Enter aggro if player gets too close
                    if (Vector2.Distance(transform.position, player.transform.position) <= AGGRO_RANGE)
                        Aggro();

                    // Enter idle if player is not too close
                    else
                    {
                        // Restarts walking timer & changes walking direction
                        if (!walkTimerRunning)
                        {
                            StartCoroutine(WalkTimer(4f));
                            path = Random.Range(0, 9);
                        }

                        //Idle while walking timer is running
                        else
                            Idle();
                    }
                }

                // Is skelodevil was aggro'd long enough, begin attacking
                else if (player != null && attack)
                {
                    if (!attackTimerRunning)
                        Attack();
                }
            }

            // Pauses the animator
            if (animPaused)
                animator.speed = 0;
        }

        // Destroy skelodevil after death sound has played
        if (deathSet && !audioSource.isPlaying)
        {
            FerocityManager.Instance.IncreaseFerocity();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Enemy idle behavior
    /// </summary>
    public override void Idle()
    {
        // Used for moving
        Vector3 movePos = new Vector3(0, 0);
        float rotation = 0f;
        float moveSpeed = 2 * Time.deltaTime;

        //Moves in one of eight directions based on RandomRange
        switch (path)
        {
            case 1:
                //moves enemies to the right
                movePos = new Vector3(5, 0);
                rotation = 90f;
                break;

            case 2:
                //moves enemies to the left
                movePos = new Vector3(-5, 0);
                rotation = 270;

                break;

            case 3:
                //moves enemies to the up
                movePos = new Vector3(0, 5);
                rotation = 180;
                break;

            case 4:
                //moves enemies to the down
                movePos = new Vector3(0, -5);
                rotation = 0f;
                break;

            case 5:
                //moves enemies to up right
                movePos = new Vector3(3, 2);
                rotation = 135f;
                break;

            case 6:
                //moves enemies to down right
                movePos = new Vector3(3, -2);
                rotation = 45f;
                break;

            case 7:
                //moves enemies to the up left
                movePos = new Vector3(-2, 3);
                rotation = 225f;
                break;

            case 8:
                //moves enemies to the down left
                movePos = new Vector3(-2, -3);
                rotation = 315f;
                break;
        }

        // Moves skelodevil
        transform.position = Vector2.MoveTowards(transform.position, transform.position + movePos, moveSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));

        // Plays walking sound
        if (!audioSource.isPlaying)
            PlaySkelodevilFootstep();
    }

    /// <summary>
    /// Enemy aggro behavior
    /// </summary>
    public override void Aggro()
    {
        // Moves while walking timer is still running
        if (walkTimerRunning)
        {
            //if target is too close, skelodevil backs away to avoid being hit
            if (Vector2.Distance(gameObject.transform.position, player.transform.position) < FLEE_RANGE)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position,
                    -FLEE_SPEED * Time.deltaTime);
            }

            //Circles the player
            else
            {
                Vector3 targ = player.transform.position;
                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;
                transform.RotateAround(player.transform.position, new Vector3(0, 0, 1), ROTATION_SPEED * Time.deltaTime);
                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

                // Plays walking sound
                if (!audioSource.isPlaying)
                    PlaySkelodevilFootstep();
            }

            //if target is too far away moves closer
            if (Vector2.Distance(gameObject.transform.position, player.transform.position) > CHASE_RANGE && !immuneTimerRunning)
            {
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position,
                    CHASE_SPEED * Time.deltaTime);
            }
        }

        // Start attacking once walking timer stops running
        else
        {
            AimLocation = player.transform.position;
            attack = true;
        }
    }

    /// <summary>
    /// Enemy attack behavior
    /// </summary>
    public override void Attack()
    {
        // Shows the throwable parts & hides main sprite
        ShowParts();

        // Changes which part gets thrown next
        attackingPart--;

        // Starts the attack timer
        StartCoroutine(AttackTimer(ATTACK_DELAY));

        // Plays attack sound
        AudioManager.Play(AudioClipName.SKELODEVIL_ATTACK, audioSource);

        //Makes all remaining parts attack with a slight delay
        if (attackingPart == 6 && right_leg != null)
        {
            right_leg.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
        }
        else if (attackingPart == 5 && left_leg != null)
        {
            left_leg.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
        }
        else if (attackingPart == 4 && right_Arm != null)
        {
            right_Arm.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
        }
        else if (attackingPart == 3 && left_Arm != null)
        {
            left_Arm.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
        }
        else if (attackingPart == 2 && torso != null)
        {
            torso.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
        }
        else if (attackingPart == 1 && head != null)
        {
            head.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
        }
        else if (attackingPart == 0)
        {
            audioSource.Stop();
        }
        else if (attackingPart < 0)
        {
            attackingPart = 7;
            AssembleParts();
            audioSource.Stop();
        }
    }

    /// <summary>
    /// Damage the enemy
    /// </summary>
    /// <param name="damage"> Amount of damage to take</param>
    /// <param name="type">Type of damage to take</param>
    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        if(immuneTimerRunning)
            base.TakeDamage(damage, type, flamePower, frostPower);
        else
            base.TakeDamage(damage / 2, type, flamePower, frostPower);
    }
    ///<summary>
    ///plays skelodevil footstep sound
    ///to be used in animation events
    ///</summary>
    public void PlaySkelodevilFootstep()
    {
        int rand = UnityEngine.Random.Range(0, 3);
        AudioManager.Play(SkelofootstepClips[rand], audioSource);
    }

    /// <summary>
    /// Plays death sound & animation
    /// </summary>
    public override void Death()
    {
        // Marks the skelodevil for death
        deathSet = true;

        // Allows death animation to play
        animPaused = false;
        attack = false;
        animator.speed = 1;

        // Plays death animation
        animator.SetBool("vulnerable", false);
        animator.SetBool("dead", true);

        // Drop loot
        LootManager.DropLoot(transform.position);

        // Plays death sound
        AudioManager.Play(AudioClipName.SKELODEVIL_DIE, audioSource);

        //Removes the Box Collider when killed to prevent the enemy from dropping items if hit again.
        boxCollider.enabled = false;
    }

    /// <summary>
    /// Re-assembles the skelodevil
    /// </summary>
    public void AssembleParts()
    {
        // Prevents skelodevil attacking/ moving while being assembled
        assembling = true;

        // If skelodevil wasn't stunned, move main transform to the thrown head
        if (animator.GetBool("vulnerable") == false)
            transform.position = head.gameObject.transform.position;

        // Re-assembles skelodevil
        if (head != null) head.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (torso != null) torso.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (left_Arm != null) left_Arm.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (right_Arm != null) right_Arm.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (left_leg != null) left_leg.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (right_leg != null) right_leg.GetComponent<SkeloDevilBodyPart>().Assemble();
        HideParts();

        // Makes skelodevil move again
        attack = false;
        StartCoroutine(WalkTimer(4f));

        // Re-enables walking animation
        animator.speed = 1;
        animator.SetBool("vulnerable", false);
    }

    /// <summary>
    /// Freezes skelodevil when a part is hit
    /// </summary>
    /// <param name="part">Moves SkeloDevil to the hit part</param>
    public void Stun(GameObject part)
    {
        StartCoroutine(ImmuneTimer(IMMUNE_DELAY));
        HideParts();
        transform.position = part.transform.position;
        attackingPart = 7;
        animator.SetBool("vulnerable", true);
    }

    /// <summary>
    /// Disables sprite renderer & box collider for parts
    /// </summary>
    private void HideParts()
    {
        // Re-enables skelodevils box collider
        boxCollider.enabled = true;

        // Shows the main skelodevil
        spriteRenderer.enabled = true;
        animator.speed = 1;

        // Disables thrown parts
        head.GetComponent<SpriteRenderer>().enabled = false;
        head.GetComponent<BoxCollider2D>().enabled = false;
        torso.GetComponent<SpriteRenderer>().enabled = false;
        torso.GetComponent<BoxCollider2D>().enabled = false;
        left_Arm.GetComponent<SpriteRenderer>().enabled = false;
        left_Arm.GetComponent<BoxCollider2D>().enabled = false;
        right_Arm.GetComponent<SpriteRenderer>().enabled = false;
        right_Arm.GetComponent<BoxCollider2D>().enabled = false;
        left_leg.GetComponent<SpriteRenderer>().enabled = false;
        left_leg.GetComponent<BoxCollider2D>().enabled = false;
        right_leg.GetComponent<SpriteRenderer>().enabled = false;
        right_leg.GetComponent<BoxCollider2D>().enabled = false;
    }

    /// <summary>
    /// Enables sprite renderer & box collider for parts
    /// </summary>
    private void ShowParts()
    {
        // Disables skelodevil box collider
        boxCollider.enabled = false;

        // Hides the main skelodevil 
        animator.speed = 0;
        spriteRenderer.enabled = false;

        // Enables thrown parts
        head.GetComponent<SpriteRenderer>().enabled = true;
        head.GetComponent<BoxCollider2D>().enabled = true;
        torso.GetComponent<SpriteRenderer>().enabled = true;
        torso.GetComponent<BoxCollider2D>().enabled = true;
        left_Arm.GetComponent<SpriteRenderer>().enabled = true;
        left_Arm.GetComponent<BoxCollider2D>().enabled = true;
        right_Arm.GetComponent<SpriteRenderer>().enabled = true;
        right_Arm.GetComponent<BoxCollider2D>().enabled = true;
        left_leg.GetComponent<SpriteRenderer>().enabled = true;
        left_leg.GetComponent<BoxCollider2D>().enabled = true;
        right_leg.GetComponent<SpriteRenderer>().enabled = true;
        right_leg.GetComponent<BoxCollider2D>().enabled = true;
    }

    /// <summary>
    /// A timer used for attacking
    /// </summary>
    /// <param name="timeToWait">How long the timer runs</param>
    /// <returns></returns>
    IEnumerator AttackTimer(float timeToWait)
    {
        if (!attackTimerRunning)
        {
            attackTimerRunning = true;
            yield return new WaitForSeconds(timeToWait);
            attackTimerRunning = false;
        }
    }

    /// <summary>
    /// A timer used for walking
    /// </summary>
    /// <param name="timeToWait">How long the timer runs</param>
    /// <returns></returns>
    IEnumerator WalkTimer(float timeToWait)
    {
        if (!walkTimerRunning)
        {
            assembling = false;
            walkTimerRunning = true;
            yield return new WaitForSeconds(timeToWait);
            walkTimerRunning = false;
        }
    }

    /// <summary>
    /// A timer used for immunity
    /// </summary>
    /// <param name="timeToWait">How long the timer runs</param>
    /// <returns></returns>
    IEnumerator ImmuneTimer(float timeToWait)
    {
        if (!immuneTimerRunning)
        {
            gameObject.tag = "Untagged";
            immuneTimerRunning = true;
            yield return new WaitForSeconds(timeToWait);
            immuneTimerRunning = false;
            animPaused = false;
            gameObject.tag = "Enemy";
            AssembleParts();
        }
    }
}
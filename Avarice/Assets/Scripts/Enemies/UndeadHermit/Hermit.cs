using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to handle the Undead Hermit
/// </summary>
public class Hermit : Enemy
{
    // Constants
    private float CRAWL_WAIT;                                               // The time in seconds that the hermit waits between crawling
    private float CRAWL_TIME;                                               // The time in seconds that the hermit spends crawling
    private float CRAWL_AUDIO_COOLDOWN;                                     // The time in seconds that the hermit waits before playing crawl sounds
    private float IDLE_AUDIO_COOLDOWN;                                      // The time in seconds that the hermit waits before playing idle sounds

    // Variables
    private bool canCrawl = true;                                           // Whether the hermit is allowed to jump, also used to determine if it is attacking
    private bool isCrawling = false;                                        // Whether the hermit is currently jumping
    private Vector2 movementDirection;                                      // Direction to move
    private bool canMakeSound = true;                                       // Whether the hermit is allowed to generate repeating audio

    // Animation and sound variables                                        
    private List<AudioClipName> idleClips = new List<AudioClipName>         // List of idle audio clips to randomly choose from, I have no idea why there isn't an UNDEAD_HERMIT_IDLE2 and UNDEAD_HERMIT_IDLE4
    {
        AudioClipName.UNDEAD_HERMIT_IDLE, AudioClipName.UNDEAD_HERMIT_IDLE3, AudioClipName.UNDEAD_HERMIT_IDLE5
    };
    private List<AudioClipName> gruntClips = new List<AudioClipName>          // List of grunt audio clips to randomly choose from
    {
        AudioClipName.UNDEAD_HERMIT_GRUNT, AudioClipName.UNDEAD_HERMIT_GRUNT2, AudioClipName.UNDEAD_HERMIT_GRUNT3, AudioClipName.UNDEAD_HERMIT_GRUNT4, AudioClipName.UNDEAD_HERMIT_GRUNT5
    };
    private List<AudioClipName> slideClips = new List<AudioClipName>          // List of slide audio clips to randomly choose from
    {
        AudioClipName.UNDEAD_HERMIT_SLIDE, AudioClipName.UNDEAD_HERMIT_SLIDE2
    };
    private List<AudioClipName> hurtClips = new List<AudioClipName>           // List of hurt audio clips to randomly choose from
    {
        AudioClipName.UNDEAD_HERMIT_HURT
    };
    // Used to stop sliding
    private bool hasBeenHit;

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Set constants
        HermitValues container = (HermitValues)Constants.Values(ContainerType.HERMIT);
        CRAWL_WAIT = container.CrawlDelay;
        CRAWL_TIME = container.CrawlTime;
        CRAWL_AUDIO_COOLDOWN = container.CrawlAudioDelay;
        IDLE_AUDIO_COOLDOWN = container.IdleAudioDelay;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        if (!isFrozen)
        {
            Idle();

            // Stops enemy from slidign when hit
            if (hasBeenHit)
                rb2d.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Hermit idle behavior
    /// </summary>
    public override void Idle()
    {
        // If the hermit is allowed to crawl, make an attack
        if (canCrawl)
        {
            Attack();
            canCrawl = false;
        }
        // If it is idle and not already playing audio
        else if(canMakeSound)
        {
            // Get a random idle clip and play it
            int rand = Random.Range(0, 3);
            AudioManager.Play(idleClips[rand], audioSource);
            StartCoroutine(AudioCooldown(IDLE_AUDIO_COOLDOWN));
        }
    }

    /// <summary>
    /// Hermit aggro behavior
    /// </summary>
    public override void Aggro()
    {
        // Since the hermit doesn't distinguish between being aggressive and idle, this just repeats the idle behavior
        Idle();
    }

    /// <summary>
    /// Hermit attack behavior
    /// </summary>
    public override void Attack()
    {
        StartCoroutine(Crawl());
    }

    /// <summary>
    /// Makes the hermit crawl in a random direction
    /// </summary>
    IEnumerator Crawl()
    {
        // Calculate a random direction to travel
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        movementDirection.Normalize();

        // Turn to face move direction and switch to a crawling animation
        transform.up = -movementDirection;
        animator.SetBool("Attacking", true);

        // Move
        Move(PASSIVE_MOVE_SPEED, movementDirection);
        isCrawling = true;


        // Get a random grunt clip and play it
        if (canMakeSound)
        {
            int rand = Random.Range(0, 5);
            AudioManager.Play(gruntClips[rand], audioSource);
            StartCoroutine(AudioCooldown(CRAWL_AUDIO_COOLDOWN));
        }
        

        // The hermit stops crawling after CRAWL_TIME seconds
        yield return new WaitForSeconds(CRAWL_TIME);
        StartCoroutine(Stop());
    }

    /// <summary>
    /// Makes the hermit move
    /// </summary>
    /// <param name="speed">The speed to move at</param>
    /// /// <param name="direction">The direction to move in</param>
    private void Move(float speed, Vector2 direction)
    {
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Makes the hermit stop moving
    /// </summary>
    IEnumerator Stop()
    {
        movementDirection = Vector2.zero;
        Move(0, movementDirection);
        isCrawling = false;
        animator.SetBool("Attacking", false);

        // Get a random slide clip and play it
        int rand = Random.Range(0, 2);
        AudioManager.Play(slideClips[rand], audioSource);

        // The hermit can crawl again after CRAWL_WAIT seconds
        yield return new WaitForSeconds(CRAWL_WAIT);
        canCrawl = true;
        hasBeenHit = false;
    }
    
    /// <summary>
    /// A cooldown for when the hermit plays idle or crawl sounds
    /// </summary>
    /// <param name="cooldown"> time in seconds to wait before allowing sounds to be played</param>
    /// <returns></returns>
    IEnumerator AudioCooldown(float cooldown)
    {
        canMakeSound = false;
        yield return new WaitForSeconds(cooldown);
        canMakeSound = true;
    }

    /// <summary>
    /// Makes the hermit take damage
    /// </summary>
    /// <param name="damage">Amount of damage the hermit takes</param>
    /// /// <param name="type">Type of damage to take</param>
    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);
        hasBeenHit = true;

        // Play the hurt sound
        AudioManager.Play(hurtClips[0], audioSource);

        // Make the hermit stop when attacked while crawling
        if (isCrawling || isFrozen)
        {
            StartCoroutine(Stop());
        }
    }

    /// <summary>
    /// Stops the hermit whenever it collides with a wall
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 15)
        {
            StartCoroutine(Stop());
        }
    }

    /// <summary>
    /// Returns whether the hermit is making an attack or not
    /// </summary>
    public bool IsAttacking()
    {
        return isCrawling;
    }

    /// <summary>
    /// Hermit death behavior
    /// </summary>
    public override void Death()
    {
        base.Death();

        // TODO: Play the hermit death sound once it is implemented
        // AudioManager.Play(AudioClipName.HERMIT_DEATH, transform.position);
    }
}

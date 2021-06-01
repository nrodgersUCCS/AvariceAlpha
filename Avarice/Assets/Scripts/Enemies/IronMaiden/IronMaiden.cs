using System.Collections;
using UnityEngine;

/// <summary>
/// An iron maiden trap enemy
/// </summary>
public class IronMaiden : Decoration
{
    // Constants
    private float CLOSE_TIME;                                                    // How long in seconds before the iron maiden closes
    private float OPEN_TIME;                                                     // How long in seconds before the iron maiden opens
    private float CLOSE_FORCE;                                                   // How powerful the force is when trying to suck the player in
    private float OPEN_FORCE;                                                    // How powerful the force is when spitting the player out
    private float ATTACK_RANGE;                                                  // How close the player has to be to get damaged by the iron maiden when it closes

    // Variables
    Vector2 originalOrientation;                                                 // The original rotation of the iron maiden
    private bool attacking = false;                                              // Whether the iron maiden is currently attacking
    private bool captured = false;                                               // Wether the player was sucessfully captured by the iron maiden
    private Rigidbody2D rb2d;                                                    // The player's rigid body

    // Animation and sound variables
    private Animator maidenAnimator = null;                                      // Iron maiden's animator
    private AudioSource maidenAudioSource = null;                                // Iron maiden's audio source

    // components
    protected GameObject bloodSplatter;                                          // blood splatter particle system

    protected override void Awake()
    {
        base.Awake();

        // Set constants
        IronMaidenTrapValues container = (IronMaidenTrapValues)(Constants.Values(ContainerType.IRONMAIDEN_TRAP));
        CLOSE_TIME = container.CloseTime;
        OPEN_TIME = container.OpenTime;
        CLOSE_FORCE = container.CloseForce;
        OPEN_FORCE = container.OpenForce;
        ATTACK_RANGE = container.AttackRange;

        // Get animator and audio source
        maidenAnimator = GetComponent<Animator>();
        maidenAudioSource = GetComponent<AudioSource>();

        // load blood splatter prefab
        bloodSplatter = Resources.Load<GameObject>("Prefabs/ParticleEffects/BloodSplatter");
    }

    /// <summary>
    /// Used for initialization
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Set variables
        rb2d = player.GetComponent<Rigidbody2D>();
        playerPickup = player.GetComponent<PlayerPickup>();
    }

    /// <summary>
    /// Iron maiden attack behavior
    /// </summary>
    public void Attack()
    {
        if (!attacking)
        {
            originalOrientation = transform.up;
            attacking = true;

            // Turn to face the player
            transform.up = -1 * (player.transform.position - gameObject.transform.position).normalized;

            // Temporarily disable collider
            GetComponent<BoxCollider2D>().enabled = false;

            // Suck the player in
            Vector2 direction = ((Vector2)transform.position - (Vector2)player.position).normalized;
            rb2d.AddForce(direction * CLOSE_FORCE, ForceMode2D.Impulse);
            captured = true;
            PlayerMovement.ControlsLocked = true;

            // Close the iron maiden after a delay
            StartCoroutine(Close());
        }
    }

    /// <summary>
    /// Change iron maiden to its close state after CLOSE_TIME seconds
    /// </summary>
    IEnumerator Close()
    {
        bool killed = false;                                                // Whether the player died from the iron maiden or not
        
        yield return new WaitForSeconds(2 * (CLOSE_TIME));

        // Start closing animation and sounds
        maidenAnimator.SetTrigger("Closing");
        maidenAnimator.SetBool("IsOpen", false);
        AudioManager.Play(AudioClipName.IRON_MAIDEN_CLOSE, maidenAudioSource);

        // If the player is within range
        if(Vector2.Distance(player.position, transform.position) <= ATTACK_RANGE || captured)
        {
            captured = true;

            // Damage the player
            killed = player.GetComponent<PlayerDeath>().DamagePlayer();

            // Lock player inside the iron maiden
            player.position = transform.position;

            // Play attack sound
            AudioManager.Play(AudioClipName.IRON_MAIDEN_ATTACK, maidenAudioSource);
        }

        // If the player is still alive
        if (!killed)
        {
            // Open again after a delay
            StartCoroutine(Open());
        }

        // delay for blood splatter
        yield return new WaitForSeconds(0.35f);

        // play blood splatter
        Instantiate(bloodSplatter, transform.position, transform.rotation);
        ParticleSystem bloodSplatterPS = bloodSplatter.GetComponent<ParticleSystem>();
        bloodSplatterPS.Play();

    }

    /// <summary>
    /// Return iron maiden to its open state after OPEN_TIME seconds
    /// </summary>
    IEnumerator Open()
    {
        yield return new WaitForSeconds(OPEN_TIME);
        
        // Start opening animation and sounds
        maidenAnimator.SetTrigger("Opening");
        maidenAnimator.SetBool("IsOpen", true);
        AudioManager.Play(AudioClipName.IRON_MAIDEN_OPEN, maidenAudioSource);

        // If the player failed to get away from the iron maiden and took damage
        if (captured)
        {
            captured = false;
            
            // Unlock player movement
            PlayerMovement.ControlsLocked = false;

            // Spit player out of the iron maiden
            Vector2 direction = -transform.up;
            rb2d.AddForce(direction * OPEN_FORCE, ForceMode2D.Impulse);
        }

        // Return variables to normal
        transform.up = originalOrientation;
        attacking = false;

        yield return new WaitForSeconds(.5f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public override void DamageItem(Vector2 location)
    {
        base.DamageItem(location);
        // Drop loot
        if (CurrentHealth <= 0)
        {
            LootManager.DropLoot(transform.position);
        }
    }

    /// <summary>
    /// Death behviour for iron maiden
    /// </summary>
    /// <param name="location"></param>
    public override void DestroyItem(Vector2 location)
    {
        base.DestroyItem(transform.position);

        // Increases player's ferocity
        FerocityManager.Instance.IncreaseFerocity();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// A class that handles player movement 
/// </summary> 
public class PlayerMove : MonoBehaviour
{
    private float moveSpeed;            // Player's move speed 
    private bool movementPaused;
    private Rigidbody2D rb2d;           // Player's rigidbody 
    private AudioSource audioSource;    // Player's audio source
    private Animator animator;          // Player's animator
    private float knockBackTimer;       // Timer for reseting movement after knockback
    private bool killAfterKnockback;    // Whether the player si flagged to die after knockback

    // World bounds vars 
    private BoxCollider2D worldBounds;      // Counding box of the current level 
    private Vector2 worldCenter;            // Center of world bounds 
    private float worldRight,               // Edges of world bounds 
                  worldLeft,
                  worldTop,
                  worldBottom;

    /// <summary> 
    /// Start is called before the first frame update 
    /// </summary> 
    void Start()
    {
        // Get player move speed 
        moveSpeed = ConfigurationUtils.PlayerMoveSpeed;
        movementPaused = false;
        knockBackTimer = 0f;
        killAfterKnockback = false;

        // Get components
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        // Set world bounds 
        worldBounds = GameObject.FindGameObjectWithTag("WorldBounds").GetComponent<BoxCollider2D>();
        worldCenter = worldBounds.bounds.center;
        worldRight = worldCenter.x + (worldBounds.size.x / 2);
        worldLeft = worldCenter.x - (worldBounds.size.x / 2);
        worldTop = worldCenter.y + (worldBounds.size.y / 2);
        worldBottom = worldCenter.y - (worldBounds.size.y / 2);
    }

    /// <summary> 
    /// Update is called once per frame 
    /// </summary> 
    void Update()
    {
        // If the player is getting knocked back
        if (knockBackTimer > 0)
            knockBackTimer -= Time.deltaTime;
        else
        {
            // Kill player after knockback if flagged
            if (killAfterKnockback)
                Destroy(gameObject);


            else if (!movementPaused)
            {
                moveSpeed = ConfigurationUtils.PlayerMoveSpeed;

                // Get vertical and horizontal vectors 
                Vector2 verticalVect = new Vector2(0, Input.GetAxis("Vertical"));
                Vector2 horizontalVect = new Vector2(Input.GetAxis("Horizontal"), 0);

                // Add vectors and normalize 
                Vector2 movementDirection = (verticalVect + horizontalVect).normalized;

                // If player is moving
                if (movementDirection != Vector2.zero)
                {
                    // Move player 
                    Vector2 position = transform.position;
                    position += movementDirection * moveSpeed * Time.deltaTime;

                    // Clamp within world bounds 
                    BoxCollider2D collider = GetComponent<BoxCollider2D>();
                    float halfWidth = (collider.size.x * transform.localScale.x) / 2,
                          halfHeight = (collider.size.y * transform.localScale.y) / 2;
                    position.x = Mathf.Clamp(position.x, worldLeft + halfWidth, worldRight - halfWidth);
                    position.y = Mathf.Clamp(position.y, worldBottom + halfHeight, worldTop - halfHeight);

                    // Finish clamping 
                    rb2d.MovePosition(position);

                    // Change sprite to match movement direction 
                    transform.up = movementDirection;

                    // Walking animation
                    animator.SetBool("Walking", true);

                    // Play walking sound
                    if (!audioSource.isPlaying)
                        AudioManager.Play(AudioClipName.vsCharacterWalk, audioSource);
                }

                else
                {
                    // Return to idle animation
                    animator.SetBool("Walking", false);
                }
            }
        }
    }

    public void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void animationPause()
    {
        movementPaused = true;
        StartCoroutine(ExecuteAfterTime(1.5f));
    }

    /// <summary>
    /// Knocks the player back
    /// </summary>
    /// <param name="source">Position to be knocked away from</param>
    public void KnockBack(Vector2 source)
    {
        // Knock back
        Vector2 direction = ((Vector2)transform.position - source).normalized;
        rb2d.AddForce(direction * 20, ForceMode2D.Impulse);

        // Lock movement
        moveSpeed = 0;
        knockBackTimer = 1f;

        // Kill player if no armor equipped
        if (GetComponent<PlayerInventory>().Armor == null)
        {
            killAfterKnockback = true;
            Destroy(GetComponent<BoxCollider2D>());
        }

        // Destroy armor
        else
            GetComponent<PlayerInventory>().RemoveArmor();
    }

    /// <summary>
    /// Delay for item animation
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        movementPaused = false;
    }
}
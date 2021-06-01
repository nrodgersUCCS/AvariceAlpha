using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// A class to handle all player movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    //Constants for the player movement
    private float MOVE_SPEED;                                               // Max veloctiy magnitude of player movement
    private float ACCELERATION_RATE;                                        // Rate of acceleration
    private float DECELERATION_RATE;                                        // Rate of deceleration
    private float MIN_SPEED_THRESHOLD;                                      // The minimum speed the player can move at before being forced to stop
    private float SPIN_SLOW_SPEED;                                          // The percent the movement speed is recuced to while spinning

    // Animation and sound variables
    private Animator playerAnimator = null;                                 // Player's animator
    private AudioSource playerAudioSource = null;                           // Player's audio source
    private List<AudioClipName> footstepClips = new List<AudioClipName>     // List of footstep audio clips to randomly choose from
    {
        AudioClipName.PLAYER_FOOTSTEP_1, AudioClipName.PLAYER_FOOTSTEP_2, AudioClipName.PLAYER_FOOTSTEP_3
    };
    private bool canPlayWallHit;                                            // Whether the wall hit sound is being delayed (to avoid rapid playing)
    private float WALL_HIT_SOUND_DELAY;                                     // How frequently wall hits can be played

    //Setting up variables needed for the player movement
    private Vector2 currentDirection;                                       // The player's current movement direction
    private Rigidbody2D rb2d;                                               // Player's rigidbody2D
    private Camera playerCam;                                               // The camera that follows the player
    private PlayerInventory playerInventory;                                // To get the movement multiplier
    private Quaternion lastRotation;                                        // The players last rotation
    public static bool controllingRotation;                                 // Whether the player's rotation is controlled by the player.

    // World bounds
    private float topBound, bottomBound, rightBound, leftBound;             // The outer bounds of the current level
    private float camTop, camBottom, camRight, camLeft;                     // Bounds for camera position

    /// <summary>
    /// Whether the player's controls are locked
    /// </summary>
    public static bool ControlsLocked { get; set; }

    /// <summary>
    /// Whether the player is spinning
    /// </summary>
    public bool IsSpinning { get; set; }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // Set constants
        PlayerValues container = (PlayerValues)Constants.Values(ContainerType.PLAYER);
        MOVE_SPEED = container.MoveSpeed;
        ACCELERATION_RATE = container.AccelerationRate / 100f;
        DECELERATION_RATE = container.DecelerationRate / 100f;
        MIN_SPEED_THRESHOLD = container.MinSpeedThreshold;
        WALL_HIT_SOUND_DELAY = container.WallHitDelay;
        SPIN_SLOW_SPEED = container.SpinMultiplier;

        // Get animator and audio source
        playerAnimator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        canPlayWallHit = true;

        //Getting the Rigidbody2D from the player prefab
        rb2d = GetComponent<Rigidbody2D>();

        // Assign player camera
        playerCam = Camera.main;
        playerCam.orthographicSize = container.CameraSize;

        // Get world bounds
        Transform boundsObject = GameObject.FindGameObjectWithTag("WorldBounds").transform;
        BoxCollider2D boundsCollider = boundsObject.GetComponent<BoxCollider2D>();
        topBound = boundsObject.position.y + (boundsCollider.size.y / 2) + boundsCollider.offset.y;
        bottomBound = boundsObject.position.y - (boundsCollider.size.y / 2) + boundsCollider.offset.y;
        rightBound = boundsObject.position.x + (boundsCollider.size.x / 2) + boundsCollider.offset.x;
        leftBound = boundsObject.position.x - (boundsCollider.size.x / 2) + boundsCollider.offset.x;
        Destroy(boundsObject.gameObject, 1f);

        // Set camera clamp values
        float verticalExtent = playerCam.orthographicSize;
        float horizontalExtent = verticalExtent * ((float)Screen.width / Screen.height);
        camLeft = leftBound + horizontalExtent;
        camRight = rightBound - horizontalExtent;
        camTop = topBound - verticalExtent;
        camBottom = bottomBound + verticalExtent;

        //Get the player inventory
        playerInventory = GetComponent<PlayerInventory>();

        // Unlock controls to start
        ControlsLocked = false;
        controllingRotation = true;
        currentDirection = Vector2.zero;
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void FixedUpdate()
    {
        // Move if alive & game isn't paused
        if (!ControlsLocked && !PauseManager.Instance.IsPaused)
        {
            // Unlock movement constraints
            rb2d.constraints = RigidbodyConstraints2D.None;

            // Get player movement input direction
            Vector2 newDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // If the player is giving movement input
            if (newDirection != Vector2.zero)
            {
                // Accelerate in the input direction
                currentDirection += newDirection * ACCELERATION_RATE;
            }

            // Otherwise, decelerate to a stop
            else
            {
                currentDirection = Vector2.Lerp(currentDirection, Vector2.zero, DECELERATION_RATE);
                if (currentDirection.magnitude < MIN_SPEED_THRESHOLD)
                    currentDirection = Vector2.zero;
            }

            // Clamp direction vector magnitude at 1
            currentDirection = Vector2.ClampMagnitude(currentDirection, 1f);

            // Create new position
            Vector2 newPos = transform.position;
            float speed = MOVE_SPEED;
            float movementMultiplier = 1 - (playerInventory.CurrentWeight / playerInventory.MaxWeight);

            // Apply movement multipliers
            // If the player is spinning an item
            if (IsSpinning != false)
                speed *= SPIN_SLOW_SPEED;
            // Inventory weight
            if (playerInventory.CurrentWeight > playerInventory.MaxWeight)      // Prevents controls from being inverted 
            {
                playerInventory.CurrentWeight = playerInventory.MaxWeight;
                speed = 0;
            }
            if (playerInventory.CurrentWeight > 0)                             // Applies the movementMultiplier to player speed
                speed *= movementMultiplier;

            if (FerocityManager.Instance.FerocityMovement > 0)
                speed += FerocityManager.Instance.FerocityMovement;            // Applies the ferocity movement multiplier to player speed

            // Updpate position
            newPos += currentDirection * speed * Time.deltaTime;

            // Clamp position in world bounds
            newPos = new Vector3(
                Mathf.Clamp(newPos.x, leftBound, rightBound),
                Mathf.Clamp(newPos.y, bottomBound, topBound),
                transform.position.z
                );

            // Move player
            rb2d.MovePosition(newPos);
        }

        // If movement is locked
        else
        {
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            currentDirection = Vector2.zero;
        }

        // If the player is moving, play walking animation
        if (currentDirection != Vector2.zero)
        {
            playerAnimator.SetBool("Walking", true);
        }

        // Otherwise, stop animation
        else
        {
            playerAnimator.SetBool("Walking", false);
        }

        // Always set angular velocity to 0 
        rb2d.angularVelocity = 0f;
    }

    /// <summary>
    /// Called after most update mechanics
    /// </summary>
    private void LateUpdate()
    {
        // Move camera with player and clamp in world bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, camLeft, camRight);
        pos.y = Mathf.Clamp(pos.y, camBottom, camTop);
        pos.z = playerCam.transform.position.z;
        playerCam.transform.position = pos;

        // Look towards mouse if alive & game isn't paused
        if (!ControlsLocked && controllingRotation && !PauseManager.Instance.IsPaused)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            lastRotation = transform.rotation;
        }
        else if (controllingRotation)
        {
            transform.rotation = lastRotation;
        }

        // Stops rotation from resetting while paused
        if (PauseManager.Instance.IsPaused && !ControlsLocked)
            transform.rotation = lastRotation;
    }

    /// <summary>
    /// Disables player's control of rotation
    /// </summary>
    public void LockRotationControl()
    {
        controllingRotation = false;
    }

    /// <summary>
    /// Enables player's control of rotation
    /// </summary>
    public void UnlockRotationControl()
    {
        controllingRotation = true;
    }


    /// <summary>
    /// Plays a footsetp sound effect.  
    /// For use with animation events
    /// </summary>
    public void PlayFootstep()
    {
        // Get a random footstep clip and play it
        int rand = UnityEngine.Random.Range(0, 3);
        AudioManager.Play(footstepClips[rand], playerAudioSource);
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If colliding with a tileset collider
        TilemapCollider2D collider = collision.gameObject.GetComponent<TilemapCollider2D>();
        if (collider != null)
        {
            // Play wall hit sound
            if (canPlayWallHit)
                StartCoroutine(PlayWallHit());
        }
    }

    /// <summary>
    /// Plays the wall hitsound with a minimum delay between clips
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayWallHit()
    {
        // Play sound
        AudioManager.Play(AudioClipName.PLAYER_WET_WALL_BUMP, playerAudioSource);
        canPlayWallHit = false;

        // Delay next possible hit sound
        yield return new WaitForSeconds(WALL_HIT_SOUND_DELAY);
        canPlayWallHit = true;
    }


    /*
    /// <summary>
    /// Slows the player down while they are spinning
    /// </summary>
    public void SlowSpinSpeed()
    {
        // If they are spinning, multiply the movement speed by the spin slow percent
        
    }
    */
}

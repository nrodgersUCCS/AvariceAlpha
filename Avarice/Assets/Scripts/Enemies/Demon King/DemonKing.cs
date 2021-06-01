using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Demon King boss enemy
/// </summary>
public class DemonKing : Enemy
{
    // The walls of the boss room
    private enum Wall { NORTH, EAST, SOUTH, WEST };

    // Constants
    private float MIN_SPEED;                                                    // Minimum movement speed
    private float MAX_SPEED;                                                    // Maximum movement speed
    private int DIRECTION_CHANGE_CHANCE;                                        // Percent chance to change directions after an attack
    private int MULTISHOT_PROBABILITY;                                          // Chance of using multishot attack, does not need to add up to 100
    private int RAPIDSHOT_PROBABILITY;                                          // Chance of using rapidshot attack, does not need to add up to 100
    private int GEOKINESIS_PROBABILITY;                                         // Chance of using geokinesis attack, does not need to add up to 100
    private int PROBABILITY_TOTAL;                                              // Total value of all attack probability constants

    // Multishot Constants
    private int MULTISHOT_FIREBALLS;                                            // Number of fireballs shot from the multishot, must be an odd number
    private float MULTISHOT_ANGLE;                                              // Angle in degrees between fireballs that are shot from the multishot
    private float MULTISHOT_ANIMATION_SPEED;                                    // The speed of the animator during the multishot attack
    private int MULTISHOT_COUNT;                                                // Number of times to make the multishot attack
    private float MULTISHOT_COOLDOWN;                                           // Cooldown before demon king can make another attack after using multishot

    // Rapidshot Constants
    private int RAPIDSHOT_FIREBALLS;                                            // Number of fireballs to be shot in one stream of the rapidshot
    private int RAPIDSHOT_COUNT;                                                // How many streams of fireballs to shoot during the rapidshot
    private float RAPIDSHOT_DELAY;                                              // Delay in seconds between rapidshot streams
    private float RAPIDSHOT_FIREBALL_DELAY;                                     // Delay in seconds between individual fireballs in each stream
    private float RAPIDSHOT_COOLDOWN;                                           // Cooldown before demon king can make another attack after using rapidshot
    private float RAPIDSHOT_VULNERABLE_TIME;                                    // Cooldown before the demon king can move again after using rapidshot
    private float RAPIDSHOT_START_ANGLE;                                        // Angle to begin the rapidshot attack
    private float RAPIDSHOT_END_ANGLE;                                          // Angle to end the rapidshot attack
    private float RAPIDSHOT_FIREBALL_SPEED;                                     // The speed of the fireballs shot in the rapidshot attack
    private int RAPIDSHOT_GAP;                                                  // Number of fireballs to not create within gaps
    private int RAPIDSHOT_MIN_GAPS;                                             // Minimum number of gaps to create
    private int RAPIDSHOT_MAX_GAPS;                                             // Maximum number of gaps to create
    private float RAPIDSHOT_AIM_OFFSET;                                         // Random range to make rapidshot fireballs be shot at a different angle by

    // Geokinesis Constants
    private float GEOKINESIS_TIME;                                              // Time that geokinesis rocks exist before splitting apart
    private float GEOKINESIS_DELAY;                                             // Delay between launching giant rocks
    private float GEOKINESIS_CREATION_DELAY;                                    // Delay between creating giant rocks
    private float GEOKINESIS_COOLDOWN;                                          // Cooldown before demon king can use another attack after using geokinesis
    private float GEOKINESIS_SPEED;                                             // Speed of the giant rocks
    private float GEOKINESIS_SMALL_SPEED;                                       // Speed of the smaller rocks
    private int GEOKINESIS_SPLIT_COUNT;                                         // Number of rocks to break into when a giant rock reaches the max range

    // Variables
    private GameObject fireballPrefab;                                          // The fireball
    private GameObject rockPrefab;                                              // The rock
    private bool aggressive;                                                    // Whether the demon king is aggressive
    private bool canAttack;                                                     // Whether the demon king can make another attack
    private bool canMove;                                                       // Whether the demon king can move or not
    private bool canRotate;                                                     // Whether the demon king can rotate freely to face the player or not
    private bool clockwise;                                                     // Whether the demon king is moving clockwise or counter clockwise
    private bool bossFightLive;                                                 // Whether the boss should fight the player or not
    private int destination;                                                    // Node that the demon king is currently trying to get to
    private float northSouthDistance;                                           // Distance between the north and south nodes
    private float eastWestDistance;                                             // Distance between the east and west nodes
    private int multishotWaves;                                                 // The number of waves of multishots that have been done already
    private float speed;                                                        // Current movement speed
    private GameObject[] nodes;                                                 // Nodes used for pathfinding
    private GameObject bossRoom;                                                // The room the boss is fought in
    private Wall currentWall;                                                   // The current wall that the demon king is moving along
    private Collider2D fightTrigger;                                            // The collider that the player needs to hit to start the boss fight
    private bool willUseRapidShot;                                              // Whether the demon king is planning to use the rapidshot attack or not
    private bool willUseGeokinesis;                                             // Whether the demon king is planning to use the geokinesis attack or not
    private SaveManager data;                                                // Player's Save Data

    // Animation and sound variables
    private List<AudioClipName> voiceLines = new List<AudioClipName>            // List of voice lines to choose from
    {
        AudioClipName.DEMON_KING_VOICE_LINE, AudioClipName.DEMON_KING_VOICE_LINE2, AudioClipName.DEMON_KING_VOICE_LINE3,
        AudioClipName.DEMON_KING_VOICE_LINE4, AudioClipName.DEMON_KING_VOICE_LINE5, AudioClipName.DEMON_KING_VOICE_LINE6
    };
    private List<AudioClipName> hurtClips = new List<AudioClipName>             // List of hurt sounds to choose from
    {
        AudioClipName.DEMON_KING_HURT, AudioClipName.DEMON_KING_HURT2, AudioClipName.DEMON_KING_HURT3
    };

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Set up constants
        DemonKingValues container = (DemonKingValues)Constants.Values(ContainerType.DEMON_KING);
        MIN_SPEED = container.MinSpeed;
        MAX_SPEED = container.MaxSpeed;
        DIRECTION_CHANGE_CHANCE = container.DirectionChangeChance;
        MULTISHOT_PROBABILITY = container.MultishotProbability;
        RAPIDSHOT_PROBABILITY = container.RapidshotProbability;
        GEOKINESIS_PROBABILITY = container.GeokinesisProbability;
        PROBABILITY_TOTAL = MULTISHOT_PROBABILITY + RAPIDSHOT_PROBABILITY + GEOKINESIS_PROBABILITY;

        // Multishot constants
        MULTISHOT_FIREBALLS = container.MultishotFireballs;
        MULTISHOT_ANGLE = container.MultishotAngle;
        MULTISHOT_ANIMATION_SPEED = container.MultishotAnimationSpeed;
        MULTISHOT_COUNT = container.MultishotCount;
        MULTISHOT_COOLDOWN = container.MultishotCooldown;

        // Rapidshot constants
        RAPIDSHOT_FIREBALLS = container.RapidshotFireballs;
        RAPIDSHOT_COUNT = container.RapidshotCount;
        RAPIDSHOT_DELAY = container.RapidshotDelay;
        RAPIDSHOT_FIREBALL_DELAY = container.RapidshotFireballDelay;
        RAPIDSHOT_COOLDOWN = container.RapidshotCooldown;
        RAPIDSHOT_VULNERABLE_TIME = container.RapidshotVulnerableTime;
        RAPIDSHOT_START_ANGLE = container.RapidshotStartAngle;
        RAPIDSHOT_END_ANGLE = container.RapidshotEndAngle;
        RAPIDSHOT_FIREBALL_SPEED = container.RapidShotFireballSpeed;
        RAPIDSHOT_GAP = container.RapidshotGap;
        RAPIDSHOT_MIN_GAPS = container.RapidshotMinGaps;
        RAPIDSHOT_MAX_GAPS = container.RapidshotMaxGaps;
        RAPIDSHOT_AIM_OFFSET = container.RapidshotAimOffset;

        // Geokinesis constants
        GEOKINESIS_TIME = container.GeokinesisTime;
        GEOKINESIS_DELAY = container.GeokinesisDelay;
        GEOKINESIS_CREATION_DELAY = container.GeokinesisCreationDelay;
        GEOKINESIS_COOLDOWN = container.GeokinesisCooldown;
        GEOKINESIS_SPEED = container.GeokinesisSpeed;
        GEOKINESIS_SMALL_SPEED = container.GeokinesisSmallSpeed;
        GEOKINESIS_SPLIT_COUNT = container.GeokinesisSplitCount;

        // Set up variables
        aggressive = false;
        canAttack = true;
        canMove = true;
        canRotate = true;
        clockwise = true;
        bossFightLive = false;
        speed = MIN_SPEED;
        destination = 0;
        currentWall = Wall.NORTH;
        willUseRapidShot = false;
        willUseGeokinesis = false;
        bossRoom = GameObject.Find("Boss Room");

        // Set up nodes
        nodes = new GameObject[4];
        nodes[0] = bossRoom.transform.Find("DemonKing Node 0").gameObject;
        nodes[1] = bossRoom.transform.Find("DemonKing Node 1").gameObject;
        nodes[2] = bossRoom.transform.Find("DemonKing Node 2").gameObject;
        nodes[3] = bossRoom.transform.Find("DemonKing Node 3").gameObject;
        northSouthDistance = Vector2.Distance(nodes[0].transform.position, nodes[1].transform.position);
        eastWestDistance = Vector2.Distance(nodes[1].transform.position, nodes[2].transform.position);

        // Set up the trigger
        fightTrigger = bossRoom.GetComponent<Collider2D>();

        // Set up prefabs and other game components
        fireballPrefab = Resources.Load<GameObject>("Prefabs/Enemies/Cherub/Fireball");
        rockPrefab = Resources.Load<GameObject>("Prefabs/Enemies/DemonKing/RockProjectile");

        // Get player save data
        data = player.GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the player is in the room
        CheckForPlayer();

        if (!isFrozen && bossFightLive)
        {
            // If demon king gets close to its current destination switch it to the next node in the list
            if (Vector2.Distance(nodes[destination].transform.position, transform.position) <= .5f)
            {
                // The next node depends on whether the demon king is currently moving clockwise or not
                if (clockwise)
                {
                    destination++;
                    if (destination >= nodes.Length)
                    {
                        destination = 0;
                    }

                    // Change currentWall to match the wall that the demon king is moving along
                    switch (destination)
                    {
                        case 0:
                            currentWall = Wall.NORTH;
                            break;
                        case 1:
                            currentWall = Wall.EAST;
                            break;
                        case 2:
                            currentWall = Wall.SOUTH;
                            break;
                        case 3:
                            currentWall = Wall.WEST;
                            break;
                    }
                }
                else
                {
                    destination--;
                    if (destination <= -1)
                    {
                        destination = nodes.Length - 1;
                    }

                    // Change currentWall to match the wall that the demon king is moving along
                    switch (destination)
                    {
                        case 0:
                            currentWall = Wall.EAST;
                            break;
                        case 1:
                            currentWall = Wall.SOUTH;
                            break;
                        case 2:
                            currentWall = Wall.WEST;
                            break;
                        case 3:
                            currentWall = Wall.NORTH;
                            break;
                    }
                }
            }

            // Become aggressive if player gets too close
            if (Vector2.Distance(player.transform.position, transform.position) <= AGGRO_RANGE && !aggressive)
            {
                aggressive = true;

                // Play random voice line
                int rand = UnityEngine.Random.Range(0, voiceLines.Count);
                AudioManager.Play(voiceLines[rand], audioSource);
            }

            // Aggressive and idle behaviors
            if (aggressive)
            {
                Aggro();
            }
            else
            {
                Idle();
            }
        } else if (!bossFightLive)
        {
            rb2d.velocity = Vector2.zero;
            Idle();
        }
    }

    /// <summary>
    /// Demon King aggro behavior
    /// </summary>
    public override void Aggro()
    {
        // Start an attack if it can
        if (canAttack)
        {
            Attack();
        }
        if (canMove)
        {
            // Calculate the direction to the current destination
            Vector2 movementDirection = ((Vector2)nodes[destination].transform.position - (Vector2)transform.position).normalized;
            Move(speed, movementDirection);
        }
        if (canRotate)
        {
            // Rotate the demon king to face the player
            transform.up = -1 * (player.transform.position - gameObject.transform.position).normalized;
        }
    }

    /// <summary>
    /// Demon King idle behavior
    /// </summary>
    public override void Idle()
    {
        // Turn off movement animations
        animator.SetBool("StrafingRight", false);
        animator.SetBool("StrafingLeft", false);
    }

    /// <summary>
    /// Checks if the player is in the collider and starts the fight if it hasn't started already.
    /// </summary>
    /// <param name="isPlayerHere"></param>
    private void CheckForPlayer()
    {
        bool temp = bossFightLive;      // Used to check if check has changed

        // If player is in the boss room
        if (fightTrigger.OverlapPoint(player.transform.position))
        {
            // Continue fight
            bossFightLive = true;

            // Play boss music if check changed to true
            if (temp != bossFightLive)
                MusicManager.Instance.ChangeMusic(AudioClipName.MUSIC_DEMON_KING);
        }

        // Otherwise pause the fight
        else
        {
            bossFightLive = false;

            // Play normal music if check changed to false
            if (temp != bossFightLive)
                MusicManager.Instance.ChangeMusic(AudioClipName.MUSIC_CLASSICAL_DEMO + Random.Range(0, 2));
        }
    }

    /// <summary>
    /// Makes the demon king move
    /// </summary>
    /// <param name="speed">The speed to move at</param>
    /// /// <param name="direction">The direction to move in</param>
    private void Move(float speed, Vector2 direction)
    {
        // Set moving animations based on the direction it is moving
        if (clockwise)
        {
            animator.SetBool("StrafingRight", true);
            animator.SetBool("StrafingLeft", false);
        }
        else
        {
            animator.SetBool("StrafingRight", false);
            animator.SetBool("StrafingLeft", true);
        }
        
        // Move the demon king
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Demon King attack behavior
    /// </summary>
    public override void Attack()
    {
        canAttack = false;
        
        // If planning to use the rapidshot attack or the geokinesis attack
        if (willUseRapidShot || willUseGeokinesis)
        {
            // Get previous node
            int previous = destination - 1;
            if (previous == -1)
            {
                previous = nodes.Length - 1;
            }

            // Only use the rapidshot and geokinesis attacks if demon king is close to a corner, if it isn't wait until it is
            if (Vector2.Distance(nodes[destination].transform.position, transform.position) <= 1f || Vector2.Distance(nodes[previous].transform.position, transform.position) <= 1f)
            {
                if (willUseRapidShot)
                {
                    StartRapidshot();
                    willUseRapidShot = false;
                }
                else if (willUseGeokinesis)
                {
                    StartGeokinesis();
                    willUseGeokinesis = false;
                }
            }
            else
            {
                canAttack = true;
            }
        }
        else
        {
            // Select an attack based on the attack probabilities
            int rand = Random.Range(1, PROBABILITY_TOTAL + 1);
            if (rand <= MULTISHOT_PROBABILITY)
            {
                StartMultishot();
            }
            else if(rand <= RAPIDSHOT_PROBABILITY + MULTISHOT_PROBABILITY)
            {
                // Wait until in position before using the rapidshot attack
                willUseRapidShot = true;
                canAttack = true;
            }
            else
            {
                // Wait until in position before using the geokinesis attack
                willUseGeokinesis = true;
                canAttack = true;
            }
        }
    }

    /// <summary>
    /// Cooldown between attacks
    /// </summary>
    /// <param name="cooldown">Time in seconds before demon king can attack again</param>
    IEnumerator AttackCooldown(float cooldown)
    {
        // Chance to change directions
        int rand = UnityEngine.Random.Range(1, 100);
        if(rand <= DIRECTION_CHANGE_CHANCE)
        {
            clockwise = !clockwise;
        }

        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }

    /// <summary>
    /// Start the multishot attack
    /// </summary>
    void StartMultishot()
    {
        multishotWaves = 0;

        // Start attack sound
        AudioManager.Play(AudioClipName.DEMON_KING_ATTACK, audioSource);

        // Playing this animation will call MultishotAttack once it ends
        animator.SetTrigger("Multishot");
        animator.SetBool("MultishotPlaying", true);
        animator.speed = MULTISHOT_ANIMATION_SPEED;
    }

    public void MultishotAttack()
    {
        GameObject fireball;                                                    // The last fireball shot
        Vector2 playerDirection;                                                // Direction to the player
        Vector2 direction1;                                                     // Direction that the fireballs need to travel
        Vector2 direction2;                                                     // Direction that the fireballs need to travel

        // Shoot a fireball directly at the player
        fireball = Instantiate(fireballPrefab, (Vector2)transform.position, Quaternion.identity);
        fireball.GetComponent<Fireball>().Shoot();

        // Set up directions
        playerDirection = (player.transform.position - gameObject.transform.position).normalized;
        direction1 = playerDirection;
        direction2 = playerDirection;

        // Shoot fireballs at set angles from the one shot directly at the player
        for (int j = 0; j < (MULTISHOT_FIREBALLS - 1) / 2; j++)
        {
            // Rotate the directions of the fireballs
            direction1 = Rotate(direction1, MULTISHOT_ANGLE);
            direction2 = Rotate(direction2, -MULTISHOT_ANGLE);

            // Shoot the fireballs
            fireball = Instantiate(fireballPrefab, (Vector2)transform.position, Quaternion.identity);
            fireball.GetComponent<Fireball>().Shoot(direction1);
            fireball = Instantiate(fireballPrefab, (Vector2)transform.position, Quaternion.identity);
            fireball.GetComponent<Fireball>().Shoot(direction2);
        }

        // Repeat this attack MULTISHOT_COUNT times
        multishotWaves++;
        if (multishotWaves >= MULTISHOT_COUNT)
        {
            // Finish animation cycle
            animator.SetBool("MultishotPlaying", false);
            animator.speed = 1f;

            // Start the cooldown
            StartCoroutine(AttackCooldown(MULTISHOT_COOLDOWN));
            multishotWaves = 0;
        }
    }

    /// <summary>
    /// Start the rapidshot attack
    /// </summary>
    void StartRapidshot()
    {
        // Stand still and charge the attack
        canMove = false;
        rb2d.velocity = Vector2.zero;
        animator.SetBool("StrafingRight", false);
        animator.SetBool("StrafingLeft", false);

        // Playing this animation will call RapidshotTrigger once it ends
        animator.SetTrigger("Rapidshot");
    }

    /// <summary>
    /// Trigger the rapidshot attack
    /// </summary>
    public void RapidshotTrigger()
    {
        StartCoroutine(RapidshotAttack());
        animator.SetBool("RapidshotPlaying", true);
    }

    /// <summary>
    /// Rapidshot attack behavior
    /// </summary>
    IEnumerator RapidshotAttack()
    {
        GameObject fireball;                                                    // The last fireball shot
        Vector2 startDirection;                                                 // Starting direction to face
        Vector2 direction;                                                      // Direction to shoot fireballs
        float rotationSpeed;                                                    // Speed to rotate during the attack
        bool reverseDirection;                                                  // The direction of rotation changes after every stream of fireballs
        float distance;                                                         // Distance for checking whether to initally reverse direction
        bool inGap = false;                                                     // Whether there should be a gap in the fireballs or not
        int gapCount;                                                           // Number of gaps to create
        int rand;                                                               // Random number used for generating gaps
        int[] gapPositions = new int[RAPIDSHOT_MAX_GAPS];                       // Positions of the gaps
        int gapIndex;                                                           // Index for gaps

        // Calculate rotation speed
        rotationSpeed = Mathf.Abs(RAPIDSHOT_END_ANGLE - RAPIDSHOT_START_ANGLE) / RAPIDSHOT_FIREBALLS;

        // Turn off the ability to rotate on its own
        canRotate = false;

        // Start the attack based on the demon king's current position
        switch (currentWall) 
        {
            case Wall.NORTH:
                startDirection = Vector2.down;
                distance = eastWestDistance / 2;
                break;
            case Wall.EAST:
                startDirection = Vector2.left;
                distance = northSouthDistance / 2;
                break;
            case Wall.SOUTH:
                startDirection = Vector2.up;
                distance = eastWestDistance / 2;
                break;
            case Wall.WEST:
                startDirection = Vector2.right;
                distance = northSouthDistance / 2;
                break;
            default:
                startDirection = Vector2.down;
                distance = eastWestDistance / 2;
                break;
        }
        direction = startDirection;

        // Reverse the starting direction depending on which corner the demon king is closest too
        if (Vector2.Distance(nodes[destination].transform.position, transform.position) <= distance)
        {
            reverseDirection = clockwise;
        }
        else
        {
            reverseDirection = !clockwise;
        }

        // Repeat the attack RAPIDSHOT_COUNT times
        for (int i = 0; i < RAPIDSHOT_COUNT; i++)
        {
            // Play attack sounds and animations
            AudioManager.Play(AudioClipName.DEMON_KING_ATTACK, audioSource);

            // Determine number of gaps randomly, unless RAPIDSHOT_MIN_GAPS is equal to RAPIDSHOT_MAX_GAPS
            if (RAPIDSHOT_MIN_GAPS == RAPIDSHOT_MAX_GAPS)
            {
                gapCount = RAPIDSHOT_MAX_GAPS;
            }
            else
            {
                gapCount = Random.Range(RAPIDSHOT_MIN_GAPS, RAPIDSHOT_MAX_GAPS);
            }
            

            // Determine positions of gaps randomly
            int fraction = RAPIDSHOT_FIREBALLS / gapCount;
            for (int j = 0; j < gapCount; j++)
            {
                // Calculate the position of one gap in a way that leaves them distributed relatively evenly
                rand = Random.Range(j * fraction, (j + 1) * fraction);

                // If too close to the previous gap, increase the value slightly
                if (j > 0)
                {
                    if (rand - gapPositions[j - 1] <= RAPIDSHOT_GAP)
                    {
                        rand += RAPIDSHOT_GAP;
                    }
                }

                // Add the position to the list
                gapPositions[j] = rand;
            }
            gapIndex = 0;
            inGap = false;

            // Start the next stream at an angle from the current direction
            if (reverseDirection)
            {
                if (clockwise)
                {
                    transform.up = Rotate(startDirection * -1, RAPIDSHOT_START_ANGLE);
                }
                else
                {
                    transform.up = Rotate(startDirection * -1, -1 * RAPIDSHOT_END_ANGLE);
                }
            }
            else
            {
                if (clockwise)
                {
                    transform.up = Rotate(startDirection * -1, RAPIDSHOT_END_ANGLE);
                }
                else
                {
                    transform.up = Rotate(startDirection * -1, -1 * RAPIDSHOT_START_ANGLE);
                }
            }
            
            // Shoot a stream of fireballs at the player
            for (int j =  0; j < RAPIDSHOT_FIREBALLS; j++)
            {
                // Rotate the demon king
                if (!reverseDirection)
                {
                    transform.up = Rotate(transform.up, rotationSpeed);
                }
                else
                {
                    transform.up = Rotate(transform.up, -1 * rotationSpeed);
                }
                direction = -1 * transform.up;

                // Add a slight random angle to the fireball directions
                direction += new Vector2(Random.Range(-1 * RAPIDSHOT_AIM_OFFSET, RAPIDSHOT_AIM_OFFSET), Random.Range(-1 * RAPIDSHOT_AIM_OFFSET, RAPIDSHOT_AIM_OFFSET));
                direction.Normalize();

                // If it is not time to make a gap in the stream
                if (!inGap)
                {
                    // Shoot a fireball
                    fireball = Instantiate(fireballPrefab, (Vector2)transform.position, Quaternion.identity);
                    fireball.GetComponent<Fireball>().Shoot(direction, RAPIDSHOT_FIREBALL_SPEED);

                    // Check when it is time to make a gap
                    if(gapIndex < RAPIDSHOT_MAX_GAPS)
                    {
                        if (j == gapPositions[gapIndex])
                        {
                            inGap = true;
                        }
                    }
                }
                // Otherwise check when it is time to continue firing again
                else if (j == gapPositions[gapIndex] + RAPIDSHOT_GAP)
                {
                    inGap = false;
                    gapIndex++;
                }

                // Wait a delay before the next fireball
                yield return new WaitForSeconds(RAPIDSHOT_FIREBALL_DELAY);
            }

            // Reverse the direction
            reverseDirection = !reverseDirection;

            // Wait a delay before the next stream of fireballs
            yield return new WaitForSeconds(RAPIDSHOT_DELAY);
        }

        // Start the vulnerable phase
        canRotate = true;
        animator.SetBool("RapidshotPlaying", false);
        yield return new WaitForSeconds(RAPIDSHOT_VULNERABLE_TIME);
        canMove = true;

        // Set moving animations based on the direction it is moving
        if (clockwise)
        {
            animator.SetBool("StrafingRight", true);
            animator.SetBool("StrafingLeft", false);
        }
        else
        {
            animator.SetBool("StrafingRight", false);
            animator.SetBool("StrafingLeft", true);
        }

        // Start the cooldown
        StartCoroutine(AttackCooldown(RAPIDSHOT_COOLDOWN));
    }

    /// <summary>
    /// Start the geokinesis attack
    /// </summary>
    void StartGeokinesis()
    {
        // Stand still and charge the attack
        canMove = false;
        rb2d.velocity = Vector2.zero;
        animator.SetBool("StrafingRight", false);
        animator.SetBool("StrafingLeft", false);

        // Play animation
        animator.SetTrigger("Geokinesis");
        animator.SetBool("GeokinesisThrowPlaying", true);
        StartCoroutine(GeokinesisAttack());
    }

    /// <summary>
    /// Geokinesis attack behavior
    /// </summary>
    IEnumerator GeokinesisAttack()
    {
        GameObject[] rocks = new GameObject[4];                         // Array of rocks
        Vector2 spawnPosition;                                          // The position to spawn rocks
        Vector2 direction;                                              // Direction to shoot rocks

        // Spawn the rocks along each wall, each rock is spawned halfway between the nodes and slightly closer to the wall
        #region RockSpawns

        // First rock
        spawnPosition = (Vector2)nodes[0].transform.position;
        spawnPosition.y -= (northSouthDistance / 2);
        spawnPosition.x += .4f;
        rocks[0] = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        AudioManager.Play(AudioClipName.ROCK_CREATION, rocks[0].GetComponent<AudioSource>());
        yield return new WaitForSeconds(GEOKINESIS_CREATION_DELAY);

        // Second rock
        spawnPosition = (Vector2)nodes[1].transform.position;
        spawnPosition.x -= (eastWestDistance / 2);
        spawnPosition.y -= .4f;
        rocks[1] = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        AudioManager.Play(AudioClipName.ROCK_CREATION, rocks[1].GetComponent<AudioSource>());
        yield return new WaitForSeconds(GEOKINESIS_CREATION_DELAY);

        // Third rock
        spawnPosition = (Vector2)nodes[2].transform.position;
        spawnPosition.y += (northSouthDistance / 2);
        spawnPosition.x -= .4f;
        rocks[2] = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        AudioManager.Play(AudioClipName.ROCK_CREATION, rocks[2].GetComponent<AudioSource>());
        yield return new WaitForSeconds(GEOKINESIS_CREATION_DELAY);

        // Fourth rock
        spawnPosition = (Vector2)nodes[3].transform.position;
        spawnPosition.x += (eastWestDistance / 2);
        spawnPosition.y += .4f;
        rocks[3] = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);
        AudioManager.Play(AudioClipName.ROCK_CREATION, rocks[3].GetComponent<AudioSource>());
        #endregion RockSpawns

        // Launch each rock at the player
        for (int i = 0; i < rocks.Length; i++)
        {
            yield return new WaitForSeconds(GEOKINESIS_DELAY);

            // Play throw clip
            AudioManager.Play(AudioClipName.ROCK_THROW, rocks[i].GetComponent<AudioSource>());

            // Calculate direction to player and throw the rock
            direction = (player.transform.position - rocks[i].transform.position).normalized;
            rocks[i].GetComponent<Rock>().Shoot(direction, GEOKINESIS_SPEED, GEOKINESIS_TIME, GEOKINESIS_SMALL_SPEED, GEOKINESIS_SPLIT_COUNT);
        }

        // Turn off throwing animation
        animator.SetBool("GeokinesisThrowPlaying", false);

        // Set moving animations based on the direction it is moving
        canMove = true;
        if (clockwise)
        {
            animator.SetBool("StrafingRight", true);
            animator.SetBool("StrafingLeft", false);
        }
        else
        {
            animator.SetBool("StrafingRight", false);
            animator.SetBool("StrafingLeft", true);
        }

        // Start the cooldown
        StartCoroutine(AttackCooldown(GEOKINESIS_COOLDOWN));
    }

    /// <summary>
    /// Helper method for rotating vectors
    /// </summary>
    /// <param name="vector">original vector</param>
    /// <param name="angle">angle to rotate by, in degrees</param>
    /// <returns></returns>
    Vector2 Rotate(Vector2 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }

    /// <summary>
    /// Deal damage
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="type"></param>
    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);

        // Play random hurtclip
        int rand = UnityEngine.Random.Range(0, hurtClips.Count);
        AudioManager.Play(hurtClips[rand], audioSource);

        // Increase speed as demon king gets closer to death
        float speedDifference = MAX_SPEED - MIN_SPEED;
        float speedMultiplier = 1 - (CurrentHealth / MaxHealth);
        speed = MIN_SPEED + speedDifference * speedMultiplier;
    }
    /// <summary>
    /// Demon King death behavior
    /// </summary>
    public override void Death()
    {
        base.Death();

        // Stop the game's music
        MusicManager.Instance.StopMusic();

        SaveManager.Instance.SaveData();

        // Defeating demon king transport player to win screen
        //TODO: might need to wait for death animation to end when death animation gets made before executing this code
        SceneManager.LoadSceneAsync("WinScreen", LoadSceneMode.Single);
    }
}

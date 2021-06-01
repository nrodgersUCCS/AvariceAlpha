using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Bandit ranger enemy
/// </summary>
public class BanditRanger : Enemy
{
    // Constants
    private float ITEM_DISTANCE;

    // Bools are set by animator on certain frames
    [SerializeField]
    private bool spawnArrow;
    [SerializeField]
    private bool attack;

    // Saved for effeciency
    private GameObject arrowPrefab;
    private AudioSource bowAudioSource;
    private AudioSource voiceAudioSource;
    
    // Used for animations
    private bool walking;

    // Used for tracking timer status
    private bool attackTimerRunning;
    private bool arrowTimerRunning;

    // Used for moving along walls
    public bool canMoveBack;
    public bool canMoveLeft;
    public bool canMoveRight;
    private GameObject backWallCheck;
    private GameObject leftWallCheck;
    private GameObject rightWallCheck;
    private bool walkingRight;

    // For moving towards items
    [SerializeField]
    private LayerMask itemLayer = 0;                // The layer item pickups lie on
    public bool showDistance;
    private List<GameObject> pickedUpLoot;          // List of loot picked up by the bandit ranger

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Set constants
        ITEM_DISTANCE = ((BanditValues)Constants.Values(ContainerType.BANDIT)).ItemDistance;

        // Saved for effeciency
        bowAudioSource = gameObject.AddComponent<AudioSource>();
        voiceAudioSource = gameObject.AddComponent<AudioSource>();
        arrowPrefab = Resources.Load<GameObject>("Prefabs/Enemies/BanditRanger/Arrow");

        // Set 3D audio settings
        voiceAudioSource.spatialBlend = 1f;
        voiceAudioSource.maxDistance = 20f;
        voiceAudioSource.rolloffMode = AudioRolloffMode.Linear;
        bowAudioSource.spatialBlend = 1f;
        bowAudioSource.maxDistance = 20f;
        bowAudioSource.rolloffMode = AudioRolloffMode.Linear;

        // New list for loot
        pickedUpLoot = new List<GameObject>();

        spawnArrow = false;

        // Wall checkers
        backWallCheck = transform.GetChild(0).gameObject;
        leftWallCheck = transform.GetChild(1).gameObject;
        rightWallCheck = transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFrozen)
        {
            // Checks if walking into wall or not
            canMoveBack = !backWallCheck.GetComponent<WallCheck>().wallHit;
            canMoveLeft = !leftWallCheck.GetComponent<WallCheck>().wallHit;
            canMoveRight = !rightWallCheck.GetComponent<WallCheck>().wallHit;

            // On specific frame of animation, spawn an arrow
            if (spawnArrow && !walking && !arrowTimerRunning)
                StartCoroutine(SpawnArrow());

            // Set animations
            animator.SetBool("Walking", walking);
            animator.SetBool("Shooting", attack);

            if (showDistance)
                Debug.Log(Vector2.Distance(transform.position, player.transform.position));
        }
    }

    /// <summary>
    /// Fixed update is called every fixed-frame
    /// </summary>
    private void FixedUpdate()
    {
        if (!isFrozen)
        // If player gets close, makes bandit ranger aggro
        if (Vector2.Distance(transform.position, player.transform.position) < AGGRO_RANGE)
        {
            // Play voice
            if (!voiceAudioSource.isPlaying)
                AudioManager.Play(AudioClipName.BANDIT_RANGER_VOICE + Random.Range(0, 7), voiceAudioSource);

            Aggro();
        }
        else
        {
            Idle();
        }
    }

    /// <summary>
    /// Idle behaviour, the bandit ranger will attempt to pickup nearby items if any are close enough
    /// </summary>
    public override void Idle()
    {
        GameObject[] items;                                               // Array of items
        Collider2D[] itemColliders = new Collider2D[20];                  // Array of colliders
        float distance = ITEM_DISTANCE;                                   // Distance to check for
        GameObject closestItem = null;                                    // The closest item, also the item that the bandit ranger will move toward

        // Get all items within ITEM_DISTANCE
        int length = Physics2D.OverlapCircleNonAlloc(transform.position, ITEM_DISTANCE, itemColliders, itemLayer);
        items = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            items[i] = itemColliders[i].gameObject;
        }

        // For every item
        foreach (GameObject item in items)
        {
            // If the item is less than the maximum distance or the currently closest item
            if (Vector2.Distance(item.transform.position, transform.position) <= distance)
            {
                distance = Vector2.Distance(item.transform.position, transform.position);
                closestItem = item;
            }
        }

        // If the closest item is within range, move toward it
        if(closestItem != null)
        {
            walking = true;
            Vector2 destination = closestItem.GetComponent<Collider2D>().ClosestPoint(transform.position);
            Vector2 direction = (destination - (Vector2)transform.position).normalized;
            transform.up = -direction;
            rb2d.velocity = direction * PASSIVE_MOVE_SPEED;
        }
        // Otherwise stand still
        else
        {
            rb2d.velocity = Vector2.zero;
            walking = false;
        }
        
    }

    /// <summary>
    /// Aggro behaviour
    /// </summary>
    public override void Aggro()
    {
        if (canMoveBack || canMoveLeft || canMoveRight)
        {
            // Move bandit ranger if player is too close & not against wall
            if (Vector2.Distance(transform.position, player.transform.position) < FLEE_RANGE && !attack)
            {
                walking = true;
                animator.SetBool("Walking", walking);
                
                Vector3 leftVector = transform.rotation * new Vector3(-1f, 0f);
                Vector3 leftMoveSpeed = leftVector * 3 * Time.deltaTime;
                Vector3 rightVector = transform.rotation * new Vector3(1f, 0f);
                Vector3 rightMoveSpeed = rightVector * 3 * Time.deltaTime;

                if (canMoveBack && !canMoveLeft && !canMoveRight)
                    walking = false;
                else if (canMoveBack)
                    rb2d.MovePosition(transform.position + (transform.up * 3 * Time.deltaTime));
                else if (!canMoveBack && canMoveLeft && !walkingRight)
                    rb2d.MovePosition(transform.position + leftMoveSpeed);
                else if (!canMoveBack && !canMoveLeft && canMoveRight)
                {
                    rb2d.MovePosition(transform.position + rightMoveSpeed);
                    walkingRight = true;
                }
                else if (!canMoveBack && !canMoveLeft && !canMoveRight)
                    walking = false;
            }
        }
        if (Vector2.Distance(transform.position, player.transform.position) >= 3 && !attack)
        {
            rb2d.velocity = Vector2.zero;
            walking = false;
        }
        if (!canMoveRight)
            walkingRight = false;

        // Rotate bandit ranger to face player
        Vector3 targ = player.transform.position - transform.position;
        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        if (!attackTimerRunning && !walking)
            Attack();
    }

    /// <summary>
    /// Adds an item to the bandit ranger's inventory when they collide with it 
    /// </summary>
    /// <param name="col">Other collider</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        // If colliding with an item that is meant to be picked up
        Item itemScript = col.transform.root.GetComponent<Item>();
        if (itemScript != null && itemScript.gameObject.layer == LayerMask.NameToLayer("ItemPickups"))
        {
            // Add item to list of loot then disable it
            pickedUpLoot.Add(itemScript.gameObject);
            itemScript.gameObject.SetActive(false);
            itemScript.transform.SetParent(transform);

            // Stop moving the bandit ranger
            rb2d.velocity = Vector2.zero;
            walking = false;
        }
    }

    /// <summary>
    /// Attack behaviour
    /// </summary>
    public override void Attack()
    {
        attack = true;
        StartCoroutine(AttackTimer(ATTACK_DELAY));
    }

    public override void TakeDamage(float damage = 1f, DamageType type = DamageType.NORMAL, float flamePower = 0f, int frostPower = 0)
    {
        base.TakeDamage(damage, type, flamePower, frostPower);

        AudioManager.Play(AudioClipName.BANDIT_RANGER_HURT, voiceAudioSource);

        // Stops sliding
        rb2d.velocity = Vector2.zero;
    }

    /// <summary>
    /// An attack timer
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
    /// spawns an arrow
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnArrow()
    {
        if (!arrowTimerRunning)
        {
            arrowTimerRunning = true;

            // Instantiates a new arrow & set aim location
            GameObject arrow = Instantiate<GameObject>(arrowPrefab);
            arrow.transform.position = transform.position;
            arrow.GetComponent<Arrow>().AimLocation = player.transform.position;

            // If big enemy, scale arrow by same amount as enemy
            if (IsBigEnemy)
                arrow.transform.localScale *= SCALE_MULTIPLIER;

            //Play shoot sound
            if (!bowAudioSource.isPlaying)
            {
                AudioManager.Play(AudioClipName.BANDIT_RANGER_SHOOT, bowAudioSource);
            }

            // Prevents extra arrows from being spawned
            yield return new WaitForSeconds(1f);
            arrowTimerRunning = false;
        }
    }

    /// <summary>
    /// Drop all picked up items on death
    /// </summary>
    public override void Death()
    {
        // Drop loot
        LootManager.DropLoot(transform.position, lootList:pickedUpLoot);

        // Increases player's ferocity
        FerocityManager.Instance.IncreaseFerocity();

        // Destroy enemy
        Destroy(gameObject);
    }
}

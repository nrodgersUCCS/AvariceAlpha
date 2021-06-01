using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeloDevil : Enemy
{
    public new float Damage { get; protected set; }     // Damage dealt by the enemy

    public float health;
    private bool aggro;
    private bool attack;
    private bool tele;
    private float teleTimer;
    private float attackTimer;
    private int frame;
    public int attackingPart;

    //Body Part GameObjects
    List<GameObject> parts;
    GameObject head;
    GameObject torso;
    GameObject left_Arm;
    GameObject right_Arm;
    GameObject left_leg;
    GameObject right_leg;

    //Targeting vars
    GameObject target;
    private Vector3 AimLocation;
    public Transform center;
    public Vector2 axis = Vector2.up;
    public Vector2 desiredPosition;
    public bool immune;
    public float radiusSpeed = 0.5f;
    public float rotationSpeed = 80.0f;
    private int path;
    private float immuneTimer;

    //audio source
    AudioSource audioSource;

    // World bounds vars 
    private BoxCollider2D worldBounds;      // Counding box of the current level 
    private Vector2 worldCenter;            // Center of world bounds 
    private float worldRight,               // Edges of world bounds 
                  worldLeft,
                  worldTop,
                  worldBottom;

    private void Start()
    {
        immune = false;
        health = 6;
        Damage = 1;
        attackingPart = 6;
        teleTimer = 8;
        immuneTimer = 0;
        attackTimer = 3;
        aggro = false;

        parts = new List<GameObject>();
        parts.Capacity = 6;

        //randomizes path
        path = Random.Range(1, 9);

        //sets player to target
        target = GameObject.FindGameObjectWithTag("Player");

        center = target.transform;

        gameObject.AddComponent<AudioSource>();

        audioSource = gameObject.GetComponent<AudioSource>();

        //Find Children by name
        head = gameObject.transform.Find("SkeloDevil_Head").gameObject;
        torso = gameObject.transform.Find("SkeloDevil_Torso").gameObject;
        left_Arm = gameObject.transform.Find("SkeloDevil_Left_Arm").gameObject;
        right_Arm = gameObject.transform.Find("SkeloDevil_Right_Arm").gameObject;
        left_leg = gameObject.transform.Find("SkeloDevil_Left_Leg").gameObject;
        right_leg = gameObject.transform.Find("SkeloDevil_Right_Leg").gameObject;

        //Set Children's part and add them to the parts list
        head.GetComponent<SkeloDevilBodyPart>().SetPart(SkeloDevilBodyPart.BodyPart.Head);
        parts.Add(head);
        torso.GetComponent<SkeloDevilBodyPart>().SetPart(SkeloDevilBodyPart.BodyPart.Torso);
        parts.Add(torso);
        left_Arm.GetComponent<SkeloDevilBodyPart>().SetPart(SkeloDevilBodyPart.BodyPart.Left_Arm);
        parts.Add(left_Arm);
        right_Arm.GetComponent<SkeloDevilBodyPart>().SetPart(SkeloDevilBodyPart.BodyPart.Right_Arm);
        parts.Add(right_Arm);
        left_leg.GetComponent<SkeloDevilBodyPart>().SetPart(SkeloDevilBodyPart.BodyPart.Left_leg);
        parts.Add(left_leg);
        right_leg.GetComponent<SkeloDevilBodyPart>().SetPart(SkeloDevilBodyPart.BodyPart.Right_leg);
        parts.Add(right_leg);

        // Set world bounds 
        worldBounds = GameObject.FindGameObjectWithTag("WorldBounds").GetComponent<BoxCollider2D>();
        worldCenter = worldBounds.bounds.center;
        worldRight = worldCenter.x + (worldBounds.size.x / 2);
        worldLeft = worldCenter.x - (worldBounds.size.x / 2);
        worldTop = worldCenter.y + (worldBounds.size.y / 2);
        worldBottom = worldCenter.y - (worldBounds.size.y / 2);

    }

    private void Update()
    {
        //timers
        attackTimer -= Time.deltaTime;
        teleTimer -= Time.deltaTime;
        immuneTimer -= Time.deltaTime;

        //if target is too close sets aggro to true
        if (target != null && Vector2.Distance(gameObject.transform.position, target.transform.position) < 10 && attack != true)
        {
            aggro = true;
        }
        else
        {
            aggro = false;

            if (!attack)
            {
                gameObject.GetComponent<Animator>().enabled = false;
                Idle();
            }
        }
        if (aggro && !attack)
        {
            Aggro();
        }
        //if attack timer = 0
        if (attack && attackTimer <= 0)
        {
            Attack();

        }
        if (attackTimer <= 0)
        {
            //randomizes path
            path = Random.Range(1, 9);
            attackTimer = 4;
        }
        //if immune == true makes all remaining children immune
        if (immune)
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i] != null) parts[i].GetComponent<SkeloDevilBodyPart>().immune = true;
            }
        }
        //once immuneTimer is lower than zero makes all remaining children not immune
        if (immuneTimer <= 0)
        {
            immune = false;
            immune = false;
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i] != null) parts[i].GetComponent<SkeloDevilBodyPart>().immune = false;
            }
        }
    }


    /// <summary>
    /// Enemy idle behavior
    /// </summary>
    public override void Idle()
    {
        // Move player 
        Vector2 position = transform.position;

        // Clamp within world bounds 
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        float halfWidth = (collider.size.x * transform.localScale.x) / 2,
              halfHeight = (collider.size.y * transform.localScale.y) / 2;

        //Moves in one of eight directions based on RandomRange
        switch (path)
        {
            case 1:

                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to the right
                position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(5, 0), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                break;

            case 2:
                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to the left
                position = Vector2.MoveTowards(transform.position, transform.position - new Vector3(5, 0), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));

                break;

            case 3:

                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to the up
                position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(0, 5), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                break;

            case 4:
                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to the down
                position = Vector2.MoveTowards(transform.position, transform.position - new Vector3(0, 5), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                break;

            case 5:

                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to up right
                position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(3, 2), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 135));
                break;

            case 6:
                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to down right
                position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(3, -2), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                break;

            case 7:

                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to the up left
                position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(-2, 3), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 225));
                break;

            case 8:
                gameObject.transform.localScale = new Vector3(1, 1.0f, 1);
                //moves enemies to the down left
                position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(-2, -3), 2 * Time.deltaTime);
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 315));
                break;
        }

        position.x = Mathf.Clamp(position.x, worldLeft + halfWidth, worldRight - halfWidth);
        position.y = Mathf.Clamp(position.y, worldBottom + halfHeight, worldTop - halfHeight);

        transform.position = position;

        if (!audioSource.isPlaying)
        {
            AudioManager.Play(AudioClipName.vsSkelodevilIdle, audioSource);
        }
    }

    /// <summary>
    /// Enemy aggro behavior
    /// </summary>
    public override void Aggro()
    {
        //if target is too close gameobject moves back to avoid being hit
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) < 4 && !attack)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, -5.5f * Time.deltaTime);
        }
        else if (!attack)
        {
            //Circles the player
            Vector3 targ = target.transform.position;
            targ.z = 0f;
            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;
            transform.RotateAround(center.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        }
        //if target is too far away moves closer
        if (Vector2.Distance(gameObject.transform.position, target.transform.position) > 7 && !attack)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, 3.5f * Time.deltaTime);
        }
        //if attackTimer is lower than zero sets attack to true
        if (attackTimer < 0)
        {
            attack = true;
            AimLocation.x = target.transform.position.x;
            AimLocation.y = target.transform.position.y;
            AimLocation.z = target.transform.position.z;

            if (teleTimer < 0)
            {
                tele = true;


                for (int i = 0; i < parts.Count; i++)
                {
                    if (parts[i] != null) parts[i].GetComponent<SpriteRenderer>().enabled = true; ;
                }

                gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
        }
    }

    /// <summary>
    /// Enemy attack behavior
    /// </summary>
    public override void Attack()
    {
        if (tele)
        {

            //Makes allremaining parts attack with a slight delay
            if (attackingPart == 1 && head != null)
            {
                head.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
                attackingPart--;
                attackTimer = .5f;
            }
            else if (attackingPart == 2 && torso != null)
            {
                torso.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
                attackingPart--;
                attackTimer = .5f;
                AudioManager.Play(AudioClipName.vsSkeloDevilAttack, audioSource);
            }
            else if (attackingPart == 3 && left_Arm != null)
            {
                left_Arm.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
                attackingPart--;
                attackTimer = .5f;
            }
            else if (attackingPart == 4 && right_Arm != null)
            {
                right_Arm.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
                attackingPart--;
                attackTimer = .5f;
                AudioManager.Play(AudioClipName.vsSkeloDevilAttack, audioSource);
            }
            else if (attackingPart == 5 && left_leg != null)
            {
                left_leg.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
                attackingPart--;
                attackTimer = .5f;
            }
            else if (attackingPart == 6 && right_leg != null)
            {
                right_leg.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
                attackingPart--;
                attackTimer = .5f;

                AudioManager.Play(AudioClipName.vsSkeloDevilAttack, audioSource);

            }
            else if (attackingPart == 0)
            {
                attackTimer = 2.4f;
                attackingPart--;
            }
            else if (attackingPart < 0)
            {
                attackTimer = 3;
                attackingPart = 6;
                teleTimer = 8;
                attack = false;
                tele = false;
            }
            //if a part is null lowers the delay so next part can attack
            else
            {
                attackingPart--;
                attackTimer = .1f;
            }
        }
        else if (!tele)
        {
            GameObject bone = Instantiate(Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/SkeloDevil_Bone"), gameObject.transform.position, Quaternion.identity);

            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.GetComponent<Animator>().enabled = true;

            AudioManager.Play(AudioClipName.vsSkeloDevilAttack, audioSource);

            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i] != null) parts[i].GetComponent<SpriteRenderer>().enabled = false; ;
            }

            gameObject.GetComponent<Animator>().Play("SkeloDevilAttack", 0);

            bone.GetComponent<SkeloDevilBodyPart>().Attack(AimLocation);
            attack = false;
            attackTimer = 1;
        }
    }

    /// <summary>
    /// Deal damage to the enemy
    /// </summary>
    /// <param name="damage">Amount of damage to do</param>
    public override void TakeDamage(float damage)
    {

        if (immune == false)
        {
            health = health - damage;

            immune = true;

            immuneTimer = 5;

            AudioManager.Play(AudioClipName.vsSkeletonHurt, audioSource);
        }

        if (health <= 0)
        {
            Death();
        }
    }

    /// <summary>
    /// Enemy death behavior
    /// </summary>
    public override void Death()
    {
        base.Death();

        AudioManager.Play(AudioClipName.vsSkeletonDeath, target.GetComponent<AudioSource>());

        gameObject.AddComponent<Pickup>();
        gameObject.GetComponent<Pickup>().DropMoney(25, 50, gameObject);

        Destroy(gameObject);
    }
    /// <summary>
    /// Brings all parts to the postion of their parent sets immune to false
    /// </summary>
    public void AssembleParts()
    {
        if (gameObject.transform.position != ((AimLocation - gameObject.transform.position).normalized * 5))
        {
            gameObject.transform.position = AimLocation + ((AimLocation - gameObject.transform.position).normalized * 5);
            gameObject.transform.Rotate(0, 0, gameObject.transform.rotation.z - 180);
        }

        if (head != null) head.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (torso != null) torso.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (left_Arm != null) left_Arm.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (right_Arm != null) right_Arm.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (left_leg != null) left_leg.GetComponent<SkeloDevilBodyPart>().Assemble();
        if (right_leg != null) right_leg.GetComponent<SkeloDevilBodyPart>().Assemble();
        immune = false;

    }
    /// <summary>
    /// Checks if player has entered collider2D
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().KnockBack(transform.position);
        }
        else if (collision.gameObject.transform.parent != null && immune == false)
        {
            if (collision.gameObject.transform.parent.tag == "Player")
            {
                TakeDamage(1);
            }
        }
    }
}
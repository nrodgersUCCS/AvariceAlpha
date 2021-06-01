using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    AudioSource audioSource;    // Skeleton audio source
    Transform target;           // Player target
    Vector2 spawn;              // Skeleton spawn location
    bool turn = false;          // Whether the skeleton is facing left or right
    bool player;                // Whether the player target exists
    bool dead = false;          // Whether the skeleton is dead


    // Start is called before the first frame update
    void Start()
    {
        //gameObject.AddComponent<Pickup>();

        audioSource = gameObject.GetComponent<AudioSource>();

        gameObject.AddComponent<BoxCollider2D>();

        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;

        // Set health
        Health = 3f;

        //finds GameObject with the "Player" tag and sets it's transform as target
        target = GameObject.FindWithTag("Player").transform;
        player = true;

        //saves position when first spawned
        spawn = transform.position;

        // Set rotation
        Quaternion roation = Quaternion.Euler(0, 0, 90);
        transform.rotation = Quaternion.Slerp(transform.rotation, roation, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            //checks if target is null and if the target's position is close enough using magnitude
            if (target != null && (target.position - transform.position).magnitude < 5)
            {
                Aggro();
            }

            else
            {
                Idle();
            }
            
        }
    }

    /// <summary>
    /// Skeleton idle behavior
    /// </summary>
    public override void Idle()
    {
        //if target has been Attacked and set to null and the skeleton is not at its spawn
        if (target == null && transform.position != (Vector3)spawn && player)
        {
            if (!audioSource.isPlaying)
            {
                AudioManager.Play(AudioClipName.vsSkeletonWalk, audioSource);
            }

            //Moves toward its starting postion
            transform.position = Vector2.MoveTowards(transform.position, spawn, 4 * Time.deltaTime);
            var dir = (Vector3)spawn - transform.position;
            var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //sets player bool to false when spawn location has been reached
            if (transform.position.magnitude == spawn.magnitude)
            {
                player = false;
            }
        }

        //Calls the Move method if their is no player
        else if (!dead)
        {
            Move();
        }
    }

    /// <summary>
    /// Used to move the skeleton in a back and forth pattern after moving 10 units turns around and moves the other way
    /// </summary>
    /// <returns></returns>
    private bool Move()
    {
        if (!audioSource.isPlaying)
        {
            AudioManager.Play(AudioClipName.vsSkeletonWalk, audioSource);
        }

        //if turn is false the skeleton moves to the right
        if (turn == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            //moves skeleton to the right
            transform.position = Vector2.MoveTowards(transform.position, spawn + new Vector2(5,0), 4 * Time.deltaTime);

            //when the skeleton moves 10 units sets turn to true and calls Damage method
            if (transform.position.x > spawn.x + 4)
            {
                turn = true;

                //used to show damage mechanic
                //Damage();

            }
            return true;
        }
        //if turn is true skeleton moves to the left
        else if (turn == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            //moves skeleton to the left
            transform.position = Vector2.MoveTowards(transform.position, spawn - new Vector2(5, 0), 4 * Time.deltaTime);
            //After moving 10 units sets turn to false again and calls Damage method
            if (transform.position.x < spawn.x - 4)
            {
                turn = false;
                //Damage();
            }
            return true;
        }

        return false;
    }

    /// <summary>
    /// Skeleton aggro behavior
    /// </summary>
    public override void Aggro()
    {
        //moves towards target
        transform.position = Vector2.MoveTowards(transform.position, target.position, 5 * Time.deltaTime);
        var dir = target.position - transform.position;
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) + 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //if within 1 of the target calles Attack method
        if ((target.position - transform.position).magnitude < 1)
        {
            Attack();
        }
    }

    /// <summary>
    /// Used to decrement the skeleton's health count. Calls Death method if health drops below 1
    /// </summary>
    public override void TakeDamage(float damage)
    {
        // If hit won't kill skeleton
        if (Health > damage)
        {
            AudioManager.Play(AudioClipName.vsSkeletonHurt, audioSource);
        }

        // Take damage
        Health -= damage;

        //if health is - or lower calls Death method
        if (Health <= 0)
        {
            Death();
        }
    }
    /// <summary>
    /// used to destroy this gameobject from the scene
    /// </summary>
    public override void Death()
    {
        base.Death();
        AudioManager.Play(AudioClipName.vsSkeletonDeath, audioSource);
        dead = true;
        gameObject.AddComponent<Pickup>();
        gameObject.GetComponent<Pickup>().DropMoney(5, 25,gameObject);
        Destroy(gameObject);
    }

    /// <summary>
    /// Used to deal damage to the player. At the moment this only destroys the GameObject with the tag "Player" 
    /// </summary>
    public override void Attack()
    {
        // Damages armor
        if (GameObject.Find("ArmorSlot") != null)
        {
            if (GameObject.Find("ArmorSlot").transform.childCount != 0)
            {
                if (GameObject.Find("ArmorSlot").transform.GetChild(0).gameObject.name == "Enchanted Armor")
                {
                    Destroy(GameObject.Find("ArmorSlot").transform.GetChild(0).gameObject);
                }
            }
            // Kills player
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerMove>().KnockBack(transform.position);
                target = null;
            }
            AudioManager.Play(AudioClipName.vsSkeletonAttack, audioSource);
        }
        else
        {
            GameObject.Destroy(GameObject.FindWithTag("Player"));
            target = null;
            AudioManager.Play(AudioClipName.vsSkeletonAttack, audioSource);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().KnockBack(transform.position);
        }
        else if (collision.gameObject.transform.parent != null)
        {
            if (collision.gameObject.transform.parent.tag == "Player")
            {
                TakeDamage(3);
            }
        }
    }
 
}


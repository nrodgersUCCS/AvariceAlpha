using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    GameObject player;          // Player object
    Vector2 startPosition;      // Starting location of the dagger
    public bool attacking;             // Whether the dagger can damage enemies
    Animator animator;          // Animator for the dagger
    AudioSource audioSource;    // Dagger's audio source
    bool missSoundPlayed;

    /// <summary>
    /// Called before Start
    /// </summary>
    void Awake()
    {
        // Set vars
        attacking = true;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        missSoundPlayed = false;
        Name = WeaponName.DAGGER;
        Type = WeaponType.MELEE;

        // Move to spawn location
        if (transform.parent != null && transform.parent.tag == "Player")
        {
            player = transform.parent.gameObject;
            Vector2 spawnOffset =
                (-transform.up * ConfigurationUtils.WeaponSpawnLocations[Name].x) +
                (transform.right * ConfigurationUtils.WeaponSpawnLocations[Name].y);
            transform.position += (Vector3)spawnOffset;
        }

        // If not thrown by player
        else
        {
            // Stop actions
            animator.speed = 0;
            attacking = false;
            missSoundPlayed = true;
        }

        // Detach parent
        transform.parent = null;

        // Set start position
        startPosition = transform.position;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If dagger is still travelling
        if (attacking && ((Vector2)transform.position - startPosition).magnitude < ConfigurationUtils.DaggerTravelDistance)
        {
            // Move forward
            transform.position += transform.right * ConfigurationUtils.MediumProjectileSpeed * Time.deltaTime;
        }

        // If max distance reached
        else
        {
            // Pause animation
            animator.speed = 0;

            // Play miss sound
            if (!missSoundPlayed)
            {
                AudioManager.Play(AudioClipName.vsDaggerMiss, audioSource);
                missSoundPlayed = true;
            }

            // Cannot damage enemies
            attacking = false;
        }
    }

    /// <summary>
    /// Dagger attack behavior
    /// </summary>
    public override void Attack()
    {
        // Play dagger throw sound
        AudioManager.Play(AudioClipName.vsDaggerThrow, audioSource);

        // Remove dagger from player inventory
        if (player != null)
        {
            if (player.GetComponent<PlayerInventory>().LeftHand.GetComponent<Dagger>() != null)
                player.GetComponent<PlayerInventory>().RemoveLeftHand();
            //else if (player.GetComponent<PlayerInventory>().RightHand.GetComponent<Dagger>() != null)
                //player.GetComponent<PlayerInventory>().RemoveRightHand();
        }
    }

    /// <summary>
    /// Called on collisions
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // If attacking
            if (attacking)
            {
                attacking = false;
                // Damage enemy
                if (collision.gameObject.TryGetComponent<Skeleton>(out Skeleton skeleton))
                {
                    skeleton.TakeDamage(3);
                }
                else if(collision.gameObject.TryGetComponent<SkeloDevilBodyPart>(out SkeloDevilBodyPart skeloDevilBodyPart))
                {
                    skeloDevilBodyPart.TakeDamage();
                }
                else if (collision.gameObject.TryGetComponent<Zombie>(out Zombie zombie))
                {
                    zombie.TakeDamage(5);
                }

                // FOR TESTING ONLY
               // Destroy(collision.gameObject);
                Destroy(gameObject);

                if (Camera.main.gameObject.GetComponent<ScreenShake>() != null)
                    Camera.main.GetComponent<ScreenShake>().Shake(0.5f, 0.2f, 1.5f);
            }
        }

        else if (collision.gameObject.tag == "Player" && !attacking)
        {
            // Add to inventory
            if (!collision.gameObject.GetComponent<PlayerInventory>().leftHandIsFull)
            {
                collision.gameObject.GetComponent<PlayerInventory>().AddLeftHand(Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Dagger"));
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("DaggerItem");
                GameObject.FindWithTag("Player").GetComponent<PlayerMove>().animationPause();
                AudioManager.Play(AudioClipName.vsItemFanfare, GameObject.FindWithTag("Player").GetComponent<AudioSource>());
                Destroy(gameObject);
            }
            //else if (!collision.gameObject.GetComponent<PlayerInventory>().rightHandIsFull)
            //    collision.gameObject.GetComponent<PlayerInventory>().AddRightHand(gameObject);
            else
            {
                // TODO: Add to player inventory if weapon slots full
            }

            
        }
    }

}

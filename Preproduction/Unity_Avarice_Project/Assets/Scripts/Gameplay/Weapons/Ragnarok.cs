using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragnarok : Weapon
{
    GameObject player;          // Player object
    Vector2 startPosition;      // Starting location of the ragnarok
    bool attacking;             // Whether the ragnarok can damage enemies
    AudioSource audioSource;    // Ragnarok's audio source
    private GameObject ragnarokDamage;

    /// <summary>
    /// Called before Start
    /// </summary>
    void Awake()
    {
        // Set vars
        attacking = true;
        audioSource = GetComponent<AudioSource>();
        ragnarokDamage = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/RagnarokDuration");

        Name = WeaponName.RAGNAROK;
        Type = WeaponType.FIRE;

        // Move to spawn location
        if (transform.parent != null && transform.parent.tag == "Player")
        {
            //player = transform.parent.gameObject;
            player = GameObject.FindWithTag("Player");
            transform.position = player.transform.position;

        }

        // If not thrown by player
        else
        {
            // Stop actions
            attacking = false;
        }

    }

    /// <summary>
    /// Ragnarok attack behavior
    /// </summary>
    public override void Attack()
    {
        // Play ragnarok throw sound
        if(GameObject.Find("RagnarokDuration(Clone)") == null)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerMove>().setMoveSpeed(0);
            Instantiate(ragnarokDamage, GameObject.FindWithTag("Player").transform);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Called on collisions
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !attacking)
        {
            // Add to inventory
            if (!collision.gameObject.GetComponent<PlayerInventory>().leftHandIsFull)
            {
                collision.gameObject.GetComponent<PlayerInventory>().AddLeftHand(Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Ragnarok"));
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("RagnarokItem");
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

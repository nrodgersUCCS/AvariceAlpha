using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    GameObject player;          // Player object
    Vector2 startPosition;      // Starting location of the crossbow
    bool attacking;             // Whether the crossbow can damage enemies
    AudioSource audioSource;    // crossbow's audio source
    private GameObject arrow;

    /// <summary>
    /// Called before Start
    /// </summary>
    void Awake()
    {
        // Set vars
        attacking = true;
        audioSource = GetComponent<AudioSource>();
        arrow = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Arrow");

        Name = WeaponName.CROSSBOW;
        Type = WeaponType.RANGED;

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
    /// Crossbow's attack behavior
    /// </summary>
    public override void Attack()
    {
        StartCoroutine(ExecuteAfterTime(0.8f));
        gameObject.GetComponent<Renderer>().enabled = false;
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
                collision.gameObject.GetComponent<PlayerInventory>().AddLeftHand(Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Crossbow"));
                GameObject.FindWithTag("Player").GetComponent<Animator>().SetTrigger("CrossbowItem");
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

    /// <summary>
    /// Delay to reset cooldown on lance
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        Instantiate(arrow, GameObject.FindWithTag("Player").transform);
        Destroy(gameObject);
    }
}

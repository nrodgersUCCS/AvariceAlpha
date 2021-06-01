using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject player;          // Player object
    Vector2 startPosition;      // Starting location of the arrow
    bool attacking;             // Whether the arrow can damage enemies
    AudioSource audioSource;    // arrow's audio source
    bool missSoundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        // Set vars
        attacking = true;
        audioSource = GetComponent<AudioSource>();
        missSoundPlayed = false;

        Destroy(gameObject, 6.0f);
        AudioManager.Play(AudioClipName.vsCrossbowShoot, audioSource);

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
        // If arrow is still travelling
        if (attacking && ((Vector2)transform.position - startPosition).magnitude < ConfigurationUtils.DaggerTravelDistance)
        {
            // Move forward
            transform.position += transform.up * ConfigurationUtils.FastProjectileSpeed * Time.deltaTime;
        }

        // If max distance reached
        else
        {

            // Play miss sound
            if (!missSoundPlayed)
            {
                AudioManager.Play(AudioClipName.vsProjectileMiss, audioSource);
                missSoundPlayed = true;
            }

            // Cannot damage enemies
            attacking = false;
        }
    }

    // Destroy arrow on hitting an enemy
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
                // FOR TESTING ONLY
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// A class to handle the fireball projectile
/// </summary>
public class Fireball : MonoBehaviour
{
    // Constants for the fireball
    private float SPEED;                                        // The flight speed of the fireball
    private float MOVE_DELAY;                                   // The time in seconds before the fireball begins moving
    private float DESTROY_TIME;                                 // The time in seconds before the fireball is destroyed if it hasn't hit anything, must be higher than MOVE_DELAY

    // Variables for fireball
    private Rigidbody2D rb2d;                                   // The fireball's rigidbody2D
    private GameObject player;                                  // The player
    private GameObject fireball;                                // The fireball

    // Animation and sound variables
    private Animator fireballAnimator = null;                   // Fireball's animator
    private AudioSource fireballAudioSource = null;             // Fireball's audio source


    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // Set Constants
        CherubValues container = (CherubValues)Constants.Values(ContainerType.CHERUB);
        SPEED = container.FireballSpeed;
        MOVE_DELAY = container.FireballDelay;
        DESTROY_TIME = container.FireballDestroyTime;

        // Get animator and audio source
        fireballAnimator = GetComponent<Animator>();
        fireballAudioSource = GetComponent<AudioSource>();

        // Get the Rigidbody2D from the fireball prefab
        rb2d = GetComponent<Rigidbody2D>();

        // Set up player and fireball prefabs
        player = GameObject.FindWithTag("Player");
        fireball = gameObject;
    }

    /// <summary>
    /// Makes the fireball fly in the direction of the player
    /// </summary>
    public void Shoot()
    {
        // Determine direction of player
        Vector2 movementDirection = (player.transform.position - gameObject.transform.position).normalized;

        // Shoot at the player
        StartCoroutine(Fire(movementDirection, SPEED));

        // Destroy the fireball after SPAWN_TIME seconds
        Destroy(gameObject, DESTROY_TIME);
    }

    /// <summary>
    /// Makes the fireball fly in the given direction
    /// </summary>
    public void Shoot(Vector2 direction, float speed = 0f)
    {
        // Default speed
        if (speed == 0f)
            speed = SPEED;

        // Shoot in that direction
        StartCoroutine(Fire(direction, speed));

        // Destroy the fireball after SPAWN_TIME seconds
        Destroy(gameObject, DESTROY_TIME);
    }

    /// <summary>
    /// Makes the fireball fly in the given direction
    /// </summary>
    IEnumerator Fire(Vector2 direction, float speed)
    {
        // Play fired sound
        AudioManager.Play(AudioClipName.FIREBALL_FIRED, fireballAudioSource);

        // Turn to face move direction
        transform.up = -direction;

        // Wait for DELAY seconds before moving
        yield return new WaitForSeconds(MOVE_DELAY);
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Called on collision
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play the wall hit sound and destroy the fireball
        AudioManager.Play(AudioClipName.FIREBALL_HITTING_WALL, transform.position);
        Destroy(gameObject);
    }
}

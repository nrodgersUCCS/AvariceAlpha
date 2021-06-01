using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to handle rock projectiles
/// </summary>
public class Rock : MonoBehaviour
{
    // Variables for rock
    private Rigidbody2D rb2d;                                   // The rock's rigidbody2D
    private GameObject player;                                  // The player
    private GameObject rock;                                    // The rock

    // Animation and sound variables
    private Animator rockAnimator = null;                   // Rock's animator
    private AudioSource rockAudioSource = null;             // Rock's audio source


    /// <summary>
    /// Used for initialization
    /// </summary>
    void Awake()
    {
        // Get animator and audio source
        rockAnimator = GetComponent<Animator>();
        rockAudioSource = GetComponent<AudioSource>();

        // Get the Rigidbody2D from the rock prefab
        rb2d = GetComponent<Rigidbody2D>();

        // Set up player and rock prefabs
        player = GameObject.FindWithTag("Player");
        rock = gameObject;
    }

    /// <summary>
    /// Makes the rock fly in the given direction and then makes them split into smaller rocks
    /// </summary>
    /// <param name="direction">The direction to move</param>
    /// <param name="speed">The speed to move at</param>
    /// <param name="destroyTime">The amount of time it takes before the rock splits into smaller rocks</param>
    /// <param name="newSpeed">The speed of the smaller rocks</param>
    /// <param name="count">The number of smaller rocks to create</param>
    public void Shoot(Vector2 direction, float speed, float destroyTime, float newSpeed, int count)
    {
        StartCoroutine(SplitFire(direction, speed, destroyTime, newSpeed, count));
    }

    /// <summary>
    /// Make the rock move
    /// </summary>
    /// <param name="direction">direction to move</param>
    /// <param name="speed">speed to move</param>
    private void Move(Vector2 direction, float speed)
    {
        // Turn to face move direction and move
        transform.up = -direction;
        rb2d.velocity = direction * speed;
    }

    /// <summary>
    /// Makes the rock fly in the given direction and then makes them split into smaller rocks
    /// </summary>
    IEnumerator SplitFire(Vector2 direction, float speed, float destroyTime, float newSpeed, int count)
    {
        float angle = 360 / count;                      // Angle to rotate rocks by
        GameObject rock;                                // The rock to shoot

        Move(direction, speed);

        // After destroyTime seconds, split the rock into multiple smaller rocks
        yield return new WaitForSeconds(destroyTime);
       
        // Spawn each rock and have them shoot at different angles around the original
        direction = Rotate(direction, angle / 2);
        for (int i = 0; i < count; i++)
        {
            // Spawn the rock, change its scale, and shoot it
            rock = Instantiate(gameObject, transform.position, Quaternion.identity);
            rock.transform.localScale = rock.transform.localScale / (count/2);
            rock.GetComponent<Rock>().Move(direction, speed);

            // Change the angle for the next rock
            direction = Rotate(direction, angle);
        }

        // Destroy the original rock
        AudioManager.Play(AudioClipName.ROCK_HIT_WALL, transform.position);
        Destroy(gameObject);
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
    /// Called on collision
    /// </summary>
    /// <param name="collision">Collision data</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play the wall hit sound and destroy the rock
        AudioManager.Play(AudioClipName.ROCK_HIT_WALL, transform.position);
        Destroy(gameObject);
    }
}

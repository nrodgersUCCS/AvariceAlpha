using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The brute enemy's shield
/// </summary>
public class BruteShield : MonoBehaviour
{
    private int currentHealth;                              // How much health the shield has
    private AudioSource audioSource;                        // Shield's audiosource
    public Transform anchorPoint { set;  private get; }     // The point the shield follows

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get audiosource from gameObject
        audioSource = GetComponent<AudioSource>();

        // Set constant
        currentHealth = ((BruteValues)Constants.Values(ContainerType.BRUTE)).ShieldHealth;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Sets shield's position & rotation to it's anchor point's position & rotation
        transform.position = anchorPoint.position;
        transform.rotation = anchorPoint.rotation;
    }

    /// <summary>
    /// Damages shield when hit by thrown weapon
    /// </summary>
    /// <param name="collision"> Object collided with </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks if collided object is a weapon
        if(collision.gameObject.GetComponent<ThrowableItem>() != null)
        {
            // Checks if weapon was thrown
            if (collision.gameObject.GetComponent<ThrowableItem>().InFlight)
            {
                // Play damage sound
                AudioManager.Play(AudioClipName.BRUTE_SHIELD_HIT, audioSource);

                // Decrease health
                currentHealth -= 1;
            }

            // Destroy shield if health is at or less than 0
            if (currentHealth <= 0)
            {
                StartCoroutine(DeathTimer(1f));
                // Play break sound
                AudioManager.Play(AudioClipName.BRUTE_SHIELD_BREAK, audioSource);
            }
        }
    }

    /// <summary>
    /// A timer that delays the shield from breaking so the last thrown weapon can bounce off
    /// </summary>
    /// <param name="timeToWait">How long to wait</param>
    /// <returns></returns>
    IEnumerator DeathTimer(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }
}

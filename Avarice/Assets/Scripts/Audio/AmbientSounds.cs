using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// This script handles ambient sounds playing in the background
/// </summary>
public class AmbientSounds : MonoBehaviour
{
    private AudioClipName currentClip;          // The current clip that should be played
    private AudioSource audioSource;            // The ambient sound prefab's audio source
    private const float minDelay = 5f;          // Minimum delay between ambient sounds
    private const float maxDelay = 20f;          // Maximum delay between ambient sounds

    /// <summary>
    /// List of all possible ambient sounds
    /// </summary>
    List<AudioClipName> ambientSounds = new List<AudioClipName>()
    {
        AudioClipName.DEMON_EATING,
        AudioClipName.DEMONS_SNARLING,
        AudioClipName.DEMON_FLYING,
        AudioClipName.AMBIENT_WIND,
        AudioClipName.AMBIENT_WIND2,
        AudioClipName.DOOMED_SOUL,
        AudioClipName.DOOMED_SOUL2,
        AudioClipName.DEMON_ROAR,
        AudioClipName.MICE_SOUND,
        AudioClipName.SLEEPING_DEMON,
        AudioClipName.DEMON_JAILER,
        AudioClipName.CRYPT_OPENING,
        AudioClipName.AMBIENT_TORCH,
    };
    
    /// <summary>
    /// Used for initialization and starting the timer
    /// </summary>
    void Start()
    {
        // Initialize audio source
        audioSource = GetComponent<AudioSource>();

        // Starts coroutine and sets intial timer
        StartCoroutine("Countdown");
    }
    
    /// <summary>
    /// Repeating timer coroutine that plays the ambient sound
    /// </summary>
    private IEnumerator Countdown()
    {
        while (true)
        {
            // Starts timer
            yield return new WaitForSeconds (Random.Range(minDelay, maxDelay));

            // Picks a random sound form the list of ambient sounds and assigns it as the current clip
            currentClip = ambientSounds[Random.Range(0, ambientSounds.Count)];

            // Plays the current clip
            AudioManager.Play(currentClip, audioSource);
        }
    }
}


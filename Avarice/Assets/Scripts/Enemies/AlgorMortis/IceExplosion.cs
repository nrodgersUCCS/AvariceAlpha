using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An icy explosion for the algormortis enemy
/// </summary>
public class IceExplosion : MonoBehaviour
{
    // Constants
    private float EXPLOSION_RADIUS;                                                                         // The radius of the explosion
    private float EXPLOSION_TIME;                                                                           // The time in seconds that the explosion stays in effect before disappearing
    private float FREEZE_TIME;                                                                              // The time in seconds that a player is frozen when hit by the explosion

    // Variables
    private GameObject player;                                                                              // The player
    private bool active = true;                                                                             // Whether the explosion is active

    // particle system
    protected GameObject iceExplosion;                                                                     // particle system for ice explosion

    // Sound variables                                        
    private AudioSource explosionAudioSource = null;                                                        // The audio source
    private List<AudioClipName> explosionClips = new List<AudioClipName>                                    // List of explosion audio clips to randomly choose from
    {
        AudioClipName.ALGOR_MORTIS_EXPLOSION, AudioClipName.ALGOR_MORTIS_EXPLOSION2, AudioClipName.ALGOR_MORTIS_EXPLOSION3
    };

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // Set constants
        AlgorMortisValues container = (AlgorMortisValues)Constants.Values(ContainerType.ALGOR_MORTIS);
        EXPLOSION_RADIUS = container.ExplosionRadius;
        EXPLOSION_TIME = container.ExplosionTime;
        FREEZE_TIME = container.FreezeTime;

        // load ice explosion prefab
        iceExplosion = Resources.Load<GameObject>("Prefabs/ParticleEffects/IceExplosion");

        // play ice explosion particle system
        Instantiate(iceExplosion, transform.position, transform.rotation);
        ParticleSystem iceExplosionPS = iceExplosion.GetComponent<ParticleSystem>();
        iceExplosionPS.Play();

        // Get audio source
        explosionAudioSource = GetComponent<AudioSource>();

        // Set up player prefab
        player = GameObject.FindWithTag("Player");

        // Begin the explosion
        StartCoroutine(Explode());

        // Get a random explosion clip and play it
        int rand = Random.Range(0, 3);
        AudioManager.Play(explosionClips[rand], explosionAudioSource);
    }

    /// <summary>
    /// Called once per frame
    /// </summary>
    void Update()
    {
        // If the player is within range and not already frozen
        if (Vector2.Distance(player.transform.position, transform.position) <= EXPLOSION_RADIUS && !PlayerMovement.ControlsLocked && active)
        {
            // Lock player movement
            PlayerMovement.ControlsLocked = true;

            //Warhammer specific frozen code
            player.GetComponent<Animator>().SetBool("Frozen", true);
            player.GetComponent<PlayerMovement>().UnlockRotationControl();
           
            // Change the player's sprite to appear frozen
            player.GetComponent<SpriteRenderer>().color = Color.cyan;

            // Play freeze sound effect
            AudioManager.Play(AudioClipName.FREEZING, player.transform.position);

            // Start the unfreeze timer
            StartCoroutine(Unfreeze());
        }
    }

    /// <summary>
    /// Unfreezes the player after FREEZE_TIME seconds
    /// </summary>
    IEnumerator Unfreeze()
    {
        yield return new WaitForSeconds(FREEZE_TIME);

        // Unlock player movement
        PlayerMovement.ControlsLocked = false;

        // Get scripts
        PlayerThrow pThrow = player.GetComponent<PlayerThrow>();
        PlayerMovement pMove = player.GetComponent<PlayerMovement>();
        Animator pAnimator = player.GetComponent<Animator>();

        //Warhammer specific frozen code
        if (pThrow.CurrentItem is Warhammer && pMove.IsSpinning)
        {
            pThrow.CurrentItem.SpecialLand(0f);
            pThrow.CurrentItem.PickedUp();
        }

        // Return the player to normal
        player.GetComponent<SpriteRenderer>().color = Color.white;
        pAnimator.SetBool("Frozen", false);
        pAnimator.speed = 1;
        pMove.IsSpinning = false;
        pMove.UnlockRotationControl();
        pThrow.CanThrow = true;

        // Play unfreeze sound effect
        AudioManager.Play(AudioClipName.UNFREEZING, player.transform.position);
    }

    /// <summary>
    /// Hides itself after EXPLOSION_TIME seconds
    /// </summary>
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(EXPLOSION_TIME);

        // Hide the explosion since it is no longer active
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        active = false;

        // Destroy itself after FREEZE_TIME + 1, which should be after the player has had the chance to be unfrozen
        yield return new WaitForSeconds(FREEZE_TIME+1);
        Destroy(gameObject);
    }

}

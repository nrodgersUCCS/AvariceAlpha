using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A class to handle the player death
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    //Vars for the script
    private float DELAY_AFTER_DEATH_SECONDS;            //A const that is the amount of real time to wait after the sound and animation are done.
    private int playerDeaths;                           //An int that tracks the amount player deaths. 
    private int itemsLost;                              //An int that tracks the amount of items the player has lost from dying. 
    private int coinsLost;                              //An int that tracks the amount of currency the player has lost from dying.
    private bool soundFinished;                         //A bool to know if the death sound has finished.
    private bool animationFinished;                     //A bool to know if the death animation has finished.
    private AudioSource playerAudioSource = null;       //The player audioSource to play sounds from
    private AudioClipName playerDeathSound;             //The player death sound clip name
    private Animator playerAnimator = null;             //The player Animator to play the death animation
    private BoxCollider2D playerCollider;               // The player's collider
    private Rigidbody2D playerRigidbody2D;              // The player's rigidbody
    private PlayerInventory playerInventory;            // The player's inventory
    
    /// <summary>
    /// The getters and setters for the properties in the player death script
    /// </summary>
    public int PlayerDeaths { get => playerDeaths; set => playerDeaths = value; }
    public int ItemsLost { get => itemsLost; set => itemsLost = value; }
    public int CoinsLost { get => coinsLost; set => coinsLost = value; }

    /// <summary>
    /// Used for initialization of variables
    /// </summary>
    void Start()
    {
        //Initializing vars
        DELAY_AFTER_DEATH_SECONDS = ((PlayerValues)Constants.Values(ContainerType.PLAYER)).DeathDelay;
        playerDeaths = 0;
        itemsLost = 0;
        coinsLost = 0;
        soundFinished = false;
        animationFinished = false;
        playerAudioSource = GetComponent<AudioSource>();
        playerDeathSound = AudioClipName.MUSIC_DEATH;
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerInventory = GetComponent<PlayerInventory>();
    }

    /// <summary>
    /// A test method to show off the player death working. To be removed once a script to handle the player taking
    /// damage is in place.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        //If the object that hit the player has the tag 'EnemyWeapon', then we know the player was hit by an enemy weapon.
        //If the object that hit the player has the tag 'Enemy', then we know the player was hit by an enemy.
        if (collision.gameObject.tag == "EnemyWeapon" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "SkeloDevilWeapon")
        {
            DamagePlayer();
        }
    }

    /// <summary>
    /// This method goes through everything needed to handle the player death.
    /// </summary>
    void PlayerDies()
    {
        //Increment up the player death counter and put it into the playerDeath var
        //for the grave in the hud to increment up the amount of total deaths
        ++playerDeaths;

        // Remove collider
        playerCollider.enabled = false;

        // Remove Rigidbody
        playerRigidbody2D.simulated = false;

        // Disbale controls
        PlayerMovement.ControlsLocked = true;

        //Play the death sound for the player
        StartCoroutine(PlayDeathSound());


        //This checks for what death animation to play
        //Defaults to NormalDeath
        switch (tag)
        {
            case "Fireball":
                playerAnimator.SetTrigger("FlameDeath");
                break;
            default:
                playerAnimator.SetTrigger("NormalDeath");
                break;
        }


        //Start the coroutine to change the scene to the hub
        StartCoroutine(ChangeSceneToHub());

        // Resets ferocity
        FerocityManager.Instance.ResetFerocity();

        //Save the game
        SaveManager.Instance.SaveData(true, false);
    }

    /// <summary>
    /// Damages the player
    /// </summary>
    /// <returns>Whether the player has been killed</returns>
    public bool DamagePlayer()
    {
        //If the player has no armor kill them
        if (playerInventory.ArmorStack.Count < 1)
        {
            

            PlayerDies();
            return true;
        }
        //If the player has armor damage the armor
        else
        {
            playerInventory.ArmorStack.Peek().ArmorTakeDamage(1);
            return false;
        }
    }

    /// <summary>
    /// This method is called on the last frame of the death animation. Sets the bool tracking if the animation is finished
    /// to true
    /// </summary>
    public void EndOfAnimationEvent()
    {
        animationFinished = true;
    }


    /// <summary>
    /// This method is called when the sound is able to play. It plays the sound, waits for it to end, then sets the bool
    /// back to true.
    /// </summary>
    private IEnumerator PlayDeathSound()
    {
        MusicManager.Instance.StopMusic();

        AudioManager.Play(playerDeathSound, playerAudioSource);

        yield return new WaitWhile(() => playerAudioSource.isPlaying);

        soundFinished = true;
    }


    /// <summary>
    ///This method waits until the sound and animation have finished, then it waits an extra second, then it changes the scene
    ///to the hub.
    /// </summary>
    private IEnumerator ChangeSceneToHub()
    {
        while (!soundFinished || !animationFinished)
        {
            yield return null;
        }

        //After the sound and animaiton are done wait an extra second
        yield return new WaitForSeconds(DELAY_AFTER_DEATH_SECONDS);

        //Switch these back to false to be extra sure they are false after the scene change
        soundFinished = false;
        animationFinished = false;

        //Load the lose screen, unloading the current scene.
        SceneManager.LoadSceneAsync("LoseScreen", LoadSceneMode.Single);

        // Re-enable player controls
        PlayerMovement.ControlsLocked = false;

        // Resets ferocity
        FerocityManager.Instance.ResetFerocity();
    }
}
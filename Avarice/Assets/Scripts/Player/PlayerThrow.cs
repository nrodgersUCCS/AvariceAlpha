using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles how the player throws a throwableItem
/// </summary>
public class PlayerThrow : MonoBehaviour
{
    //Variables
    private List<Weapon> throwStack;                                        // All the items to throw on the throw stack
    private Animator playerAnimator;                                        // The player animator
    private bool pickupFrame;                                               // If this is the frame the player picked up a decoration
    private PlayerPickup pickupItems;                                       // Access the PlayerPickup script
    private bool preventThrowTimerRunning;                                  // Determines if prevent throw timer is running

    [SerializeField]
    private GameObject handAnchor = null;                                   // The anchor to the player's hand
    private PlayerInventory playerInventory;                                // The player's inventory

    /// <summary>
    /// To track the top item on the throwable stack 
    /// </summary>
    public ThrowableItem CurrentItem { get; private set; }

    /// <summary>
    /// The current decoration the player is holding.
    /// Overrides the top item in the throw stack
    /// </summary>cur
    public Decoration CurrentDecoration { get; set; }

    /// <summary>
    /// Whether the player can throw an item
    /// </summary>
    public bool CanThrow { private get; set; }

    /// <summary>
    /// Whether the player is throwing an item
    /// </summary>
    public bool IsThrowing { get; private set; }

    /// <summary>
    /// Used for initialization
    /// </summary>
    void Start()
    {
        // Initialize throw stack
        pickupItems = GetComponent<PlayerPickup>();
        throwStack = GetComponent<PlayerInventory>().ThrowStack;
        if (throwStack.Count > 0)
            CurrentItem = throwStack[0];
        else
            CurrentItem = null;
        CanThrow = true;
        IsThrowing = false;

        // Initialize decoration to null
        CurrentDecoration = null;

        // Get player animator and audio source
        playerAnimator = GetComponent<Animator>();

        //Initialize the const animation values into the animTriggerNames dictionary
        PlayerAnimationTriggers.Initialize();

        // Get player inventory
        playerInventory = GetComponent<PlayerInventory>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void LateUpdate()
    {
        // If the throwStack is out of date, update it
        if (throwStack.Count != GetComponent<PlayerInventory>().ThrowStack.Count)
            throwStack = GetComponent<PlayerInventory>().ThrowStack;


        // If the throw stack isn't empty and the player isnt holding a room item
        if (throwStack.Count > 0 || CurrentDecoration != null)
        {
            // Check which item to instantiate on the player's hands
            if (CurrentDecoration != null)
            {
                // Check if the item was picked up this frame
                if (CurrentItem != CurrentDecoration)
                {
                    // Get current decoration
                    CurrentItem = CurrentDecoration;
                    pickupFrame = true;
                }
                else
                    pickupFrame = false;
            }
            else
                // Get current top item in throw stack
                CurrentItem = throwStack[0];


            // If there is no item instantiated
            if (handAnchor.transform.childCount <= 0)
            {
                // Show the currently held item
                CurrentItem.transform.parent = handAnchor.transform;
                CurrentItem.transform.position = handAnchor.transform.position;
                CurrentItem.transform.rotation = handAnchor.transform.rotation;
                CurrentItem.gameObject.SetActive(true);
            }

            // Get instantiated object
            Transform instantiatedObj = handAnchor.transform.GetChild(0);

            // If instantiated object is not the correct one
            if (instantiatedObj != CurrentItem.transform)
            {
                // Disable current object
                instantiatedObj.parent = gameObject.transform;
                instantiatedObj.gameObject.SetActive(false);

                // Instantiate new object
                CurrentItem.transform.parent = handAnchor.transform;
                CurrentItem.transform.position = handAnchor.transform.position;
                CurrentItem.transform.rotation = handAnchor.transform.rotation;
                CurrentItem.gameObject.SetActive(true);
            }

            //If the left mouse button is down call the throw method
            if (!PlayerMovement.ControlsLocked && CanThrow && Input.GetMouseButtonDown(0) && !pickupFrame &&
                !preventThrowTimerRunning)
            {
                Throw();
            }

            // Start the prevent throw timer if game pauses
            if (PauseManager.Instance.IsPaused)
                StartCoroutine(PreventThrowTimer(Time.unscaledDeltaTime));
        }
        // If the player is not holding an item
        else
            CurrentItem = null;

        // Always set animator to not carry items if nothing is held 
        if (CurrentDecoration == null)
        {
            playerAnimator.SetBool("HoldingItem", false);
        }
    }

    /// <summary>
    /// Throws the current item held by the player
    /// </summary>
    void Throw()
    {
        // Start animation
        playerAnimator.SetTrigger(PlayerAnimationTriggers.TriggerNames[CurrentItem.ThrowAnimation]);

        // Turn warhammer collisions on
        if (CurrentItem is Warhammer)
        {
            Warhammer hammer = (Warhammer)CurrentItem;
            hammer.TurnColliderOn();
        }

        // Delay next throw
        CanThrow = false;

        // Prevent player from picking up items while throwing
        IsThrowing = true;
    }

    /// <summary>
    /// Takes the current item that is attached to the player and 
    /// removes it off the player
    /// Called from animation event
    /// </summary>
    public void DetachItem()
    {
        // Detach item from player
        CurrentItem.Detach();

        // Remove current item
        if (CurrentItem is Decoration)
        {
            CurrentDecoration = null;
        }
        else
            throwStack.RemoveAt(0);

        // Re-enbale throw
        CanThrow = true;

        // Let player pick up items again
        IsThrowing = false;

        // Set HoldingItem to false
        playerAnimator.SetBool("HoldingItem", false);

        // Remove item weight
        playerInventory.CurrentWeight -= CurrentItem.Weight;

        // Decreases greed
        playerInventory.UpdateGreed();

        // Update throw range indicator
        GetComponentInChildren<ThrowRangeIndicator>().UpdateIndicator();
    }

    #region Special Throw Events
    ////////////////////////////////////////////////////
    // PUBLIC METHODS CALLED FROM EVENTS IN
    // PLAYER ANIMATIONS FOR SPECIFIC THROWABLE ITEMS
    ////////////////////////////////////////////////////

    /// <summary>
    /// Stops the animator from applying root motion
    /// </summary>
    public void StopApplyingRootMotion()
    {
        playerAnimator.applyRootMotion = false;
    }

    /// <summary>
    /// Increases the animation speed - for the spin animation
    /// </summary>
    public void IncreaseAnimatorSpeed()
    {
        if (playerAnimator.speed < ((PlayerValues)Constants.Values(ContainerType.PLAYER)).MaxSpinSpeed)
            playerAnimator.speed *= 1.2f;
    }

    /// <summary>
    /// Resets the player's animation speed
    /// </summary>
    public void ResetAnimatorSpeed()
    {
        playerAnimator.speed = 1f;
    }

    /// <summary>
    /// Plays the warhammer swing sound
    /// </summary>
    public void PlayWarhammerSwingSound()
    {
        Warhammer wh = (Warhammer)CurrentItem;
        wh.PlaySwingSound();
    }

    #endregion

    /// <summary>
    /// A timer used to prevent the player from throwing held weapon as soon as they unpause the game
    /// </summary>
    /// <param name="timeToWait">How long to wait</param>
    /// <returns></returns>
    IEnumerator PreventThrowTimer(float timeToWait)
    {
        preventThrowTimerRunning = true;
        yield return new WaitForSeconds(timeToWait);
        preventThrowTimerRunning = false;
    }
}

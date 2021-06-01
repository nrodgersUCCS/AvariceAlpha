using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A gate
/// </summary>
public class GateController : MonoBehaviour
{
    private RoomChecker roomChecker;             // Checks if enemies & player are in current room
    private BoxCollider2D gateCollider;          // The gate's collider
    private Animator animator;                   // The gate's animator
    private SpriteRenderer spriteRenderer;       // The gate's sprite renderer
    private AudioSource audioSource;             // The gate's audio source

    /// <summary>
    /// Whether the gate is open
    /// </summary>
    public bool IsOpen { get; private set; }

    /// <summary>
    /// Runs before start
    /// </summary>
    private void Awake()
    {
        // If not part of boss room, find room checker gameObject
        if (transform.parent.name != "Boss Room")
            roomChecker = transform.parent.Find("RoomChecker").GetComponent<RoomChecker>();

        // If part of boss room, get RoomChecker script from the boss room gameObject
        else
            roomChecker = transform.parent.gameObject.GetComponent<RoomChecker>();

        // Add this gate to the room checker
        roomChecker.AddGate(this);

        // Saved for effeciency
        gateCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        IsOpen = true;
    }

    /// <summary>
    /// Closes the gate
    /// </summary>
    public void CloseGate()
    {
        IsOpen = false;

        // Plays the gate's closing animation
        animator.SetTrigger("close");

        // Enables gate's collider
        gateCollider.enabled = true;

        // Changes gate's sorting layer so the player, weapons, and enemies appear behind it
        spriteRenderer.sortingLayerName = "Weapons";
        spriteRenderer.sortingOrder = 1;

        // Plays the gate sound
        AudioManager.Play(AudioClipName.GATE_OPENING, audioSource);
    }

    /// <summary>
    /// Opens the gate
    /// </summary>
    public void OpenGate()
    {
        IsOpen = true;

        // Plays the gate's opening animation
        animator.SetTrigger("open");

        // Disables the gate's collider
        gateCollider.enabled = false;

        // Changes gate's sorting layer so player, weapons, and enemies appear above it
        spriteRenderer.sortingLayerName = "Player";
        spriteRenderer.sortingOrder = -1;

        // Plays the gate sound
        AudioManager.Play(AudioClipName.GATE_OPENING, audioSource);
    }
}

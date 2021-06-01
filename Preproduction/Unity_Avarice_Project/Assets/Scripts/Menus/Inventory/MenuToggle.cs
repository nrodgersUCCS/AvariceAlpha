using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An in-game menu manager
/// </summary>
public class MenuToggle : MonoBehaviour
{
    public Canvas Inventory;   // Player inventory canvas
    AudioSource audioSource;                // Player audio source

    void Start()
    {
        // Set audio source
        audioSource = GetComponent<AudioSource>();

        // set the component of Canvas to canvas and then toggle it off
        Inventory.enabled = false;

    }

    void Update()
    {
        // On keydown for tab enable or disable the canvas
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Inventory.enabled = !Inventory.enabled;

            // Play open inventory sound
            if (Inventory.enabled)
                AudioManager.Play(AudioClipName.vsOpenInventory, audioSource);
        }
    }
}
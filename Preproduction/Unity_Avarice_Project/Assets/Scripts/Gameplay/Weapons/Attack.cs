using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Create animator object
    Animator animator;
    AudioSource audioSource;    // Weapon audio source

    void Start()
    {
        // Set animator and audio source
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // On keydown press for space set the trigger for the attack animation
        if (Input.GetKeyDown("space"))
        {
            animator.SetTrigger("SwordAttack");
            AudioManager.Play(AudioClipName.vsSwordMiss, audioSource);
        }
    }
}

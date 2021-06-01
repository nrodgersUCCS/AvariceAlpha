using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles playing the break audio for decorations
/// </summary>
public class DecorationBreak : Decoration
{
    // Start is called before the first frame update
    protected override void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        AudioManager.Play(AudioClipName.WOOD_BREAKING, audioSource);
    }
}

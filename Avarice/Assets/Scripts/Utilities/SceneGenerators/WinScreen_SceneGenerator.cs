using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scene generator for the win screen
/// </summary>
public class WinScreen_SceneGenerator : MonoBehaviour
{
    AudioSource audioSource; // The source that plays sound

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        AudioManager.Play(AudioClipName.MUSIC_WIN_ANTHEM, audioSource); // play the win anthem
    }
}

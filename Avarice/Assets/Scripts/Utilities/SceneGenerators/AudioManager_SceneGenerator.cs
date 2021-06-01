using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A scene generator for testing the audio manager
/// </summary>
public class AudioManager_SceneGenerator : MonoBehaviour
{
    AudioSource audioSource; // The source that plays sound

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        AudioManager.Play(AudioClipName.AUDIOMANAGER_TEST_CLIP, audioSource);
    }
}

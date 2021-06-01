using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A music manager
/// </summary>
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;   // Singleton object
    private AudioSource audioSource;        // The gameobject's audio source
    private AudioClipName currentClip;      // The clip currently playing
    private float targetVolume;             // The current target volume of the audio source
    private bool changingVolume;            // Determines if volume is being changed

    /// <summary>
    /// Creates singleton music manager object
    /// </summary>
    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("MusicManager").AddComponent<MusicManager>();
                instance.audioSource = instance.gameObject.AddComponent<AudioSource>();
                instance.audioSource.loop = true;
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Allows the audio source to keep playing while the game is paused
        audioSource.ignoreListenerPause = true;

        // Chooses one of the background music pieces to play
        currentClip = AudioClipName.MUSIC_CLASSICAL_DEMO + Random.Range(0, 2);
        AudioManager.PlayLoop(currentClip, audioSource);
        targetVolume = audioSource.volume;
    }

    /// <summary>
    /// Changes music volume when paused
    /// </summary>
    /// <param name="volume">The volume to change to</param>
    public void ChangeVolume(float volume)
    {
        // Updates the audio source volume
        audioSource.volume = volume;
        targetVolume = volume;
    }

    /// <summary>
    /// Changes the game's music to a new track
    /// </summary>
    /// <param name="newTrack"></param>
    public void ChangeMusic(AudioClipName newTrack)
    {
        StartCoroutine(FadeMusic(newTrack));
    }

    /// <summary>
    /// Stops the music
    /// </summary>
    public void StopMusic()
    {
        audioSource.Stop();
    }

    /// <summary>
    /// Fades current track out and next track in
    /// </summary>
    /// <param name="newTrack">Music track to change to</param>
    private IEnumerator FadeMusic(AudioClipName newTrack)
    {
        // Fade the current music out
        StartCoroutine(FadeOut());
        while (changingVolume)
        {
            yield return null;
        }

        // Stop the audio source
        audioSource.Stop();

        // Change the audio clip
        AudioManager.PlayLoop(newTrack, audioSource);
        currentClip = newTrack;

        // Fade new music in
        StartCoroutine(FadeIn());
    }

    /// <summary>
    /// Lowers the volume of the music over time
    /// </summary>
    private IEnumerator FadeOut()
    {
        changingVolume = true;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.01f;
            yield return null;
        }
        changingVolume = false;
    }

    /// <summary>
    /// Increases the volume of the music over time
    /// </summary>
    private IEnumerator FadeIn()
    {
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += 0.01f;
            yield return null;
        }
    }
}

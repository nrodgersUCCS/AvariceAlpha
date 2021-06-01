using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A class for UI buttons
/// </summary>
public class MenuButton : MonoBehaviour
{
    [SerializeField]
    string displayText = "";                    // The text the button displays

    private AudioSource audioSource;            // The button's audio source
    private bool loadingNewScene;               // Checks if a new scene is being loaded

    // The button's text component
    public Text ButtonText { get; set; }

    // Awake is called before the start
    private void Awake()
    {
        // Saved for effeciency
        ButtonText = transform.GetChild(0).gameObject.GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();

        // Set button text
        ButtonText.text = displayText;

        // Allows the audio source to still play sound when game is paused
        audioSource.ignoreListenerPause = true;

        // Adds an EventSystem to the scene if one doesn't exist
        if (GameObject.Find("EventSystem") == null)
        {
            GameObject eSystem = new GameObject("EventSystem");
            eSystem.AddComponent<EventSystem>();
            eSystem.AddComponent<StandaloneInputModule>();
        }

        
    }

    /// <summary>
    /// Plays a sound when the mouse hovers over the button
    /// </summary>
    public void PlayMouseOverSound()
    {
        // Stops button's current sound and play mouse over sound
        audioSource.Stop();
        AudioManager.Play(AudioClipName.BUTTON_ENTER, audioSource);
    }

    /// <summary>
    /// Plays a sound when the button is clicked
    /// </summary>
    public void PlayClickSound()
    {
        // Stops button's current sound and play mouse over sound
        audioSource.Stop();
        AudioManager.Play(AudioClipName.BUTTON_PRESS, PauseManager.Instance.GetComponent<AudioSource>()); // Using PauseManager's audio source allows click sound to play after button is gone
    }

    /// <summary>
    /// Unpauses the game
    /// </summary>
    public void Unpause()
    {
        PauseManager.Instance.Unpause();
    }

    /// <summary>
    /// Starts coroutine to load new level
    /// </summary>
    /// <param name="sceneToLoad">The new scene to go to</param>

    public void LoadGame(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    
    /// <summary>
    /// Makes a new game
    /// </summary>
    /// <param name="sceneToLoad"></param>
    public void NewGame(string sceneToLoad)
    {
        DeleteSave();

        LoadGame(sceneToLoad);
    }

    /// <summary>
    /// Opens the instructions page
    /// </summary>
    public void OpenInstructionsPage()
    {
        FindObjectOfType<PauseManager>().CreateInstructionsPage();
    }

    /// <summary>
    /// Deletes saved data
    /// </summary>
    public void DeleteSave()
    {
        // Where data is saved
        string path = Application.persistentDataPath + "/save.dat";

        // Delete data if file exists
        if (File.Exists(path))
        {
            try
            {
                File.Delete(path);
                #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
                #endif
                Debug.Log("Save file deleted");
            }
            catch
            {
                Debug.LogWarning("Could not delete save file!");
            }
        }
        else
        {
            Debug.Log("No save file detected");
        }
    }

    // Restarts the gameplay music
    public void RestartMusic()
    {
        MusicManager.Instance.ChangeMusic(AudioClipName.MUSIC_CLASSICAL_DEMO + Random.Range(0, 2));
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}

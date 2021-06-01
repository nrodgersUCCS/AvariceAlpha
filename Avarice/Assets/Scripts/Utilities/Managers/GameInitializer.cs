using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to initialize important methods at run time
/// </summary>
public class GameInitializer : MonoBehaviour
{
    /// <summary>
    /// Awake called before start
    /// </summary>
    void Awake()
    {
        // TODO: Turn audio manager into a singleton and call the pause manager for the first time elsewhere
        // so that this script isn't needed

        // Initialize audio manager
        if (!AudioManager.Initialized)
        {
            AudioManager.Initialize();
        }

        // Initalizes the pause manager
        PauseManager.Instance.Unpause();
    }
}

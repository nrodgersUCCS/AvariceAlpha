using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    static bool initialized = false;                // Whether the game manager has been initialized
    static Dictionary<SceneName, Scene> scenes =    // Dictionary of scenes
        new Dictionary<SceneName, Scene>();

    /// <summary>
    /// Gets whether the game manager is initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initialize the game manager
    /// </summary>
    public static void Initialize()
    {
        // Initialize the game manager
        initialized = true;

        // TODO: Load scenes into dictionary
    }

    /// <summary>
    /// Loads a scene
    /// </summary>
    /// <param name="name">Scene name</param>
    public static void LoadScene(SceneName name)
    {
        // TODO: Load scene
    }
}

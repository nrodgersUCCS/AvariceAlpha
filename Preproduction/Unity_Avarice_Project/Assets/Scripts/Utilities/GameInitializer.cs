using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    /// <summary>
    /// Awake called before start
    /// </summary>
    void Awake()
    {
        // Initialize game manager
        if (!GameManager.Initialized)
            GameManager.Initialize();

        // Initialize configuration utils
        if (!ConfigurationUtils.Initialized)
            ConfigurationUtils.Initialize();

        // Initialize audio manager
        if (!AudioManager.Initialized)
            AudioManager.Initialize();
    }
}

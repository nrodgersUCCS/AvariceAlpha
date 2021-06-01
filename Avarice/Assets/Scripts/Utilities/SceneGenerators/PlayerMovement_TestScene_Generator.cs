﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement test scene generator
/// </summary>
public class PlayerMovement_TestScene_Generator : MonoBehaviour
{
    private GameObject playerPrefab;    // Player prefab
    private GameObject terrain;         // Scene terrain prefab

    /// <summary>
    /// Called before start
    /// </summary>
    private void Awake()
    {
        // Get prefab assets
        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
        terrain = Resources.Load<GameObject>("Prefabs/Environment/Terrain/PlayerMovement_TestSCene_Terrain");

        // Instantiate player
        Instantiate(playerPrefab);

        // Instantiate terrain
        Instantiate(terrain);
    }
}

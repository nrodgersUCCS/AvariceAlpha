using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dungeon Floors Rework
/// </summary>
public class NewDungeonWallsAndFloorTiles512pxDEMO_Generator : MonoBehaviour
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
        terrain = Resources.Load<GameObject>("Prefabs/Environment/Terrain/NewDungeonWallsAndFloorTiles-512px-DEMO_Terrain");

        // Instantiate player
        Instantiate(playerPrefab);

        // Instantiate terrain
        Instantiate(terrain);
    }
}

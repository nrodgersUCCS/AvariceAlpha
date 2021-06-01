using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;
    private GameObject worldBoundsPrefab;
    private GameObject terrainPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBoundsPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        terrainPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/SceneTerrainPrefabs/Walking_Test_Terrain");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        Instantiate<GameObject>(worldBoundsPrefab);

        // Terrain
        Instantiate<GameObject>(terrainPrefab);

        // Player
        Instantiate<GameObject>(playerPrefab);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;
    private GameObject worldBoundsPrefab;
    private GameObject daggerPrefab;
    private GameObject zombiePrefab;

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
        daggerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Dagger");
        zombiePrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Zombie");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        Instantiate(worldBoundsPrefab);

        // Player
        GameObject player = Instantiate(playerPrefab, new Vector3(-5f, 0f), Quaternion.identity);

        // Daggers
        Instantiate(daggerPrefab);

        // Test dummies
        GameObject zombie = Instantiate(zombiePrefab, new Vector3(5.5f, 2.5f), Quaternion.identity);
    }
}

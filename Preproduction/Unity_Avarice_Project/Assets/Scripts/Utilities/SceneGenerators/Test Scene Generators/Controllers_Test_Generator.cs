using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to instantiate objects in a scene
/// </summary>
public class Controllers_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject player;                                          // Player prefab
    private GameObject worldBounds;                                     // World bounds prefab

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        player = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////
        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);
        Instantiate<GameObject>(player, Vector3.zero, Quaternion.identity);
    }
}

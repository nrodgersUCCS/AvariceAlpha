using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragnarok_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;
    private GameObject worldBoundsPrefab;
    private GameObject ragnarokPrefab;
    private GameObject dummyPrefab;

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
        ragnarokPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Ragnarok");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

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
        Instantiate(ragnarokPrefab);

        // Test dummies
        GameObject dummy = Instantiate(dummyPrefab, new Vector3(5.5f, 2.5f), Quaternion.identity);
        {
            // Set tag
            dummy.tag = "Enemy";

            // Rotate
            dummy.transform.right = -dummy.transform.up;

            // Attach sprite renderer and collider
            dummy.AddComponent<SpriteRenderer>();
            dummy.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Enemies/FleshySkeleton/spr_EnemyFleshySkeleton0");
            dummy.AddComponent<BoxCollider2D>();
            dummy.GetComponent<BoxCollider2D>().offset = new Vector2(0.05731204f, 0.1667516f);
            dummy.GetComponent<BoxCollider2D>().size = new Vector2(1.73679f, 0.9758587f);

            // Add 2 more
            Instantiate(dummy, new Vector2(3.5f, 0f), dummy.transform.rotation);
            Instantiate(dummy, new Vector3(5.5f, -2.5f), dummy.transform.rotation);
        }

        Camera.main.gameObject.AddComponent<ScreenShake>();
    }
}

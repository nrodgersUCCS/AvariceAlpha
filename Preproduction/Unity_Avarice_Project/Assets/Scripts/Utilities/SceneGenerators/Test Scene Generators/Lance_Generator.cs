using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;        // Player prefab
    private GameObject worldBounds;         // World bounds prefab
    private GameObject dummyPrefab;         // Dummy prefab

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        // Player
        GameObject player = Instantiate<GameObject>(playerPrefab, Vector3.zero, Quaternion.identity);

        // Test dummy
        {
            GameObject dummy = Instantiate<GameObject>(dummyPrefab, new Vector2(3.61f, -0.15f), Quaternion.identity);

            // Set tag
            dummy.tag = "Enemy";

            // Set rotation
            dummy.transform.RotateAround(dummy.transform.position, Vector3.forward, 270);

            // Set sprite to skeleton
            SpriteRenderer sr = dummy.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/Enemies/FleshySkeleton/spr_EnemyFleshySkeleton0");

            // Add box collider and set to correct size
            BoxCollider2D col = dummy.AddComponent<BoxCollider2D>();
            col.offset = new Vector2(0.05416262f, -0.0464251f);
            col.size = new Vector2(1.708872f, 1.414846f);

            // Add Rigidbody
            dummy.AddComponent<Rigidbody2D>();
        }
    }
}

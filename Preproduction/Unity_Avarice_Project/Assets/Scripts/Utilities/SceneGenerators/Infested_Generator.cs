using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infested_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;        // Player prefab
    private GameObject worldBounds;         // World bounds prefab
    private GameObject layoutPrefab;         // Sword prefab

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        layoutPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/SceneTerrainPrefabs/InfestedLayout");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        GameObject world = Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        world.GetComponent<BoxCollider2D>().offset = new Vector2(-1.167614f, -7.125618f);
        world.GetComponent<BoxCollider2D>().size = new Vector2(73.69459f, 99.08464f);

        // Layout
        Instantiate<GameObject>(layoutPrefab, Vector3.zero, Quaternion.identity);

        // Player
        GameObject player = Instantiate<GameObject>(playerPrefab, Vector3.zero, Quaternion.identity);
        player.transform.position = new Vector3(-29, 35, 0);

        // Follows player
        Camera.main.gameObject.AddComponent<CameraFollowPlayer>();
        Camera.main.gameObject.GetComponent<CameraFollowPlayer>().playerCamera = player;

    }
}

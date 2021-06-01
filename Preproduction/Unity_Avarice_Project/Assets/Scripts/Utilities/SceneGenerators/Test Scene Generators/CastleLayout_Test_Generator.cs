using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleLayout_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;        // Player prefab
    private GameObject worldBounds;         // World bounds prefab
    private GameObject layoutPrefab;         // Sword prefab
    private GameObject skeloDevilPrefab;    // SkeloDevil Prefab
    private GameObject skeletonPrefab;    // Skeleton Prefab
    private GameObject zombiePrefab;
    private GameObject swordPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        layoutPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/SceneTerrainPrefabs/CastleLayout");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        skeloDevilPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/SkeloDevil");
        skeletonPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        skeletonPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        zombiePrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Zombie");
        swordPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Sword");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        GameObject world = Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        world.GetComponent<BoxCollider2D>().offset = new Vector2(20.32236f, -1.566883f);
        world.GetComponent<BoxCollider2D>().size = new Vector2(97.42408f, 52.94534f);

        // Layout
        Instantiate<GameObject>(layoutPrefab, Vector3.zero, Quaternion.identity);

        // Player
        GameObject player = Instantiate<GameObject>(playerPrefab, new Vector3(39.5f, 21, 0), Quaternion.identity);
        GameObject skeloDevil = Instantiate<GameObject>(skeloDevilPrefab, Vector3.zero, Quaternion.identity);
        GameObject skeleton = Instantiate<GameObject>(skeletonPrefab, new Vector3(34, -1, 0), Quaternion.identity);
        GameObject zombie = Instantiate<GameObject>(zombiePrefab, new Vector3(35, -15, 0), Quaternion.identity);
        GameObject sword = Instantiate(swordPrefab, player.transform);
        sword.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        skeloDevil.AddComponent<Rigidbody2D>();
        skeloDevil.GetComponent<BoxCollider2D>().enabled = true;
        skeloDevil.GetComponent<BoxCollider2D>().isTrigger = false;

        // Follows player
        Camera.main.gameObject.AddComponent<CameraFollowPlayer>();
        Camera.main.gameObject.GetComponent<CameraFollowPlayer>().playerCamera = player;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;        // Player prefab
    private GameObject worldBounds;         // World bounds prefab
    private GameObject fadePrefab;          // Fade canvas prefab
    private GameObject layoutPrefab;         // Sword prefab
    private GameObject skeloDevilPrefab;    // SkeloDevil Prefab
    private GameObject skeletonPrefab;    // Skeleton Prefab
    private GameObject zombiePrefab;
    private GameObject swordPrefab;
    private GameObject TraderPrefab;
    private GameObject TraderUIPrefab;
    private GameObject dummyPrefab;

    private GameObject bgAudioSource;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        fadePrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/FadeCanvas");
        layoutPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/SceneTerrainPrefabs/CastleLayout");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        skeloDevilPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/SkeloDevil");
        skeletonPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        skeletonPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        zombiePrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Zombie");
        swordPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Sword");
        TraderPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/Trader");
        TraderUIPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/TraderUI");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // Add camera screen shake
        Camera.main.gameObject.AddComponent<ScreenShake>();

        // World bounds
        GameObject world = Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        // Fade canvas
        GameObject fade = Instantiate(fadePrefab);

        world.GetComponent<BoxCollider2D>().offset = new Vector2(20.32236f, -1.566883f);
        world.GetComponent<BoxCollider2D>().size = new Vector2(97.42408f, 52.94534f);

        // Layout
        Instantiate<GameObject>(layoutPrefab, Vector3.zero, Quaternion.identity);

        // Player
        GameObject player = Instantiate<GameObject>(playerPrefab, new Vector3(39.5f, 21, 0), Quaternion.identity);
        player.AddComponent<ItemDrop>();
        {
            player.GetComponent<Inventory>().slots = new GameObject[21];
            player.GetComponent<Inventory>().isFull = new bool[21];
        }
        GameObject playerInventory = player.transform.Find("PlayerInventory").gameObject;
        GameObject skeloDevil = Instantiate<GameObject>(skeloDevilPrefab, Vector3.zero, Quaternion.identity);
        GameObject skeleton = Instantiate<GameObject>(skeletonPrefab, new Vector3(34, -1, 0), Quaternion.identity);
        skeleton.name = "Skeleton";
        GameObject skeleton01 = Instantiate<GameObject>(skeletonPrefab, new Vector3(34, 14, 0), Quaternion.identity);
        skeleton01.name = "Skeleton01";
        GameObject zombie = Instantiate<GameObject>(zombiePrefab, new Vector3(35, -15, 0), Quaternion.identity);

        //GameObject sword = Instantiate(swordPrefab, player.transform);
        // sword.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);

        GameObject trader = Instantiate<GameObject>(TraderPrefab, new Vector3(45f, 21, 0), Quaternion.identity);
        GameObject traderUI = Instantiate<GameObject>(TraderUIPrefab, trader.transform.position + Vector3.right, Quaternion.identity);
        {
            trader.GetComponent<Shop>().traderUI = traderUI;
            trader.GetComponent<Shop>().shopInfoTemplate = traderUI.transform.Find("ItemDetailTemplate");
        }

        skeloDevil.AddComponent<Rigidbody2D>();
        skeloDevil.GetComponent<BoxCollider2D>().enabled = true;
        skeloDevil.GetComponent<BoxCollider2D>().isTrigger = false;

        // Follows player
        Camera.main.gameObject.AddComponent<CameraFollowPlayer>();
        Camera.main.gameObject.GetComponent<CameraFollowPlayer>().playerCamera = player;



        int i = 0;
        foreach (GameObject item in player.GetComponent<Inventory>().slots)
        {
            player.GetComponent<Inventory>().slots[i] = playerInventory.transform.Find("Slot" + (i + 1)).gameObject;

            i++;
        }

        // Background music
        bgAudioSource = fade.transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        // Start background music
        bgAudioSource.GetComponent<AudioSource>().loop = true;
        bgAudioSource.GetComponent<AudioSource>().volume = 0.4f;
        AudioManager.Play(AudioClipName.vsBackgroundSong, bgAudioSource.GetComponent<AudioSource>());
    }
}

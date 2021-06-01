using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerPrefab;        // Player prefab
    private GameObject Trader;              // Trader prefab
    private GameObject TraderUI;            // Trader UI prefab
    private GameObject StashUI;             // Stash UI prefab
    private GameObject InventoryManager;    // Inventory manager
    private GameObject Stash;               // Stash prefab
    private GameObject worldBounds;         // World bounds prefab
    private GameObject layoutPrefab;        // Sword prefab
    private GameObject fadeprefab;          // Fade-in canvas prefab
    private GameObject dummyPrefab;         // Dummy prefab

    private GameObject skeleton;            // TEMP skeleton

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        layoutPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/SceneTerrainPrefabs/HubLayout");
        fadeprefab = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/FadeCanvas");
        playerPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        Trader = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/Trader");
        TraderUI = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/TraderUI");
        StashUI = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Stash/StashUI");
        InventoryManager = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Stash/InventoryManager");
        Stash = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Stash/Stash");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        skeleton = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // World bounds
        GameObject world = Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        world.GetComponent<BoxCollider2D>().offset = new Vector2(10.706f, 15.49069f);
        world.GetComponent<BoxCollider2D>().size = new Vector2(191.1954f, 137.701f);

        // Layout
        Instantiate<GameObject>(layoutPrefab, Vector3.zero, Quaternion.identity);

        // Fade-in canvas
        Instantiate<GameObject>(fadeprefab);

        // Player
        GameObject player = Instantiate<GameObject>(playerPrefab, new Vector3(13, 15, 0), Quaternion.identity);
        GameObject playerInventory = player.transform.Find("PlayerInventory").gameObject;
        {
            // Camera follows player
            Camera.main.gameObject.AddComponent<CameraFollowPlayer>();
            Camera.main.gameObject.GetComponent<CameraFollowPlayer>().playerCamera = player;

            // Set up player inventory
            player.GetComponent<Inventory>().slots = new GameObject[21];
            player.GetComponent<Inventory>().isFull = new bool[21];
            int i = 0;
            foreach (GameObject item in player.GetComponent<Inventory>().slots)
            {
                player.GetComponent<Inventory>().slots[i] = playerInventory.transform.Find("Slot" + (i + 1)).gameObject;

                i++;
            }
        }

        // Inventory manager
        GameObject inventoryManager = Instantiate<GameObject>(InventoryManager, StashUI.transform.position, Quaternion.identity);

        // Trader
        GameObject trader = Instantiate<GameObject>(Trader, new Vector2(44.48f, 32.89f), Quaternion.identity);
        GameObject traderUI = Instantiate<GameObject>(TraderUI, trader.transform.position, Quaternion.identity);
        {
            // Set up trader UI
            trader.GetComponent<Shop>().traderUI = traderUI;
            trader.GetComponent<Shop>().shopInfoTemplate = traderUI.transform.Find("ItemDetailTemplate");
        }

        // Stash
        GameObject stash = Instantiate<GameObject>(Stash, new Vector2(52.48f, 32.89f), Quaternion.identity);
        GameObject stashUI = Instantiate<GameObject>(StashUI, StashUI.transform.position, Quaternion.identity);
        {
            // Set up stash inventory
            stashUI.AddComponent<Inventory>();
            stashUI.GetComponent<Inventory>().slots = new GameObject[21];
            stashUI.GetComponent<Inventory>().isFull = new bool[21];

            // Turn off stash UI
            stashUI.GetComponent<Canvas>().enabled = false;

            // Set up stash buttons
            for (int j = 0; j <= 20; j++)
            {
                stashUI.GetComponent<Inventory>().slots[j] = stashUI.gameObject.transform.GetChild(0).gameObject.transform.GetChild(j).gameObject;
                stashUI.GetComponent<Inventory>().slots[j].transform.GetChild(0).gameObject.AddComponent<StashButton>();
            }
            stashUI.transform.GetChild(0).gameObject.transform.GetChild(21).gameObject.AddComponent<StashCloseButton>();
        }

        // Test dummys
        {
            GameObject dummy = Instantiate<GameObject>(dummyPrefab, new Vector2(1.47f, -2.93f), Quaternion.identity);

            // Set tag
            dummy.tag = "Enemy";

            // Set sprite to skeleton
            SpriteRenderer sr = dummy.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/Enemies/FleshySkeleton/spr_EnemyFleshySkeleton0");

            // Add box collider and set to correct size
            BoxCollider2D col = dummy.AddComponent<BoxCollider2D>();
            col.offset = new Vector2(0.05416262f, -0.0464251f);
            col.size = new Vector2(1.708872f, 1.414846f);

            Instantiate<GameObject>(dummy, new Vector2(-4.47f, -2.93f), Quaternion.identity);
            Instantiate<GameObject>(dummy, new Vector2(-11.47f, -2.93f), Quaternion.identity);
        }

        // Teleporter dummy
        {
            GameObject teleporter = Instantiate<GameObject>(dummyPrefab, new Vector2(39.46f, 22.0f), Quaternion.identity);

            // Set sprite to skeleton
            SpriteRenderer sr = teleporter.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/Teleporter/spr_Teleporter");
        }

        // NPC Dummy
        {
            GameObject npc = Instantiate<GameObject>(dummyPrefab, new Vector2(18.12f, 24.96f), Quaternion.identity);

            // Set sprite to skeleton
            SpriteRenderer sr = npc.AddComponent<SpriteRenderer>();
            sr.sprite = Resources.Load<Sprite>("Sprites/Player/spr_CharacterFrame1");

            // Add box collider and set to correct size
            BoxCollider2D col = npc.AddComponent<BoxCollider2D>();
            col.offset = new Vector2(0.05416262f, -0.0464251f);
            col.size = new Vector2(1.708872f, 1.414846f);
        }

        // TEMP Attacking skeleton
        Instantiate<GameObject>(skeleton, new Vector2(-2.77f, 22.2f), Quaternion.identity);
    }
}

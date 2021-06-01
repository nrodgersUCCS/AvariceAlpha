using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to instantiate objects in a scene
/// </summary>
public class Stash_Test_Generator : MonoBehaviour
{
    private GameObject Player;
    private GameObject Trader;
    private GameObject TraderUI;
    private GameObject worldBounds;
    private GameObject StashUI;
    private GameObject InventoryManager;
    private GameObject Stash;
    private GameObject EnchantedArmor;
    private GameObject Skeleton;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        Player = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        Trader = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/Trader");
        TraderUI = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/TraderUI");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");
        StashUI = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Stash/StashUI");
        InventoryManager = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Stash/InventoryManager");
        Stash = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Stash/Stash");
        EnchantedArmor = Resources.Load<GameObject>("Prefabs/Gameplay/Armor/Enchanted Armor");
        Skeleton = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");

        Camera.main.gameObject.AddComponent<ScreenShake>();

        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        GameObject player = Instantiate<GameObject>(Player, Vector3.up * 3, Quaternion.identity);
        player.AddComponent<ItemDrop>();
        GameObject playerInventory = player.transform.Find("PlayerInventory").gameObject;
        GameObject trader = Instantiate<GameObject>(Trader, Vector3.up * 3, Quaternion.identity);
        GameObject traderUI = Instantiate<GameObject>(TraderUI, trader.transform.position, Quaternion.identity);
        GameObject stashUI = Instantiate<GameObject>(StashUI, StashUI.transform.position, Quaternion.identity);
        GameObject stash = Instantiate<GameObject>(Stash, new Vector2(-6f, -1.5f), Quaternion.identity);

        trader.GetComponent<Shop>().traderUI = traderUI;
        trader.GetComponent<Shop>().shopInfoTemplate = traderUI.transform.Find("ItemDetailTemplate");

        player.GetComponent<Inventory>().slots = new GameObject[21];
        player.GetComponent<Inventory>().isFull = new bool[21];

        stashUI.AddComponent<Inventory>();
        stashUI.GetComponent<Inventory>().slots = new GameObject[21];
        stashUI.GetComponent<Inventory>().isFull = new bool[21];

        GameObject inventoryManager = Instantiate<GameObject>(InventoryManager, StashUI.transform.position, Quaternion.identity);
        stashUI.GetComponent<Canvas>().enabled = false;

        GameObject skeleton = Instantiate<GameObject>(Skeleton);
        skeleton.transform.position = new Vector3(-4, -4, 0);
        skeleton.AddComponent<Rigidbody2D>();
        skeleton.GetComponent<Rigidbody2D>().gravityScale = 0;
        skeleton.GetComponent<Rigidbody2D>().freezeRotation = true;
        skeleton.AddComponent<BoxCollider2D>();
        skeleton.GetComponent<BoxCollider2D>().isTrigger = true;

        int i = 0;
        foreach (GameObject item in player.GetComponent<Inventory>().slots)
        {
            player.GetComponent<Inventory>().slots[i] = playerInventory.transform.Find("Slot" + (i + 1)).gameObject;

            i++;
        }

        for(int j = 0; j <= 20; j++)
        {
            stashUI.GetComponent<Inventory>().slots[j] = stashUI.gameObject.transform.GetChild(0).gameObject.
                transform.GetChild(j).gameObject;
            stashUI.GetComponent<Inventory>().slots[j].transform.GetChild(0).gameObject.AddComponent<StashButton>();
        }

        for (int z = 0; z <= 3; z++)
        {
            player.transform.GetChild(0).gameObject.transform.GetChild(21 + z).gameObject.AddComponent<ArmorButton>();
        }

        stashUI.transform.GetChild(0).gameObject.transform.GetChild(21).gameObject.AddComponent<StashCloseButton>();

        GameObject enchantedArmor = Instantiate(EnchantedArmor, player.GetComponent<Inventory>().slots[0].transform, false);
        enchantedArmor.transform.position = enchantedArmor.transform.parent.transform.position;
        enchantedArmor.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        enchantedArmor.name = "Enchanted Armor";
        player.GetComponent<Inventory>().isFull[0] = true;

        Camera.main.gameObject.AddComponent<ScreenShake>();
    }
}

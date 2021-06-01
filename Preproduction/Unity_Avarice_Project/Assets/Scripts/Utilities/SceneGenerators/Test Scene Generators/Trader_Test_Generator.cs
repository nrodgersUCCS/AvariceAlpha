using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to instantiate objects in a scene
/// </summary>
public class Trader_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject Player;                                          // plyaer prefab
    private GameObject PlayerInventory;                                 // playerinventory prefab
    private GameObject Trader;                                          // Trader prefab
    private GameObject TraderUI;                                        // TraderUI prefab
    private GameObject worldBounds;                                     // World bounds prefab

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        Player = Resources.Load<GameObject>("Prefabs/Gameplay/Player/Player");
        PlayerInventory = Resources.Load<GameObject>("Prefabs/Gameplay/Player/PlayerInventory");
        Trader = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/Trader");
        TraderUI = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Trader/TraderUI");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////
        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        GameObject player = Instantiate<GameObject>(Player, Vector3.zero, Quaternion.identity);
        GameObject playerInventory = player.transform.Find("PlayerInventory").gameObject;
        GameObject trader = Instantiate<GameObject>(Trader, Vector3.up * 3, Quaternion.identity);
        GameObject traderUI = Instantiate<GameObject>(TraderUI, trader.transform.position, Quaternion.identity);

        trader.GetComponent<Shop>().traderUI = traderUI;
        trader.GetComponent<Shop>().shopInfoTemplate = traderUI.transform.Find("ItemDetailTemplate");

        player.GetComponent<Inventory>().slots = new GameObject[21];
        player.GetComponent<Inventory>().isFull = new bool[21];

        int i = 0;
        foreach (GameObject item in player.GetComponent<Inventory>().slots)
        {
            player.GetComponent<Inventory>().slots[i] = playerInventory.transform.Find("Slot" + (i + 1)).gameObject;

            i++;
        }
    }
}

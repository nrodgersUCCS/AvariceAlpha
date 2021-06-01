using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject Player;                                          // plyaer prefab
    private GameObject PlayerInventory;                                 // playerinventory prefab
    private GameObject sword;                                           // Player's sword
    private GameObject Skeleton;                                          // Trader prefab
    private GameObject Money;                                        // TraderUI prefab
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
        sword = Resources.Load<GameObject>("Prefabs/Gameplay/Weapons/Sword");
        Skeleton = Resources.Load<GameObject>("Prefabs/Gameplay/Enemies/Skeleton");
        Money = Resources.Load<GameObject>("Prefabs/Gameplay/Interactable/Money/Money");
        worldBounds = Resources.Load<GameObject>("Prefabs/Gameplay/Environment/WorldBounds");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////
        Instantiate<GameObject>(worldBounds, Vector3.zero, Quaternion.identity);

        GameObject player = Instantiate<GameObject>(Player, Vector3.up * 5, Quaternion.identity);
        Instantiate<GameObject>(sword, player.transform);
        sword.transform.localPosition = new Vector2(1.42f, 0.76f);                  // Move to correct location
        GameObject playerInventory = Instantiate<GameObject>(PlayerInventory, Vector3.up * 3, Quaternion.identity);
        GameObject skeleton = Instantiate<GameObject>(Skeleton, Vector3.left * 5 + Vector3.down * 3, Quaternion.identity);
        GameObject money = Instantiate<GameObject>(Money, Vector3.right * 3 + Vector3.down, Quaternion.identity);

        skeleton.AddComponent<Pickup>();


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


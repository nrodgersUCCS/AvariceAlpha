using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Test_Generator : MonoBehaviour
{
    /////////////////////////////////////////////////////////////
    // KEEP REFERENCES TO ANY PREFABS IN THE SCENE YOU WILL NEED
    /////////////////////////////////////////////////////////////
    private GameObject playerInventoryPrefab;
    private GameObject dummyPrefab;

    /// <summary>
    /// Called before Start
    /// </summary>
    private void Awake()
    {
        ///////////////////////////////////////
        // LOAD EACH PREFAB RESOURCE ONLY ONCE
        ///////////////////////////////////////
        playerInventoryPrefab = Resources.Load<GameObject>("Prefabs/Gameplay/Player/PlayerInventory");
        dummyPrefab = Resources.Load<GameObject>("Prefabs/DummyObject");

        ///////////////////////////////////////////
        // ADD IN OBJECTS
        // PERFORM ANY NECEESARY ACTIONS 
        // (ROTATIONS, SCALING, ADD SCRIPTS, ETC.)
        ///////////////////////////////////////////

        // Player
        GameObject player = Instantiate<GameObject>(dummyPrefab, Vector3.zero, Quaternion.identity);
        {
            // Set tag
            player.tag = "Player";

            // Add audio source
            player.AddComponent<AudioSource>();

            // Add menu scripts
            player.AddComponent<MenuToggle>();
            player.AddComponent<Inventory>();
        }

        // Player inventory
        GameObject playerInventory = Instantiate<GameObject>(playerInventoryPrefab, player.transform);
        {
            // Set menu toggle to inventory
            player.GetComponent<MenuToggle>().Inventory = playerInventory.GetComponent<Canvas>();
        }
    }
}

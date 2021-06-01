using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    Inventory playerInventory;
    Inventory stashInventory;

    GameObject armorSlot;

    GameObject EnchantedArmorAOE;
    bool enchantedArmorAOESpawned;

    AudioSource audioSource;

    //bool isStashOpen;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player(Clone)").GetComponent<Inventory>();
        if(GameObject.Find("StashUI(Clone)") != null)
            stashInventory = GameObject.Find("StashUI(Clone)").GetComponent<Inventory>();
        armorSlot = GameObject.Find("ArmorSlot");

        EnchantedArmorAOE = Resources.Load<GameObject>("Prefabs/Gameplay/Armor/EnchantedArmorAOE");
        enchantedArmorAOESpawned = false;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // Checks if player is alive
        if (GameObject.Find("Player(Clone)") != null)
        {
            // Spawns Enchanted Armor AOE when Enchated Armor is Equipped
            if (armorSlot.transform.childCount != 0 && !enchantedArmorAOESpawned)
            {
                if (armorSlot.transform.GetChild(0).gameObject.name == "Enchanted Armor" && !enchantedArmorAOESpawned)
                {
                    GameObject enchantedArmorAOE = Instantiate(EnchantedArmorAOE, GameObject.Find("Player(Clone)").gameObject.
                        transform, false);
                    enchantedArmorAOESpawned = true;
                }
            }

            // Removes Enchanted Armor AOE when Enchanted Armor is unequiped or destroyed
            if (armorSlot.transform.childCount == 0 && enchantedArmorAOESpawned)
            {
                Destroy(GameObject.Find("EnchantedArmorAOE(Clone)"));
                enchantedArmorAOESpawned = false;
            }
        }
    }

    /// <summary>
    /// Moves object from inventory to stash
    /// </summary>
    /// <param name="button"></param>
    public void Move(GameObject button)
    {
        if(GameObject.Find("StashUI(Clone)") != null)
        {
            if (GameObject.Find("StashUI(Clone)").GetComponent<Canvas>().enabled == true)
            {
                // Finds the slot tht was clicked
                for (int i = 0; i <= 20; i++)
                {
                    if (button.transform.parent.gameObject == playerInventory.slots[i])
                    {
                        if (playerInventory.isFull[i])
                        {
                            // Finds an empty slot in stash
                            for (int j = 0; j <= 20; j++)
                            {
                                if (stashInventory.isFull[j] == false)
                                {
                                    // Moves object to stash
                                    stashInventory.isFull[j] = true;
                                    GameObject item = Instantiate(playerInventory.slots[i].transform.GetChild(1).gameObject,
                                        stashInventory.slots[j].transform, false);
                                    item.name = playerInventory.slots[i].transform.GetChild(1).gameObject.name;
                                    Destroy(playerInventory.slots[i].transform.GetChild(1).gameObject);
                                    playerInventory.isFull[i] = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Moves armor from inventory to armor slot
    /// </summary>
    /// <param name="button"></param>
    public void Equip(GameObject button)
    {
        if (GameObject.Find("StashUI(Clone)") != null)
        {
            if (GameObject.Find("StashUI(Clone)").GetComponent<Canvas>().enabled == false)
            {
                // Finds empty space in inventory & places armro there
                for (int i = 0; i <= 20; i++)
                {
                    if (button.transform.parent.gameObject == playerInventory.slots[i])
                    {
                        if (playerInventory.isFull[i])
                        {
                            if (playerInventory.slots[i].transform.GetChild(1).gameObject.name == "Enchanted Armor")
                            {
                                if (button.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(21).
                                    gameObject.transform.childCount == 0)
                                {
                                    GameObject enchantedArmor = Instantiate(playerInventory.slots[i].transform.GetChild(1).
                                        gameObject, button.transform.parent.gameObject.transform.parent.gameObject.transform.
                                        GetChild(21).gameObject.transform, false);
                                    enchantedArmor.name = playerInventory.slots[i].transform.GetChild(1).gameObject.name;
                                    Destroy(playerInventory.slots[i].transform.GetChild(1).gameObject);
                                    playerInventory.isFull[i] = false;
                                    AudioManager.Play(AudioClipName.vsArmorPickup, audioSource);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Opens the stash
    /// </summary>
    public void OpenStash()
    {
        GameObject.Find("Player(Clone)").transform.Find("PlayerInventory").GetComponent<Canvas>().enabled = true;
        GameObject.Find("StashUI(Clone)").GetComponent<Canvas>().enabled = true;
        //isStashOpen = true;
    }

    /// <summary>
    /// Closes the stash
    /// </summary>
    public void CloseStash()
    {
        GameObject.Find("Player(Clone)").transform.Find("PlayerInventory").GetComponent<Canvas>().enabled = false;
        GameObject.Find("StashUI(Clone)").GetComponent<Canvas>().enabled = false;
        //isStashOpen = false;
    }
}
